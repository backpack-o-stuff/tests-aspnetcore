using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace TH.ClientLayer.Infrastructure.IoC
{
    public static class DependencyRegistrations
    {
        private static readonly Assembly[] AutoResolvedAssemblies = new []
        {
            Assembly.Load("TH.ClientLayer")
        };

        public static void Register(IServiceCollection services)
        {
            RegisterResolvers(services);
        }

        private static void RegisterResolvers(IServiceCollection services)
        {
            services.Scan(sc => sc
                .FromCallingAssembly()
                .FromAssemblies(AutoResolvedAssemblies)
                .AddClasses()
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );
        }
    }
}