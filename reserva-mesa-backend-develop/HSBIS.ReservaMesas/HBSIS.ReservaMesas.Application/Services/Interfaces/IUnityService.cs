using HBSIS.ReservaMesas.Application.Models.Unity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HBSIS.ReservaMesas.Application.Services.Interfaces
{
    public interface IUnityService
    {
        Task<IEnumerable<UnityResponseModel>> GetAll();
    }
}
