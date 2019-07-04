namespace TH.ClientLayer.Data
{
    public interface IDbContextFactory
    {
        ApplicationContext For();
    }

    public class DbContextFactory : IDbContextFactory
    {
        public ApplicationContext For()
        {
            return new ApplicationContext();
        }
    }
}