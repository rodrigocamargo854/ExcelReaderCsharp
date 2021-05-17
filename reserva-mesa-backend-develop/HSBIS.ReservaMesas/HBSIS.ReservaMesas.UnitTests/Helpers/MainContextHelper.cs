using HBSIS.ReservaMesas.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HBSIS.ReservaMesas.UnitTests.Helpers
{
    public class MainContextHelper
    {
        public MainContext CreateInMemoryMainContext()
        {
            DbContextOptions<MainContext> options;
            var builder = new DbContextOptionsBuilder<MainContext>();
            builder.UseInMemoryDatabase("InMemoryDatabase");
            options = builder.Options;

            MainContext dbContext = new MainContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            return dbContext;
        }
    }
}
