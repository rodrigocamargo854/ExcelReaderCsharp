using HBSIS.ReservaMesas.Application.Models.Floors;
using HBSIS.ReservaMesas.Application.Services.Interfaces;
using HBSIS.ReservaMesas.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HBSIS.ReservaMesas.Web.Controllers
{
    [ApiController]
    [Route("api/floors")]
    [Authorize]
    public class FloorController : ControllerBase
    {
        private readonly IFloorService _floorService;

        public FloorController(IFloorService floorService)
        {
            _floorService = floorService;
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] FloorRequestModel floor)
        {
            try
            {
                await _floorService.Update(id, floor);
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

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                return Ok(await _floorService.GetById(id));
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
        [Route("units/{id}")]
        public async Task<IActionResult> GetFloorsByUnityId([FromRoute] int id)
        {
            try
            {
                return Ok(await _floorService.GetFloorsByUnityId(id));
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

        [HttpGet]
        public async Task<IActionResult> GetByCode([FromQuery] string code)
        {
            try
            {
                return Ok(await _floorService.GetByCode(code));
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
    }
}
