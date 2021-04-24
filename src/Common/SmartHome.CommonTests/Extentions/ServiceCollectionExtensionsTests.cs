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
                s.AddDecorator<IDecoratedService, Decorator>(c =>
                {
                    c.AddSingleton<IDecoratedService, Decorated>();
                });
            });

            var count = provider.GetServices<IDecoratedService>().ToList().Count;
            count.Should().Be(2);

            var instance = provider.GetRequiredService<IDecoratedService>();

            var decorator = Assert.IsType<Decorator>(instance);

            Assert.IsType<Decorated>(decorator.Inner);
        }

        [Fact]
        public void TryAddDecoratorSomeTest()
        {
            var provider = ConfigureProvider(s =>
            {
                s.TryAddDecorator<IDecoratedService, Decorator>(sc1 =>
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

            Assert.IsType<OtherDecorated>(decorator.Inner);
        }

        [Fact]
        public void TryAddDecoratorTest()
        {

            var provider = ConfigureProvider(s =>
            {
                s.AddDecorator<IDecoratedService, Decorator>(c =>
                {
                    c.AddSingleton<IDecoratedService, Decorated>();
                });
                s.TryAddDecorator<IDecoratedService, Decorator>(c =>
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
            public OtherDecorated(IDecoratedService inner = null, IService injectedService = null)
            {
            }
        }

        private class SomeRandomService : IService { }
    }
}