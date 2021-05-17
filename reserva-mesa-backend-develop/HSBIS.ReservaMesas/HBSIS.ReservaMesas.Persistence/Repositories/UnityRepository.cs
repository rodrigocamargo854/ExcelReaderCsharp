using HBSIS.ReservaMesas.Domain.Entities;
using HBSIS.ReservaMesas.Domain.Interfaces.Repositories;
using HBSIS.ReservaMesas.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HBSIS.ReservaMesas.Persistence.Repositories
{
    public class UnityRepository : GenericRepository<Unity>, IUnityRepository
    {
        public UnityRepository(MainContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Unity>> GetAll()
        {
            return await Query().ToListAsync();
        }
    }
}
