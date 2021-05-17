using System.Linq;
using HBSIS.ReservaMesas.Domain.Entities;
using HBSIS.ReservaMesas.Persistence.Context;
using HBSIS.ReservaMesas.Persistence.Seeds.Data;
using Microsoft.EntityFrameworkCore;

namespace HBSIS.ReservaMesas.Persistence.Seeds
{
    public class InitialFloorSeed
    {
        public void SeedData(MainContext context)
        {
            DbSet<Floor> dbSet = context.Set<Floor>();

            if (!dbSet.Any())
            {
                dbSet.AddRange(new FloorList().Data);

                context.SaveChanges();
            }

        }
    }
}
