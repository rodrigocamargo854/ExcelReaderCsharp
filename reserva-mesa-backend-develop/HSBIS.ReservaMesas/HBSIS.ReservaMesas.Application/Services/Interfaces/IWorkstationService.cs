using HBSIS.ReservaMesas.Application.Models.Workstations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HBSIS.ReservaMesas.Application.Services.Interfaces
{
    public interface IWorkstationService
    {
        Task<WorkstationResponseModel> GetByName(string name);
        Task Update(string workstationName, WorkstationRequestModel requestModel);
        Task<IEnumerable<WorkstationResponseModel>> GetInactivesByFloor(int floorId);
    }
}
