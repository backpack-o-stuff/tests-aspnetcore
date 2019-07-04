using TH.ClientLayer.Data;

namespace TH.Tests.Infrastructure.Fakes
{
    public class InMemoryDbContextFactory : IDbContextFactory
    {
        public ApplicationContext For()
        {
            return new InMemoryDbContext();
        }
    }
}