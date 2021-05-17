using HBSIS.ReservaMesas.Domain.Entities;
using HBSIS.ReservaMesas.Domain.Interfaces.Repositories;
using HBSIS.ReservaMesas.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HBSIS.ReservaMesas.Persistence.Repositories
{
    public class FloorRepository : GenericRepository<Floor>, IFloorRepository
    {
        public FloorRepository(MainContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Floor>> GetAll()
        {
            return await Query().ToListAsync();
        }

        public async Task<Floor> GetByCode(string code)
        {
            return await Query().Where(x => x.Code == code).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Floor>> GetFloorsByUnityId(int unityId)
        {
            return await Query().Where(x => x.UnityId == unityId).ToListAsync();
        }
    }
}
