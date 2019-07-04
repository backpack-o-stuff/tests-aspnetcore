using Microsoft.EntityFrameworkCore;
using TH.ClientLayer.Data.Monsters;
using TH.ClientLayer.Models;

namespace TH.ClientLayer.Data
{
    public class ApplicationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=test-helpers-local.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MonsterModelBuilder.Configure(modelBuilder);
        }

        public DbSet<Monster> Monsters { get; set; }
    }
}