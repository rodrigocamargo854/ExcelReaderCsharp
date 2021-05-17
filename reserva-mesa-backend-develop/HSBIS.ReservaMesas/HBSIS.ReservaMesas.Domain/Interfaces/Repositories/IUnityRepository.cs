using HBSIS.ReservaMesas.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HBSIS.ReservaMesas.Domain.Interfaces.Repositories
{
    public interface IUnityRepository : IGenericRepository<Unity>
    {
        Task<IEnumerable<Unity>> GetAll();
    }
}
