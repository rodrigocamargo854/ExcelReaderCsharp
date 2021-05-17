using HBSIS.ReservaMesas.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HBSIS.ReservaMesas.Domain.Interfaces.Repositories
{
    public interface IWorkstationRepository : IGenericRepository<Workstation>
    {
        Task<Workstation> GetByName(string name);
        Task<IEnumerable<Workstation>> GetInactivesByFloor(int floorId);
    }
}
