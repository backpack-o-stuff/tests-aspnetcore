using Microsoft.EntityFrameworkCore;
using TH.ClientLayer.Data;

namespace TH.Tests.Infrastructure.Fakes
{
    public class InMemoryDbContext : ApplicationContext
    {
        private const string DB_NAME = "INMEMORY_TEST_DB";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(DB_NAME);
        }
    }
}