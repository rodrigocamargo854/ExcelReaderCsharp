using FluentAssertions;
using HBSIS.ReservaMesas.Application.Models;
using HBSIS.ReservaMesas.Application.Models.Reservations;
using HBSIS.ReservaMesas.Application.Services;
using HBSIS.ReservaMesas.Application.Services.Interfaces;
using HBSIS.ReservaMesas.Domain.Entities;
using HBSIS.ReservaMesas.Domain.Exceptions;
using HBSIS.ReservaMesas.Domain.Interfaces.Repositories;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using HBSIS.ReservaMesas.UnitTests.Domain.EntitiesForTest;

namespace HBSIS.ReservaMesas.UnitTests.Application.Services
{
    public class ReservationServiceTest
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IReservationService _reservationService;
        private readonly IWorkstationRepository _workstationRepository;

        public ReservationServiceTest()
        {
            _workstationRepository = Substitute.For<IWorkstationRepository>();
            _reservationRepository = Substitute.For<IReservationRepository>();
            _reservationService = new ReservationService(_reservationRepository, _workstationRepository);
        }

        [Fact]
        public async Task Should_Get_All_Reserved_Workstations_From_Date_Interval_And_Floor()
        {
            List<Workstation> workstations = new List<Workstation>();
            workstations.Add(new Workstation("", true, 2));
            _reservationRepository.GetAllReservedWorkstationsFromDateIntervalAndFloor(
                Arg.Any<DateTime>(),
                Arg.Any<DateTime>(),
                Arg.Any<int>())
                .Returns(workstations);

            var reservedWorkstations = await _reservationService.GetAllReservedWorkstationsFromDateIntervalAndFloor(
                DateTime.Today,
                DateTime.Today.AddDays(3),
                2);

            reservedWorkstations.Should().NotBeNull();
            reservedWorkstations.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Should_Not_Get_All_Reserved_Workstations_From_Date_Interval_And_Floor_When_InitialDate_Is_In_The_Past()
        {
            List<Workstation> workstations = new List<Workstation>();
            workstations.Add(new Workstation("", true, 2));
            _reservationRepository.GetAllReservedWorkstationsFromDateIntervalAndFloor(
                Arg.Any<DateTime>(),
                Arg.Any<DateTime>(),
                Arg.Any<int>())
                .Returns(workstations);

            var ex = await Assert.ThrowsAsync<CustomValidationException>(async () =>
                   await _reservationService.GetAllReservedWorkstationsFromDateIntervalAndFloor(
            default(DateTime).AddYears(2),
            DateTime.Today.AddDays(3),
            2));

            ex.Message.Should().Contain("Data Inicial não pode estar no passado");
        }

        [Fact]
        public async Task Should_Get_All_Reservations_From_Date_Interval()
        {
            List<Reservation> reservations = new List<Reservation>();
            var reservation = new ReservationForTest(1, "eduardo.zortea@hbsis.com.br", "teste", new DateTime(2020, 12, 20, 0, 0, 0), new Workstation("teste", true, 2));
            reservations.Add(reservation);

            _reservationRepository.GetAllReservationsFromDateInterval(
                Arg.Any<DateTime>(),
                Arg.Any<DateTime>(),
                Arg.Any<string>(),
                Arg.Any<int>())
                .Returns(reservations);

            _reservationRepository.CountUserReservationsByDateInterval(Arg.Any<string>(), Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(10);

            var reservationModels = await _reservationService.GetAllReservationsFromDateInterval(
              DateTime.Today,
              DateTime.Today.AddDays(2),
              "eduardo.zortea@hbsis.com.br",
              0);

            reservationModels.Should().NotBeNull();
            reservationModels.Data.Should().NotBeEmpty();

            await _reservationRepository.Received(1).GetAllReservationsFromDateInterval(
              Arg.Any<DateTime>(),
              Arg.Any<DateTime>(),
              Arg.Any<string>(),
              Arg.Any<int>());
        }

        [Fact]
        public async Task Should_Not_Get_All_Reservations_From_Date_Interval_When_InitialDate_Is_Default()
        {
            List<Reservation> reservations = new List<Reservation>();
            var reservation = new ReservationForTest(1, "eduardo.zortea@hbsis.com.br", "teste", new DateTime(2020, 12, 20, 0, 0, 0), new Workstation("teste", true, 2));
            reservations.Add(reservation);

            _reservationRepository.GetAllReservationsFromDateInterval(
                Arg.Any<DateTime>(),
                Arg.Any<DateTime>(),
                Arg.Any<string>(),
                Arg.Any<int>())
                .Returns(reservations);

            _reservationRepository.CountUserReservationsByDateInterval(Arg.Any<string>(), Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(10);

            var ex = await Assert.ThrowsAsync<CustomValidationException>(async () =>
                 await _reservationService.GetAllReservationsFromDateInterval(
              default(DateTime),
              DateTime.Today.AddDays(2),
              "eduardo.zortea@hbsis.com.br",
              0));

            ex.Message.Should().Contain("Data Inicial deve ser informada");
        }

        [Fact]
        public async Task Should_Not_Get_All_Reservations_From_Date_Interval_When_FinialDate_Is_Default()
        {
            List<Reservation> reservations = new List<Reservation>();
            var reservation = new ReservationForTest(1, "eduardo.zortea@hbsis.com.br", "teste", new DateTime(2020, 12, 20, 0, 0, 0), new Workstation("teste", true, 2));
            reservations.Add(reservation);

            _reservationRepository.GetAllReservationsFromDateInterval(
                Arg.Any<DateTime>(),
                Arg.Any<DateTime>(),
                Arg.Any<string>(),
                Arg.Any<int>())
                .Returns(reservations);

            _reservationRepository.CountUserReservationsByDateInterval(Arg.Any<string>(), Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(10);

            var ex = await Assert.ThrowsAsync<CustomValidationException>(async () =>
                 await _reservationService.GetAllReservationsFromDateInterval(
              DateTime.Today,
              default(DateTime),
              "eduardo.zortea@hbsis.com.br",
              0));

            ex.Message.Should().Contain("Data Final deve ser informada");
        }

        [Fact]
        public async Task Should_Not_Get_All_Reservations_From_Date_Interval_When_InitialDate_Is_Greater_Than_The_Allowed_Date()
        {
            List<Reservation> reservations = new List<Reservation>();
            var reservation = new ReservationForTest(1, "eduardo.zortea@hbsis.com.br", "teste", new DateTime(2020, 12, 20, 0, 0, 0), new Workstation("teste", true, 2));
            reservations.Add(reservation);

            _reservationRepository.GetAllReservationsFromDateInterval(
                Arg.Any<DateTime>(),
                Arg.Any<DateTime>(),
                Arg.Any<string>(),
                Arg.Any<int>())
                .Returns(reservations);

            _reservationRepository.CountUserReservationsByDateInterval(Arg.Any<string>(), Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(10);

            var ex = await Assert.ThrowsAsync<CustomValidationException>(async () =>
                 await _reservationService.GetAllReservationsFromDateInterval(
              DateTime.Today.AddDays(20),
              DateTime.Today.AddDays(2),
              "eduardo.zortea@hbsis.com.br",
              0));

            ex.Message.Should().Contain("Data Inicial não pode estar a mais de 2 semanas no futuro");
        }

        [Fact]
        public async Task Should_Not_Get_All_Reservations_From_Date_Interval_When_InitialDate_Is_Greater_Than_The_FinalDate()
        {
            List<Reservation> reservations = new List<Reservation>();
            var reservation = new ReservationForTest(1, "eduardo.zortea@hbsis.com.br", "teste", new DateTime(2020, 12, 20, 0, 0, 0), new Workstation("teste", true, 2));
            reservations.Add(reservation);

            _reservationRepository.GetAllReservationsFromDateInterval(
                Arg.Any<DateTime>(),
                Arg.Any<DateTime>(),
                Arg.Any<string>(),
                Arg.Any<int>())
                .Returns(reservations);

            _reservationRepository.CountUserReservationsByDateInterval(Arg.Any<string>(), Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(10);

            var ex = await Assert.ThrowsAsync<CustomValidationException>(async () =>
                 await _reservationService.GetAllReservationsFromDateInterval(
              DateTime.Today.AddDays(2),
              DateTime.Today,
              "eduardo.zortea@hbsis.com.br",
              0));

            ex.Message.Should().Contain("Data Inicial não pode ser maior que Data Final");
        }

        [Fact]
        public async Task Should_Not_Get_All_Reserved_Workstations_From_Date_Interval_And_Floor()
        {
            List<Workstation> workstations = new List<Workstation>();
            _reservationRepository.GetAllReservedWorkstationsFromDateIntervalAndFloor(
                Arg.Any<DateTime>(),
                Arg.Any<DateTime>(),
                Arg.Any<int>())
                .Returns(workstations);

            var reservedWorkstations = await _reservationService.GetAllReservedWorkstationsFromDateIntervalAndFloor(
                DateTime.Today,
                DateTime.Today.AddDays(3), 2);

            reservedWorkstations.Should().NotBeNull();
            reservedWorkstations.Should().BeEmpty();
        }

        [Fact]
        public async Task Should_Generate_Report()
        {
            var reservations = new List<Reservation>();
            _reservationRepository.GetAllReservedWorkstationsByDateInterval(Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(reservations);

            var data = await _reservationService.GetAllReserverdWorkstationsByDateInterval(DateTime.Today, DateTime.Today.AddDays(3));

            await _reservationRepository.Received(1).GetAllReservedWorkstationsByDateInterval(Arg.Any<DateTime>(), Arg.Any<DateTime>());
            Assert.NotNull(data);
        }

        [Fact]
        public async Task Should_Not_Generate_Report_When_InitialDate_Is_Wrong()
        {
            var reservations = new List<Reservation>();
            _reservationRepository.GetAllReservedWorkstationsByDateInterval(Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(reservations);

            var ex = await Assert.ThrowsAsync<CustomValidationException>(async () =>
                await _reservationService.GetAllReserverdWorkstationsByDateInterval(default(DateTime), DateTime.Today.AddDays(3)));

            ex.Message.Should().Contain("Data Inicial deve ser informada");
        }

        [Fact]
        public async Task Should_Generate_Report_With_ReportModel()
        {
            var reservations = new List<Reservation>();

            _reservationRepository.GetAllReservedWorkstationsByDateInterval(Arg.Any<DateTime>(), Arg.Any<DateTime>())
                 .Returns(reservations);

            var data = await _reservationService.GetAllReserverdWorkstationsByDateInterval(DateTime.Today, DateTime.Today.AddDays(3));

            reservations.Select(reservation =>
                                  new CsvReportModel(reservation.Date, reservation.Workstation.Floor.Name, reservation.Workstation.Name, reservation.UserId, "teste"));

            await _reservationRepository.Received(1).GetAllReservedWorkstationsByDateInterval(Arg.Any<DateTime>(), Arg.Any<DateTime>());
            Assert.NotNull(data);
        }

        [Fact]
        public async Task Should_Not_Generate_Report_When_FinalDate_Is_Wrong()
        {
            var reservations = new List<Reservation>();
            _reservationRepository.GetAllReservedWorkstationsByDateInterval(Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(reservations);

            var ex = await Assert.ThrowsAsync<CustomValidationException>(async () =>
                await _reservationService.GetAllReserverdWorkstationsByDateInterval(DateTime.Today, default(DateTime)));

            ex.Message.Should().Contain("Data Final deve ser informada");
        }

        [Fact]
        public async Task Should_Not_Generate_Report_When_InitialDate_Is_Greater_Than_FinalDate()
        {
            var reservations = new List<Reservation>();
            _reservationRepository.GetAllReservedWorkstationsByDateInterval(Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(reservations);

            var ex = await Assert.ThrowsAsync<CustomValidationException>(async () =>
                await _reservationService.GetAllReserverdWorkstationsByDateInterval(DateTime.Today.AddDays(2), DateTime.Today));

            ex.Message.Should().Contain("Data Final não pode ser anterior a data inicial");
        }

        [Fact]
        public async Task Should_Generate_Confirmed_Workstations_Report()
        {
            var reservations = new List<Reservation>();
            _reservationRepository.GetAllConfirmedWorkstationsByDateInterval(Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(reservations);

            var data = await _reservationService.GetAllConfirmedWorkstationsByDateInterval(DateTime.Today, DateTime.Today.AddDays(3));

            await _reservationRepository.Received(1).GetAllConfirmedWorkstationsByDateInterval(Arg.Any<DateTime>(), Arg.Any<DateTime>());
            Assert.NotNull(data);
        }

        [Fact]
        public async Task Should_Not_Generate_Confirmed_Workstations_Report_When_InitialDate_Is_Wrong()
        {
            var reservations = new List<Reservation>();
            _reservationRepository.GetAllConfirmedWorkstationsByDateInterval(Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(reservations);

            var ex = await Assert.ThrowsAsync<CustomValidationException>(async () =>
                await _reservationService.GetAllConfirmedWorkstationsByDateInterval(default(DateTime), DateTime.Today.AddDays(3)));

            ex.Message.Should().Contain("Data Inicial deve ser informada");
        }

        [Fact]
        public async Task Should_Not_Generate_Confirmed_Workstations_Report_When_FinalDate_Is_Wrong()
        {
            var reservations = new List<Reservation>();
            _reservationRepository.GetAllConfirmedWorkstationsByDateInterval(Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(reservations);

            var ex = await Assert.ThrowsAsync<CustomValidationException>(async () =>
                await _reservationService.GetAllConfirmedWorkstationsByDateInterval(DateTime.Today, default(DateTime)));

            ex.Message.Should().Contain("Data Final deve ser informada");
        }

        [Fact]
        public async Task Should_Not_Generate_Confirmed_Workstations_Report_When_InitialDate_Is_Greater_Than_FinalDate()
        {
            var reservations = new List<Reservation>();
            _reservationRepository.GetAllConfirmedWorkstationsByDateInterval(Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(reservations);

            var ex = await Assert.ThrowsAsync<CustomValidationException>(async () =>
                await _reservationService.GetAllConfirmedWorkstationsByDateInterval(DateTime.Today.AddDays(2), DateTime.Today));

            ex.Message.Should().Contain("Data Final não pode ser anterior a data inicial");
        }

        [Fact]
        public async Task Should_Create_Reservation_When_Is_Called()
        {
            var requestModel = new ReservationRequestModel()
            {
                WorkstationName = "01-02-01-02",
                InitialDate = DateTime.Today,
                FinalDate = DateTime.Today.AddDays(2)

            };

            var workstation = new Workstation("01-02-01-02", true, 2);

            _workstationRepository.GetByName(Arg.Any<string>()).Returns(workstation);

            var reservationReturned = await _reservationService.Create(requestModel, "kaue.salvio@hbsis.com.br", "teste");

            reservationReturned.Should().NotBeNull();
            reservationReturned.Should().HaveCount(3);

            await _reservationRepository.Received(3).Create(Arg.Is<Reservation>(r => r.WorkstationId == workstation.Id));
        }

        [Fact]
        public async Task Should_Not_Create_Reservation_When_Workstation_Is_Already_Reserved()
        {
            var requestModel = new ReservationRequestModel()
            {
                WorkstationName = "01-02-01-02",
                InitialDate = DateTime.Today,
                FinalDate = DateTime.Today.AddDays(2)
            };

            var workstation = new Workstation("01-02-01-02", true, 2);

            _workstationRepository.GetByName(Arg.Any<string>()).Returns(workstation);
            _reservationRepository.VerifyIfWorkstationIsAlreadyReserved(workstation, DateTime.Today, DateTime.Today.AddDays(2)).Returns(true);

          var ex = await Assert.ThrowsAsync<CustomValidationException>(async () =>
              await _reservationService.Create(requestModel, "teste@hbsis.com.br", "teste"));

            ex.Message.Should().Contain("A estação de trabalho informada já foi reservada neste intervalo de tempo!");
        }

        [Fact]
        public async Task Should_Not_Create_Reservation_When_Workstation_Is_Already_Reserved_On_That_Time()
        {
            var requestModel = new ReservationRequestModel()
            {
                WorkstationName = "01-02-01-02",
                InitialDate = DateTime.Today,
                FinalDate = DateTime.Today.AddDays(2)
            };

            var workstation = new Workstation("01-02-01-02", true, 2);

            var innerException = new Exception("IX_Reservation_WorkstationId_Date");

            _workstationRepository.GetByName(Arg.Any<string>()).Returns(workstation);
            _reservationRepository.VerifyIfWorkstationIsAlreadyReserved(workstation, DateTime.Today, DateTime.Today.AddDays(2)).Returns(false);
            _reservationRepository.Save().Should().Throws(new Exception("test", innerException));

            var ex = await Assert.ThrowsAsync<UniqueKeyConstraintErrorException>(async () =>
                await _reservationService.Create(requestModel, "teste@hbsis.com.br", "teste"));

            ex.Message.Should().Contain("A estação de trabalho informada já foi reservada neste intervalo de tempo!");
        }

        [Fact]
        public async Task Should_Not_Create_Reservation_When_Workstation_NotFound()
        {
            var requestModel = new ReservationRequestModel()
            {
              WorkstationName = "01-02-01-02",
              InitialDate = DateTime.Today,
              FinalDate = DateTime.Today.AddDays(2)
            };

            var ex = await Assert.ThrowsAsync<NotFoundException>(async () =>
                await _reservationService.Create(requestModel, "teste@ambevtech.com.br", "test"));

            ex.Message.Should().Contain("Estação de trabalho não encontrada.");
        }

        [Fact]
        public async Task Should_Not_Get_Create_Reservation_When_FinalDate_Is_Greater_Than_The_Allowed_Date()
        {
            var requestModel = new ReservationRequestModel()
            {
              WorkstationName = "01-02-01-02",
              InitialDate = DateTime.Today,
              FinalDate = DateTime.Today.AddDays(40)
            };

            var workstation = new Workstation("01-02-01-02", true, 2);

            _workstationRepository.GetByName(Arg.Any<string>()).Returns(workstation);
            _reservationRepository.VerifyIfWorkstationIsAlreadyReserved(workstation, DateTime.Today, DateTime.Today.AddDays(2)).Returns(true);

            var ex = await Assert.ThrowsAsync<CustomValidationException>(async () =>
                  await _reservationService.Create(requestModel, "teste@ambevtech.com.br", "test"));

            ex.Message.Should().Contain("Data Final não pode estar a mais de 2 semanas no futuro");
        }

      [Fact]
        public async Task Should_Delete_Reservations()
        {
            var reservationIds = new List<int>();
            reservationIds.Add(1);

            var reservation = new ReservationForTest(1, "teste@hbsis.com.br", "teste", new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day),
              new Workstation("01-01-02-01", true, 1));

            _reservationRepository.GetReservationByIdAndUserId(Arg.Any<int>(), Arg.Any<string>()).Returns(reservation);

            await _reservationService.DeleteReservations(new CancelReservationRequestModel() { ReservationIds = reservationIds }, "teste@hbsis.com.br");

            _reservationRepository.Received(1).DeleteReservations(Arg.Any<List<Reservation>>());
        }

        [Fact]
        public async Task Should_Not_Delete_Reservations_When_Reservation_Is_null()
        {
            var reservationIds = new List<int>();
            reservationIds.Add(1);

            _reservationRepository.GetReservationByIdAndUserId(Arg.Any<int>(), Arg.Any<string>()).ReturnsNull();

            var response = await Assert.ThrowsAnyAsync<NotFoundException>(async () => await _reservationService
                .DeleteReservations(new CancelReservationRequestModel() { ReservationIds = reservationIds }, "teste@hbsis.com.br"));
            response.Message.Should().Contain("Reserva não existe ou é de um dia anterior ao dia de hoje.");
        }

        [Fact]
        public async Task Should_Not_Delete_Reservations_When_Date_Is_Less_Than_Today()
        {
            var reservationIds = new List<int>();
            reservationIds.Add(1);

            var reservation = new ReservationForTest(1, "teste@hbsis.com.br", "teste", new DateTime(),
              new Workstation("01-01-02-01", true, 1));

            _reservationRepository.GetReservationByIdAndUserId(Arg.Any<int>(), Arg.Any<string>()).Returns(reservation);

            var response = await Assert.ThrowsAnyAsync<NotFoundException>(async () => await _reservationService
                    .DeleteReservations(new CancelReservationRequestModel() { ReservationIds = reservationIds }, "teste@hbsis.com.br"));
            response.Message.Should().Contain("Reserva não existe ou é de um dia anterior ao dia de hoje.");
        }

        [Fact]
        public async Task Should_Not_Delete_Reservations_When_ReservationIds_Is_Null()
        {
            var reservation = new ReservationForTest(1, "teste@hbsis.com.br", "test", new DateTime(),
              new Workstation("01-01-02-01", true, 1));

            _reservationRepository.GetReservationByIdAndUserId(Arg.Any<int>(), Arg.Any<string>()).Returns(reservation);

            var response = await Assert.ThrowsAnyAsync<CustomValidationException>(async () => await _reservationService
                    .DeleteReservations(new CancelReservationRequestModel() { ReservationIds = null }, "teste@hbsis.com.br"));
            response.Message.Should().Contain("Lista de IDs vazia ou inexistente.");
        }

        [Fact]
        public async Task Should_Not_Delete_Reservations_When_ReservationIds_Is_Empty()
        {
            var reservationIds = new List<int>();

            var reservation = new ReservationForTest(1, "teste@hbsis.com.br", "test", new DateTime(),
              new Workstation("01-01-02-01", true, 1));

            _reservationRepository.GetReservationByIdAndUserId(Arg.Any<int>(), Arg.Any<string>()).Returns(reservation);

            var response = await Assert.ThrowsAnyAsync<CustomValidationException>(async () => await _reservationService
                    .DeleteReservations(new CancelReservationRequestModel() { ReservationIds = reservationIds }, "teste@hbsis.com.br"));
            response.Message.Should().Contain("Lista de IDs vazia ou inexistente.");
        }

        [Fact]
        public async Task Should_Get_All_Active_Reservations_From_Today()
        {
            var reservations = new List<Reservation>();
            var reservation = new ReservationForTest(1, "eduardo.zortea@hbsis.com.br", "teste", new DateTime(2020, 12, 20, 0, 0, 0), new Workstation("teste", true, 2));
            reservations.Add(reservation);

            _reservationRepository.GetAllActiveReservationFromToday(Arg.Any<int>(), Arg.Any<string>()).Returns(reservations);

            var result = await _reservationService.GetAllActiveReservationsFromToday(0, "teste");

            result.Data.Should().NotBeEmpty().And.HaveCount(1);
        }

        [Fact]
        public void Should_Instance_Check_In_Response_Model()
        {
            var model = new CheckInResponseModel(0, "teste", true, "teste");
            model.ReservationId.Should().Be(0);
            model.UserName.Should().Be("teste");
            model.CheckInStatus.Should().BeTrue();
            model.WorkstationName.Should().Be("teste");
        }

        [Fact]
        public async Task Should_Check_in()
        {
            var reservation = new Reservation(0, DateTime.Now, "test.mesas@ambevtech.com.br", "teste");

            _reservationRepository.GetById(Arg.Any<int>()).Returns(reservation);

            await _reservationService.CheckIn(3);
        }

        [Fact]
        public async Task Should_Not_Check_in_When_Reservation_Is_Not_today()
        {
            var reservation = new Reservation(0, DateTime.Now.AddDays(4), "test.mesas@ambevtech.com.br", "teste");

            _reservationRepository.GetById(Arg.Any<int>()).Returns(reservation);

            var ex = await Assert.ThrowsAnyAsync<CustomValidationException>(async () =>
            await _reservationService.CheckIn(3));

            ex.Message.Should().Contain("Check-in pode ser feito somente com reservas do dia atual");
        }

        [Fact]
        public async Task Should_Not_Check_in_When_Reservation_Is_Not_Found()
        {
            var ex = await Assert.ThrowsAnyAsync<CustomValidationException>(async () =>
            await _reservationService.CheckIn(3));

            ex.Message.Should().Contain("Reservation solicitada não existe");
        }
    }
}
