using HBSIS.ReservaMesas.Domain.Entities;
using HBSIS.ReservaMesas.Domain.Interfaces.Repositories;
using HBSIS.ReservaMesas.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HBSIS.ReservaMesas.Persistence.Repositories
{
    public class WorkstationRepository : GenericRepository<Workstation>, IWorkstationRepository
    {
        public WorkstationRepository(MainContext dbContext) : base(dbContext)
        {
        }

        public async Task<Workstation> GetByName(string name)
        {
            return await Query().SingleOrDefaultAsync(x => x.Name == name);
        }

        public async Task<IEnumerable<Workstation>> GetInactivesByFloor(int floorId)
        {
            return await Query().Where(workstation => workstation.FloorId == floorId && !workstation.Active).ToListAsync();
        }
    }
}
