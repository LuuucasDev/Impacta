using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ExpoCenter.Repositorios.SqlServer
{
    public class ExpoCenterDbContextFactory : IDesignTimeDbContextFactory<ExpoCenterDbContext>
    {
        public ExpoCenterDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ExpoCenterDbContext>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ExpoCenter;Integrated Security=True");
            return new ExpoCenterDbContext(optionsBuilder.Options);
        }
    }
}