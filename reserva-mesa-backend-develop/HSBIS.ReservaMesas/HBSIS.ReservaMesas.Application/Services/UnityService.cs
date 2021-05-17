using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HBSIS.ReservaMesas.Application.Models.Unity;
using HBSIS.ReservaMesas.Application.Services.Interfaces;
using HBSIS.ReservaMesas.Domain.Exceptions;
using HBSIS.ReservaMesas.Domain.Interfaces.Repositories;

namespace HBSIS.ReservaMesas.Application.Services
{
    public class UnityService : IUnityService
    {
        private readonly IUnityRepository _unityRepository;

        public UnityService(IUnityRepository unityRepository)
        {
            _unityRepository = unityRepository;
        }

        public async Task<IEnumerable<UnityResponseModel>> GetAll()
        {
            var units = await _unityRepository.GetAll();

            if(units == null || !units.Any())
            {
               throw new NotFoundException("Unidades não encontradas!");
            }

            return units.Select(unity => new UnityResponseModel { Active = unity.Active, Id = unity.Id, Name = unity.Name });
        }
    }
}
