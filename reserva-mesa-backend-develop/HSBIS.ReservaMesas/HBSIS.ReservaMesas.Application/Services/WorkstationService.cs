using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HBSIS.ReservaMesas.Application.Models.Workstations;
using HBSIS.ReservaMesas.Application.Services.Interfaces;
using HBSIS.ReservaMesas.Domain.Exceptions;
using HBSIS.ReservaMesas.Domain.Interfaces.Repositories;

namespace HBSIS.ReservaMesas.Application.Services
{
    public class WorkstationService : IWorkstationService
    {
        private readonly IWorkstationRepository _workstationRepository;

        public WorkstationService(IWorkstationRepository workstationRepository)
        {
            _workstationRepository = workstationRepository;
        }

        public async Task<IEnumerable<WorkstationResponseModel>> GetInactivesByFloor(int floorId)
        {
            var workstations = await _workstationRepository.GetInactivesByFloor(floorId);

            if(workstations == null || !workstations.Any())
            {
                throw new NotFoundException("Estações de trabalho não encontradas.");
            }

            return workstations.Select(workstation => new WorkstationResponseModel(workstation.Id, workstation.Name, workstation.Active, workstation.FloorId));
        }

        public async Task<WorkstationResponseModel> GetByName(string name)
        {
            var workstation = await _workstationRepository.GetByName(name);

            if (workstation == null)
            {
                throw new NotFoundException("Estação de trabalho não encontrada!");
            }

            return new WorkstationResponseModel(workstation.Id, workstation.Name, workstation.Active, workstation.FloorId);
        }

        public async Task Update(string workstationName, WorkstationRequestModel requestModel)
        {
            var workstation = await _workstationRepository.GetByName(workstationName);

            if (workstation == null)
            {
                throw new NotFoundException("Estação de trabalho não encontrada!");
            }

            workstation.Update(requestModel.Active);

            workstation.ValidateEntity();

            _workstationRepository.Update(workstation);

            await _workstationRepository.Save();
        }
    }
}
