using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Xunit;

namespace SmartHome.Common.Extentions.Tests
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddDecoratorTest()
        {
            var provider = ConfigureProvider(s =>
            {
                s.AddTransient<IService, SomeRandomService>();
                s.AddDecorator<IDecoratedService, Decorator>(c =>
                {
                    c.AddSingleton<IDecoratedService, Decorated>();
                });
            });

            var count = provider.GetServices<IDecoratedService>().ToList().Count;
            count.Should().Be(1);

            var instance = provider.GetRequiredService<IDecoratedService>();

            var decorator = Assert.IsType<Decorator>(instance);

            Assert.IsType<Decorated>(decorator.Inner);
        }

        [Fact]
        public void AddDecoratorExceptionStackOverflowSomeTest()
        {

            Action action = () => ConfigureProvider(s =>
            {
                s.AddTransient<IService, SomeRandomService>();
                s.AddDecorator<IDecoratedService, Decorator>(c =>
                {
                    c.AddSingleton<IDecoratedService, Decorated>();
                    c.AddSingleton<IDecoratedService, Decorator>();
                });
            });

            action.Should().Throw<InvalidOperationException>().WithMessage("IDecoratedService has detected a stack overflow with some Decorator.");
        }

        [Fact]
        public void AddDecoratorExceptionStackOverflowOtherTest()
        {

            Action action = () => ConfigureProvider(s =>
            {
                s.AddTransient<IService, SomeRandomService>();
                s.AddDecorator<IDecoratedService, Decorator>(c =>
                {
                    c.AddSingleton<IDecoratedService, Decorated>();
                    c.AddSingleton<IDecoratedService, OtherDecorated>();
                });
            });

            action.Should().Throw<InvalidOperationException>().WithMessage("IDecoratedService has detected a stack overflow with other Decorator (OtherDecorated).");
        }

        [Fact]
        public void AddDecoratorExceptionDuplicateTest()
        {

            Action action = () => ConfigureProvider(s =>
            {
                s.AddTransient<IService, SomeRandomService>();
                s.AddDecorator<IDecoratedService, Decorator>(c =>
                {
                    c.AddSingleton<IDecoratedService, Decorated>();
                });
                s.AddDecorator<IDecoratedService, Decorator>(c =>
                {
                    c.AddSingleton<IDecoratedService, Decorated>();
                });
            });

            action.Should().Throw<InvalidOperationException>().WithMessage("IDecoratedService has detected a duplicate Decorator.");
        }

        [Fact]
        public void AddDecoratorExceptionConfiguredTest()
        {
            Action action = () => ConfigureProvider(s =>
            {
                s.AddTransient<IService, SomeRandomService>();
                s.AddDecorator<IDecoratedService, Decorator>(c => { });
            });

            action.Should().Throw<InvalidOperationException>().WithMessage("IDecoratedService is not configured.");
        }

        [Fact]
        public void AddDecoratorSomeTest()
        {
            var provider = ConfigureProvider(s =>
            {
                s.AddDecorator<IDecoratedService, Decorator>(sc1 =>
                {
                    sc1.AddTransient<IService, SomeRandomService>();
                    sc1.AddDecorator<IDecoratedService, OtherDecorated>(
                        sc2 =>
                        {
                            sc2.AddSingleton<IDecoratedService, Decorated>();
                        });
                });
            });

            var count = provider.GetServices<IDecoratedService>().ToList().Count;
            count.Should().Be(1);

            var instance = provider.GetRequiredService<IDecoratedService>();

            var decorator = Assert.IsType<Decorator>(instance);
            var otherDecorated = Assert.IsType<OtherDecorated>(decorator.Inner);

            Assert.IsType<Decorated>(otherDecorated.Inner);
        }

        private static ServiceProvider ConfigureProvider(Action<IServiceCollection> configure)
        {
            var services = new ServiceCollection();

            configure(services);

            return services.BuildServiceProvider();
        }

        public interface IDecoratedService { }

        public interface IService { }

        public class Decorated : IDecoratedService
        {
            public Decorated(IService injectedService = null)
            {
                InjectedService = injectedService;
            }

            public IService InjectedService { get; }
        }

        public class Decorator : IDecoratedService
        {
            public Decorator(IDecoratedService inner, IService injectedService = null)
            {
                Inner = inner;
                InjectedService = injectedService;
            }

            public IDecoratedService Inner { get; }

            public IService InjectedService { get; }
        }

        public class OtherDecorated : IDecoratedService
        {
            private readonly IService _injectedService;

            public IDecoratedService Inner { get; }

            public OtherDecorated(IDecoratedService inner = null, IService injectedService = null)
            {
                Inner = inner;
                _injectedService = injectedService;
            }
        }

        private class SomeRandomService : IService { }
    }
}