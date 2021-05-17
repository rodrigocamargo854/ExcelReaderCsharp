using System.Security.Claims;
using FluentAssertions;
using HBSIS.ReservaMesas.Application.Models;
using HBSIS.ReservaMesas.Application.Models.Reservations;
using HBSIS.ReservaMesas.Application.Services.Interfaces;
using HBSIS.ReservaMesas.Domain.Exceptions;
using HBSIS.ReservaMesas.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace HBSIS.ReservaMesas.UnitTests.Web.Controllers
{
    public class ReservationControllerTest
    {
        private readonly IReservationService _reservationService;
        private readonly ReservationController _reservationController;

        public ReservationControllerTest()
        {
            _reservationService = Substitute.For<IReservationService>();
            _reservationController = new ReservationController(_reservationService)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Substitute.For<HttpContext>()
                }
            };
        }

        [Fact]
        public async Task If_Get_All_Reserved_Workstations_From_Date_Interval_And_Floor_Return_Ok()
        {
            var reservedWorkstations = new List<string>();
            reservedWorkstations.Add("01-02-01-02");
            _reservationService.GetAllReservedWorkstationsFromDateIntervalAndFloor(Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Any<int>()).Returns(reservedWorkstations);

            var response = await _reservationController.GetAllReservedWorkstationsFromDateIntervalAndFloor(DateTime.Today, DateTime.Today.AddDays(3), 2);

            await _reservationService.Received(1).GetAllReservedWorkstationsFromDateIntervalAndFloor(Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Any<int>());
            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public async Task If_Create_Return_Created()
        {
            _reservationController.HttpContext.User.Identity.Name.Returns("teste@hbsis.com.br");
            _reservationController.HttpContext.User.FindFirst("name").Returns(new Claim("name", "teste"));

            var requestModel = new ReservationRequestModel()
            {
                WorkstationName = "01-02-01-02",
                InitialDate = DateTime.Today,
                FinalDate = DateTime.Today.AddDays(2)

            };

            var reservedWorkstations = new List<int>();
            reservedWorkstations.Add(1);

            _reservationService.Create(Arg.Any<ReservationRequestModel>(), Arg.Any<string>(), Arg.Any<string>()).Returns(reservedWorkstations);

            var response = await _reservationController.Create(requestModel);

            await _reservationService.Received(1).Create(Arg.Any<ReservationRequestModel>(), Arg.Any<string>(), Arg.Any<string>());
            Assert.IsType<CreatedResult>(response);
        }

        [Fact]
        public async Task If_Not_Create_Return_Not_Found()
        {
            _reservationController.HttpContext.User.Identity.Name.Returns("teste@hbsis.com.br");
            _reservationController.HttpContext.User.FindFirst("name").Returns(new Claim("name", "teste"));

            var requestModel = new ReservationRequestModel()
            {
              WorkstationName = "01-02-01-02",
              InitialDate = DateTime.Today,
              FinalDate = DateTime.Today.AddDays(2)

            };

            var reservedWorkstations = new List<int>();
            reservedWorkstations.Add(1);

            _reservationService
                .When(x => x.Create(requestModel, "teste@hbsis.com.br", "teste"))
                .Throw(new NotFoundException());

            var response = await _reservationController.Create(requestModel);

            Assert.IsType<NotFoundObjectResult>(response);
        }

        [Fact]
        public async Task If_Not_Create_Return_Bad_Request_For_Custom_Validation_Exception()
        {
          _reservationController.HttpContext.User.Identity.Name.Returns("teste@hbsis.com.br");
          _reservationController.HttpContext.User.FindFirst("name").Returns(new Claim("name", "teste"));

            var requestModel = new ReservationRequestModel()
          {
            WorkstationName = "01-02-01-02",
            InitialDate = DateTime.Today,
            FinalDate = DateTime.Today.AddDays(2)

          };

          var reservedWorkstations = new List<int>();
          reservedWorkstations.Add(1);

          _reservationService
              .When(x => x.Create(requestModel, "teste@hbsis.com.br", "teste"))
              .Throw(new CustomValidationException());

          var response = await _reservationController.Create(requestModel);

          Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task If_Not_Create_Return_Bad_Request_For_Unique_Key_Constraint_Error_Exception()
        {
          _reservationController.HttpContext.User.Identity.Name.Returns("teste@hbsis.com.br");
          _reservationController.HttpContext.User.FindFirst("name").Returns(new Claim("name", "teste"));

            var requestModel = new ReservationRequestModel()
          {
            WorkstationName = "01-02-01-02",
            InitialDate = DateTime.Today,
            FinalDate = DateTime.Today.AddDays(2)

          };

          var reservedWorkstations = new List<int>();
          reservedWorkstations.Add(1);

          _reservationService
              .When(x => x.Create(requestModel, "teste@hbsis.com.br", "teste"))
              .Throw(new UniqueKeyConstraintErrorException("erro"));

          var response = await _reservationController.Create(requestModel);

          Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task If_Not_Create_Return_Status_Code()
        {
            _reservationController.HttpContext.User.Identity.Name.Returns("teste@hbsis.com.br");

            var requestModel = new ReservationRequestModel()
            {
              WorkstationName = "01-02-01-02",
              InitialDate = DateTime.Today,
              FinalDate = DateTime.Today.AddDays(2)

            };

            var reservedWorkstations = new List<int>();
            reservedWorkstations.Add(1);

            _reservationService
                .When(x => x.Create(requestModel, "teste@hbsis.com.br", "teste"))
                .Throw(new Exception());

            var response = await _reservationController.Create(requestModel);

            Assert.IsType<StatusCodeResult>(response);
        }

        [Fact]
        public async Task If_Delete_User_Reservations_Return_Ok()
        {
            var reservationIds = new List<int>();
            reservationIds.Add(1);

            _reservationController.HttpContext.User.Identity.Name.Returns("eduardo.zortea@hbsis.com.br");

            var response =
              await _reservationController.DeleteUserReservations(new CancelReservationRequestModel()
              {
                ReservationIds = reservationIds
              });

            await _reservationService.Received(1).DeleteReservations(Arg.Any<CancelReservationRequestModel>(), Arg.Any<string>());
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public async Task If_Delete_User_Reservations_Return_NotFound()
        {
            var reservationIds = new List<int>();
            reservationIds.Add(1);

            _reservationService
                .When(x => x.DeleteReservations(Arg.Any<CancelReservationRequestModel>(), Arg.Any<string>()))
                .Throw(new NotFoundException());

            var response =
              await _reservationController.DeleteUserReservations(new CancelReservationRequestModel()
              {
                ReservationIds = reservationIds
              });

            Assert.IsType<NotFoundObjectResult>(response);
        }

        [Fact]
        public async Task If_Delete_User_Reservations_Return_BadRequest()
        {
          var reservationIds = new List<int>();
          reservationIds.Add(1);

          _reservationService
              .When(x => x.DeleteReservations(Arg.Any<CancelReservationRequestModel>(), Arg.Any<string>()))
              .Throw(new CustomValidationException());

          var response =
            await _reservationController.DeleteUserReservations(new CancelReservationRequestModel()
            {
              ReservationIds = reservationIds
            });

          Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task If_Delete_User_Reservations_Return_StatusCode()
        {
            var reservationIds = new List<int>();
            reservationIds.Add(1);

            _reservationService
                .When(x => x.DeleteReservations(Arg.Any<CancelReservationRequestModel>(), Arg.Any<string>()))
                .Throw(new Exception());

            var response =
              await _reservationController.DeleteUserReservations(new CancelReservationRequestModel()
              {
                ReservationIds = Arg.Any<List<int>>()
              });

            Assert.IsType<StatusCodeResult>(response);
        }


        [Fact]
        public async Task If_Get_All_Reserved_Workstations_Return_NotFound()
        {
            _reservationController.HttpContext.User.Identity.Name.Returns("eduardo.zortea@hbsis.com.br");

            _reservationService
               .When(x => x.GetAllReservedWorkstationsFromDateIntervalAndFloor(Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Any<int>()))
               .Do(x => x.Throws(new NotFoundException()));

            var response = await _reservationController.GetAllReservedWorkstationsFromDateIntervalAndFloor(DateTime.Today, DateTime.Today.AddDays(3), 2);

            Assert.IsType<NotFoundObjectResult>(response);
        }

        [Fact]
        public async Task If_Get_All_Reserved_Workstations_With_Error_Return_BadRequest()
        {
            _reservationController.HttpContext.User.Identity.Name.Returns("eduardo.zortea@hbsis.com.br");

            _reservationService
               .When(x => x.GetAllReservedWorkstationsFromDateIntervalAndFloor(Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Any<int>()))
               .Do(x => x.Throws(new CustomValidationException()));

            var response = await _reservationController.GetAllReservedWorkstationsFromDateIntervalAndFloor(DateTime.Today, DateTime.Today.AddDays(3), 2);

            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task If_Get_All_Reserved_Workstations_With_Server_Error_Return_StatusCode()
        {
            _reservationController.HttpContext.User.Identity.Name.Returns("eduardo.zortea@hbsis.com.br");

            _reservationService
               .When(x => x.GetAllReservedWorkstationsFromDateIntervalAndFloor(Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Any<int>()))
               .Do(x => x.Throws(new Exception()));

            var response = await _reservationController.GetAllReservedWorkstationsFromDateIntervalAndFloor(DateTime.Today, DateTime.Today.AddDays(3), 2);

            Assert.IsType<StatusCodeResult>(response);
        }

        [Fact]
        public async Task If_Get_All_Reservations_Return_Ok()
        {
            var reservations = new List<ReservationResponseModel>();
            var reservation = new ReservationResponseModel("eduardo.zortea@hbsis.com.br", new DateTime(2020, 12, 05, 0, 0, 0), 1);
            reservations.Add(reservation);

            _reservationController.HttpContext.User.Identity.Name.Returns("eduardo.zortea@hbsis.com.br");

            var pageResponseModel = new PageResponseModel<ReservationResponseModel>(0, 10, reservations);

            _reservationService.GetAllReservationsFromDateInterval(Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Any<string>(), Arg.Any<int>())
                     .Returns(pageResponseModel);

            var response = await _reservationController.GetAllReservationsFromDateInterval(DateTime.Today, DateTime.Today.AddDays(3), 0);

            await _reservationService.Received(1).GetAllReservationsFromDateInterval(Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Any<string>(), Arg.Any<int>());

            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task If_Get_All_Reservations_With_Error_Return_BadRequest()
        {
            _reservationController.HttpContext.User.Identity.Name.Returns("eduardo.zortea@hbsis.com.br");

            _reservationService
               .When(x => x.GetAllReservationsFromDateInterval(Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Any<string>(), Arg.Any<int>()))
               .Do(x => x.Throws(new CustomValidationException()));

            var response = await _reservationController.GetAllReservationsFromDateInterval(DateTime.Today, DateTime.Today.AddDays(3), 0);

            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task If_Get_All_Reservations_With_Server_Error_Return_StatusCode()
        {
            _reservationController.HttpContext.User.Identity.Name.Returns("eduardo.zortea@hbsis.com.br");

            _reservationService
               .When(x => x.GetAllReservationsFromDateInterval(Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Any<string>(), Arg.Any<int>()))
               .Do(x => x.Throws(new Exception()));

            var response = await _reservationController.GetAllReservationsFromDateInterval(DateTime.Today, DateTime.Today.AddDays(3), 0);

            Assert.IsType<StatusCodeResult>(response);
        }

        [Fact]
        public async Task If_Get_All_Active_Reservations_From_Today_Return_Ok() 
        {
            var reservations = new List<CheckInResponseModel>();
            reservations.Add(new CheckInResponseModel(0, "test", true, "test"));
            var pagedReservations = new PageResponseModel<CheckInResponseModel>(0, 1, reservations);
            _reservationService.GetAllActiveReservationsFromToday(Arg.Any<int>(), Arg.Any<string>()).Returns(pagedReservations);

            var response = await _reservationController.GetAllActiveReservationsFromToday(0, "test");

            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task If_Get_All_Active_Reservation_From_Today_With_Server_Error_Return_StatusCode() 
        {
            _reservationService.When(x => x.GetAllActiveReservationsFromToday(Arg.Any<int>(), Arg.Any<string>()))
                .Do(x => x.Throws(new Exception()));

            var response = await _reservationController.GetAllActiveReservationsFromToday(0, "test");

            Assert.IsType<StatusCodeResult>(response);
        }

        [Fact]
        public async Task If_Check_In_Return_Ok()
        {
            Exception ex = await Record.ExceptionAsync(async () => await _reservationController.CheckIn(3));

            ex.Should().BeNull();
        }

        [Fact]
        public async Task If_Check_In_With_Invalid_Date_Return_Bad_Request()
        {
            _reservationService.When(x => x.CheckIn(Arg.Any<int>())).Do(x => x.Throws(new CustomValidationException("Check-in pode ser feito somente com reservas do dia atual")));

            var response = await _reservationController.CheckIn(3);
        }
      
        [Fact]
        public async Task If_Generate_Report_Return_File()
        {
            var data = await _reservationController.GenerateReservedWorkstationsCsvReport(Arg.Any<DateTime>(), Arg.Any<DateTime>());

            Assert.IsType<FileContentResult>(data);
        }

        [Fact]
        public async Task If_Not_Generate_Report_Should_Return_BadRequest()
        {
            _reservationService
              .When(x => x.GetAllReserverdWorkstationsByDateInterval(Arg.Any<DateTime>(), Arg.Any<DateTime>()))
              .Do(x => x.Throws(new CustomValidationException()));

            var response = await _reservationController.GenerateReservedWorkstationsCsvReport(DateTime.Today, DateTime.Today.AddDays(3));

            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task If_Not_Generate_Report_Should_Return_StatusCode()
        {
            _reservationService
              .When(x => x.GetAllReserverdWorkstationsByDateInterval(Arg.Any<DateTime>(), Arg.Any<DateTime>()))
              .Do(x => x.Throws(new Exception()));

            var response = await _reservationController.GenerateReservedWorkstationsCsvReport(DateTime.Today, DateTime.Today.AddDays(3));

            Assert.IsType<StatusCodeResult>(response);
        }

        [Fact]
        public async Task If_Generate_Confirmed_Workstations_Report_Return_File()
        {
            var data = await _reservationController.GenerateConfirmedWorkstationsCsvReport(Arg.Any<DateTime>(), Arg.Any<DateTime>());

            Assert.IsType<FileContentResult>(data);
        }

        [Fact]
        public async Task If_Not_Generate_Confirmed_Workstations_Report_Should_Return_BadRequest()
        {
            _reservationService
              .When(x => x.GetAllConfirmedWorkstationsByDateInterval(Arg.Any<DateTime>(), Arg.Any<DateTime>()))
              .Do(x => x.Throws(new CustomValidationException()));

            var response = await _reservationController.GenerateConfirmedWorkstationsCsvReport(DateTime.Today, DateTime.Today.AddDays(3));

            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task If_Check_In_With_Server_Error_Return_StatusCode()
        {
            _reservationService.When(x => x.CheckIn(Arg.Any<int>())).Do(x => x.Throws(new Exception()));

            var response = await _reservationController.CheckIn(3);
        }
      
        [Fact]    
        public async Task If_Not_Generate_Confirmed_Workstations_Report_Should_Return_StatusCode()
        {
            _reservationService
              .When(x => x.GetAllConfirmedWorkstationsByDateInterval(Arg.Any<DateTime>(), Arg.Any<DateTime>()))
              .Do(x => x.Throws(new Exception()));

            var response = await _reservationController.GenerateConfirmedWorkstationsCsvReport(DateTime.Today, DateTime.Today.AddDays(3));

            Assert.IsType<StatusCodeResult>(response);
        }
    }
}
