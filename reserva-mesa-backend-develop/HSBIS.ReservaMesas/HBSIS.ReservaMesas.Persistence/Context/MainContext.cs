using HBSIS.ReservaMesas.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;

namespace HBSIS.ReservaMesas.Persistence.Context
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions options)
        : base(options)
        {
        }

        public MainContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(MainContext).Assembly);

            modelBuilder.ApplyConfiguration(new UnityMapConfig());

            modelBuilder.ApplyConfiguration(new FloorMapConfig());

            modelBuilder.ApplyConfiguration(new WorkstationMapConfig());

            modelBuilder.ApplyConfiguration(new ReservationMapConfig());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer("SqlServerConnection");
            }
        }
    }
}
