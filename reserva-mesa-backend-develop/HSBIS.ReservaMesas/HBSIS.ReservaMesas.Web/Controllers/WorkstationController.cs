using System;
using System.Threading.Tasks;
using HBSIS.ReservaMesas.Application.Models.Workstations;
using HBSIS.ReservaMesas.Application.Services.Interfaces;
using HBSIS.ReservaMesas.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace HBSIS.ReservaMesas.Web.Controllers
{
    [ApiController]
    [Route("api/workstations")]
    public class WorkstationController : ControllerBase
    {
        private readonly IWorkstationService _workstationService;

        public WorkstationController(IWorkstationService workstationService)
        {
            _workstationService = workstationService;
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> GetByName([FromRoute] string name)
        {
            try
            {
                return Ok(await _workstationService.GetByName(name));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("inactives")]
        public async Task<IActionResult> GetInactivesByFloor([FromQuery(Name = "floorId")] int floorId)
        {
            try
            {
                return Ok(await _workstationService.GetInactivesByFloor(floorId));
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] WorkstationRequestModel workstation)
        {
            try
            {
                await _workstationService.Update(workstation.Name, workstation);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (CustomValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
