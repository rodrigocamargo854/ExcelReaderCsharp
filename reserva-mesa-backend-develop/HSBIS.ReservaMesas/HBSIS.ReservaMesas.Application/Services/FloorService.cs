using HBSIS.ReservaMesas.Application.Models.Floors;
using HBSIS.ReservaMesas.Application.Services.Interfaces;
using HBSIS.ReservaMesas.Domain.Exceptions;
using HBSIS.ReservaMesas.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HBSIS.ReservaMesas.Application.Services
{
    public sealed class FloorService : IFloorService
    {
        private readonly IFloorRepository _floorRepository;

        public FloorService(IFloorRepository floorRepository)
        {
            _floorRepository = floorRepository;
        }

        public async Task Update(int floorId, FloorRequestModel requestModel)
        {
            var floor = await _floorRepository.GetById(floorId);

            if (floor == null)
            {
                throw new NotFoundException("Andar não encontrado!");
            }

            floor.Update(requestModel.Name, requestModel.Active, requestModel.UnityId);

            floor.ValidateEntity();

            _floorRepository.Update(floor);

            await _floorRepository.Save();
        }

        public async Task Delete(int id)
        {
            var floor = await _floorRepository.GetById(id);

            if (floor == null)
            {
                throw new NotFoundException("Andar não encontrado!");
            }

            floor.Delete();

            _floorRepository.Update(floor);

            await _floorRepository.Save();
        }

        public async Task<FloorResponseModel> GetById(int id)
        {
            var floor = await _floorRepository.GetById(id);

            if (floor == null)
            {
                throw new NotFoundException("Andar não encontrado!");
            }

            return new FloorResponseModel(floor.Id, floor.Name, floor.Active, floor.Code, floor.UnityId);
        }

        public async Task<IEnumerable<FloorResponseModel>> GetAll()
        {
            var floors = await _floorRepository.GetAll();

            if (floors == null)
            {
                throw new NotFoundException("Andar não encontrado!");
            }

            return floors.Select(x => new FloorResponseModel(x.Id, x.Name, x.Active, x.Code, x.UnityId));
        }

        public async Task<IEnumerable<FloorResponseModel>> GetFloorsByUnityId(int unityId)
        {
            var floors = await _floorRepository.GetFloorsByUnityId(unityId);


            if(floors == null || !floors.Any())
            {
                throw new NotFoundException("Andares não encontrados!");
            }

            return floors.Select(x => new FloorResponseModel(x.Id, x.Name, x.Active, x.Code, x.UnityId));
        }

        public async Task<FloorResponseModel> GetByCode(string code)
        {
            var floor = await _floorRepository.GetByCode(code);

            if (floor == null)
            {
                throw new NotFoundException("Andar não encontrado.");
            }

            return new FloorResponseModel(floor.Id, floor.Name, floor.Active, floor.Code, floor.UnityId);
        }
    }
}
