using HBSIS.ReservaMesas.Domain.Entities;
using HBSIS.ReservaMesas.Persistence.Context;
using HBSIS.ReservaMesas.Persistence.Seeds.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HBSIS.ReservaMesas.Persistence.Seeds
{
    public class InitialUnitySeed
    {
        public void SeedData(MainContext context)
        {
            DbSet<Unity> dbSet = context.Set<Unity>();
            if (!dbSet.Any())
            {
                dbSet.AddRange(new UnityList().Data);
                context.SaveChanges();
            }
        }
    }
}
