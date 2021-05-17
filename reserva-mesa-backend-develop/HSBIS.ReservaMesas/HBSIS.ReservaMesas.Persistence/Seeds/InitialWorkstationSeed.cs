using HBSIS.ReservaMesas.Domain.Entities;
using HBSIS.ReservaMesas.Persistence.Context;
using HBSIS.ReservaMesas.Persistence.Seeds.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HBSIS.ReservaMesas.Persistence.Seeds
{
    public class InitialWorkstationSeed
    {
        public void SeedData(MainContext context)
        {
            DbSet<Workstation> dbSet = context.Set<Workstation>();

            if (!dbSet.Any())
            {
                dbSet.AddRange(new WorkstationList().Data);

                context.SaveChanges();
            }
        }
    }
}
