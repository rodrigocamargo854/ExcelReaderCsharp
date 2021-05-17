using HBSIS.ReservaMesas.Application.Models.Floors;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HBSIS.ReservaMesas.Application.Services.Interfaces
{
    public interface IFloorService
    {
        Task Update(int floorId, FloorRequestModel requestModel);
        Task Delete(int id);
        Task<FloorResponseModel> GetById(int id);
        Task<IEnumerable<FloorResponseModel>> GetAll();
        Task<IEnumerable<FloorResponseModel>> GetFloorsByUnityId(int unityId);
        Task<FloorResponseModel> GetByCode(string code);
    }
}
