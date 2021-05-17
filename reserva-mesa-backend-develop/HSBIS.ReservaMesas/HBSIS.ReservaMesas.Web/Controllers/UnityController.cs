using HBSIS.ReservaMesas.Application.Services.Interfaces;
using HBSIS.ReservaMesas.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HBSIS.ReservaMesas.Web.Controllers
{
    [ApiController]
    [Route("api/units")]
    [Authorize]
    public class UnityController : ControllerBase
    {
        private readonly IUnityService _unityService;

        public UnityController(IUnityService unityService)
        {
            _unityService = unityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _unityService.GetAll());
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
    }
}
