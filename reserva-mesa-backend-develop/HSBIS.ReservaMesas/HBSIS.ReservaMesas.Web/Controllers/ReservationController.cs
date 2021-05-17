using System;
using System.Threading.Tasks;
using HBSIS.ReservaMesas.Application.Models.Reservations;
using HBSIS.ReservaMesas.Application.Services.Interfaces;
using HBSIS.ReservaMesas.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HBSIS.ReservaMesas.Web.Controllers
{
    [Route("api/reservations")]
    [ApiController]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            this._reservationService = reservationService;
        }

        [HttpPut]
        [Route("Delete")]
        public async Task<IActionResult> DeleteUserReservations([FromBody] CancelReservationRequestModel cancelReservationRequestModel)
        {
            try
            {
                var userId = this.User.Identity.Name;
                await _reservationService.DeleteReservations(cancelReservationRequestModel, userId);
                return NoContent();
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
        [Route("workstations")]
        public async Task<IActionResult> GetAllReservedWorkstationsFromDateIntervalAndFloor([FromQuery(Name = "initialDate")] DateTime initialDate, [FromQuery(Name = "finalDate")] DateTime finalDate, [FromQuery(Name = "floorId")] int floorId)
        {
            try
            {
                return Ok(await _reservationService.GetAllReservedWorkstationsFromDateIntervalAndFloor(initialDate, finalDate, floorId));
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReservationRequestModel requestModel)
        {
            try
            {
                var userId = this.User.Identity.Name;
                var userName = this.User.FindFirst("name").Value;
                return Created("/api/reservations", await _reservationService.Create(requestModel, userId, userName));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (CustomValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UniqueKeyConstraintErrorException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservationsFromDateInterval([FromQuery(Name = "initialDate")] DateTime initialDate, [FromQuery(Name = "finalDate")] DateTime finalDate, [FromQuery(Name = "currentPage")] int currentPage)
        {
            try
            {
                var userId = this.User.Identity.Name;
                return Ok(await _reservationService.GetAllReservationsFromDateInterval(initialDate, finalDate, userId, currentPage));
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
        [Route("check-in")]
        public async Task<IActionResult> GetAllActiveReservationsFromToday([FromQuery(Name = "currentPage")] int currentPage, [FromQuery(Name = "nameFilter")] string nameFilter)
        {
            try
            {
                return Ok(await _reservationService.GetAllActiveReservationsFromToday(currentPage, nameFilter));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        [Route("check-in")]
        public async Task<IActionResult> CheckIn([FromQuery(Name = "reservationId")] int reservationId)
        {
            try
            {
                await _reservationService.CheckIn(reservationId);
                return Ok();
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

        [HttpPost]
        [Route("reports/reserved-workstations")]
        public async Task<IActionResult> GenerateReservedWorkstationsCsvReport([FromQuery(Name = "initialDate")] DateTime initialDate, [FromQuery(Name = "finalDate")] DateTime finalDate)
        {
            try
            {
                return File(await _reservationService.GetAllReserverdWorkstationsByDateInterval(initialDate, finalDate), "text/csv", "Workstations");
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

        [HttpPost]
        [Route("reports/confirmed-workstations")]
        public async Task<IActionResult> GenerateConfirmedWorkstationsCsvReport([FromQuery(Name = "initialDate")] DateTime initialDate, [FromQuery(Name = "finalDate")] DateTime finalDate)
        {
            try
            {
                return File(await _reservationService.GetAllConfirmedWorkstationsByDateInterval(initialDate, finalDate), "text/csv", "Workstations");
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
