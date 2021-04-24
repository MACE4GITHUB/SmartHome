using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;

namespace SmartHome.Common.Extentions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Tries add Decorator.
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="TDecorator"></typeparam>
        /// <param name="serviceCollection"></param>
        /// <param name="configureDecorateServices"></param>
        public static void TryAddDecorator<TInterface, TDecorator>
            (this IServiceCollection serviceCollection, Action<IServiceCollection> configureDecorateServices)
            where TInterface : class
            where TDecorator : class, TInterface
        {
            if (!serviceCollection.Where(sd => sd.ServiceType == typeof(TInterface) 
                                               && sd.GetImplementationType() == typeof(TDecorator)).ToArray().Any())
            {
                serviceCollection.AddDecorator<TInterface, TDecorator>(configureDecorateServices);
            }
        }

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
            var decorateServices = new ServiceCollection();
            configureDecorateServices(decorateServices);

            var decorateDescriptor = decorateServices.SingleOrDefault(sd => sd.ServiceType == typeof(TInterface));

            if (decorateDescriptor == null)
            {
                throw new InvalidOperationException($"{typeof(TInterface).Name} is not configured.");
            }

            decorateServices.Remove(decorateDescriptor);
            serviceCollection.Add(decorateServices);

            var decoratorInstanceFactory = ActivatorUtilities.CreateFactory(
                typeof(TDecorator), new[] { typeof(TInterface) });

            var decorateImplType = decorateDescriptor.GetImplementationType();

            Func<IServiceProvider, TDecorator> decoratorFactory = sp =>
            {
                var decorate = sp.GetRequiredService(decorateImplType);
                var decorator = (TDecorator)decoratorInstanceFactory(sp, new[] { decorate });
                return decorator;
            };

            var decoratorDescriptor = ServiceDescriptor.Describe(typeof(TInterface), decoratorFactory, decorateDescriptor.Lifetime);

            decorateDescriptor = UpdateDecorateDescriptor(decorateDescriptor);

            serviceCollection.Add(decorateDescriptor);
            serviceCollection.Add(decoratorDescriptor);
        }

        private static ServiceDescriptor UpdateDecorateDescriptor(ServiceDescriptor decorateDescriptor)
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
