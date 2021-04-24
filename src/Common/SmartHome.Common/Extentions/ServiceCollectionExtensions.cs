using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SmartHome.Common.Extentions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds Decorator.
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="TDecorator"></typeparam>
        /// <param name="serviceCollection"></param>
        /// <param name="configureDecorateServices"></param>
        public static void AddDecorator<TInterface, TDecorator>
        (this IServiceCollection serviceCollection, Action<IServiceCollection> configureDecorateServices)
            where TInterface : class
            where TDecorator : class, TInterface
        {
            SetDescriptors<TInterface>(configureDecorateServices,
                out var decorateDescriptors,
                out var nonDecorateDescriptors);

            ThrowIfInterfaceNotConfigured<TInterface>(decorateDescriptors);
            ThrowIfDuplicate<TInterface, TDecorator>(serviceCollection);
            ThrowIfStackOverflow<TInterface, TDecorator>(decorateDescriptors);

            serviceCollection.Add(nonDecorateDescriptors);

            foreach (var decorateDescriptor in decorateDescriptors)
            {
                var decoratorInstanceFactory = ActivatorUtilities.CreateFactory(
                    typeof(TDecorator), new[] { typeof(TInterface) });

                var decorateImplType = decorateDescriptor.GetImplementationType();

                Func<IServiceProvider, TDecorator> decoratorFactory = sp =>
                {
                    var decorate = sp.GetRequiredService(decorateImplType);
                    var decorator = (TDecorator)decoratorInstanceFactory(sp, new[] { decorate });
                    return decorator;
                };

                var decoratorDescriptor =
                    ServiceDescriptor.Describe(typeof(TInterface), decoratorFactory, decorateDescriptor.Lifetime);

                serviceCollection.Add(decorateDescriptor.UpdateDecorateDescriptor());
                serviceCollection.Add(decoratorDescriptor);
            }
        }

        private static void SetDescriptors<TInterface>(Action<IServiceCollection> configureDecorateServices,
            out List<ServiceDescriptor> decorateDescriptors, out List<ServiceDescriptor> nonDecorateDescriptors)
            where TInterface : class
        {
            var services = new ServiceCollection();
            configureDecorateServices(services);

            decorateDescriptors = services.Where(sd => sd.ServiceType == typeof(TInterface)).ToList();
            nonDecorateDescriptors = services.Where(sd => sd.ServiceType != typeof(TInterface)).ToList();
        }

        private static void ThrowIfInterfaceNotConfigured<TInterface>(IEnumerable<ServiceDescriptor> decorateDescriptors)
            where TInterface : class
        {
            if (!decorateDescriptors.Any())
            {
                throw new InvalidOperationException($"{typeof(TInterface).Name} is not configured.");
            }
        }

        private static void ThrowIfStackOverflow<TInterface, TDecorator>(IEnumerable<ServiceDescriptor> decorateDescriptors)
            where TInterface : class where TDecorator : class, TInterface
        {
            var descriptors = decorateDescriptors.ToList();

            if (descriptors.Any(sd =>
                sd.ServiceType == typeof(TInterface)
                && sd.GetImplementationType() == typeof(TDecorator)))
            {
                throw new InvalidOperationException(
                    $"{typeof(TInterface).Name} has detected a stack overflow with some {typeof(TDecorator).Name}.");
            }

            Type otherType = null;
            if (descriptors.Any(sd =>
                {
                    otherType = sd.GetImplementationType();
                    var constructor = otherType.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                        .OrderByDescending(c => c.GetParameters().Length).First();

                    var parameters = constructor.GetParameters();

                    return sd.ImplementationFactory?.Method.Name != "<AddDecorator>b__0"
                           && parameters.Any(parameter => parameter.ParameterType == typeof(TInterface));
                })
            )
            {
                throw new InvalidOperationException(
                    $"{typeof(TInterface).Name} has detected a stack overflow with other {typeof(TDecorator).Name} ({otherType.Name}).");
            }
        }

        private static void ThrowIfDuplicate<TInterface, TDecorator>(IServiceCollection serviceCollection)
            where TInterface : class where TDecorator : class, TInterface
        {
            if (serviceCollection.Where(sd =>
                sd.ServiceType == typeof(TInterface)
                && sd.GetImplementationType() == typeof(TDecorator)).ToArray().Any())
            {
                throw new InvalidOperationException(
                    $"{typeof(TInterface).Name} has detected a duplicate {typeof(TDecorator).Name}.");
            }
        }

        private static ServiceDescriptor UpdateDecorateDescriptor(this ServiceDescriptor decorateDescriptor)
        {
            var decorateImplType = decorateDescriptor.GetImplementationType();

            decorateDescriptor = decorateDescriptor.ImplementationFactory != null
                ? ServiceDescriptor.Describe(decorateImplType, decorateDescriptor.ImplementationFactory, decorateDescriptor.Lifetime)
                : decorateDescriptor.ImplementationInstance != null
                    ? ServiceDescriptor.Singleton(decorateImplType, decorateDescriptor.ImplementationInstance)
                    : ServiceDescriptor.Describe(decorateImplType, decorateImplType, decorateDescriptor.Lifetime);

            return decorateDescriptor;
        }

        private static Type GetImplementationType(this ServiceDescriptor serviceDescriptor) =>
            serviceDescriptor.ImplementationType ?? (serviceDescriptor.ImplementationInstance != null
                ? serviceDescriptor.ImplementationInstance.GetType()
                : serviceDescriptor.ImplementationFactory != null
                    ? serviceDescriptor.ImplementationFactory.GetType().GenericTypeArguments[1]
                    : throw new InvalidOperationException($"The implementation type of {serviceDescriptor.GetType().Name} is not registered."));
    }
}
