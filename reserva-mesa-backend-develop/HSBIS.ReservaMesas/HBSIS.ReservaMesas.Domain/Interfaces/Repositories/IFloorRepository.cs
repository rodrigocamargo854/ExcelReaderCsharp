using HBSIS.ReservaMesas.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HBSIS.ReservaMesas.Domain.Interfaces.Repositories
{
    public interface IFloorRepository : IGenericRepository<Floor>
    {
        Task<IEnumerable<Floor>> GetAll();
        Task<IEnumerable<Floor>> GetFloorsByUnityId(int unityId);
        Task<Floor> GetByCode(string code);
    }
}
