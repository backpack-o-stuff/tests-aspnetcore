using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using TH.ClientLayer.Data;
using TH.ClientLayer.Infrastructure.IoC;
using TH.Tests.Infrastructure.Fakes;

namespace TH.Tests.Infrastructure.TestBases
{
    public class IntegratedFor<T> : ArrangeActAssertOn
        where T : class
    {
        protected readonly List<Action<IServiceCollection>> DependencyFakes = new List<Action<IServiceCollection>>();

        protected T SUT;

        public IntegratedFor()
        {
            SharedBeforeAll();
            SUT = Resolve<T>();
        }

        protected virtual void BeforeEach() {}

        protected TResolveFor Resolve<TResolveFor>()
        {
            return DependencyRegistrations.Resolve<TResolveFor>(DependencyFakes);
        }

        private void SharedBeforeAll()
        {
            DependencyFakes.Add((services) => 
            {
                services.AddScoped<IDbContextFactory, InMemoryDbContextFactory>();
            });
        }
    }
}