using FluentAssertions;
using HBSIS.ReservaMesas.Domain.Entities;
using HBSIS.ReservaMesas.Domain.Interfaces.Repositories;
using HBSIS.ReservaMesas.Persistence.Context;
using HBSIS.ReservaMesas.Persistence.Repositories;
using HBSIS.ReservaMesas.Persistence.Seeds;
using HBSIS.ReservaMesas.UnitTests.Domain.EntitiesForTest;
using HBSIS.ReservaMesas.UnitTests.Helpers;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HBSIS.ReservaMesas.UnitTests.Persistence.Repositories
{
    [Collection("Non-Parallel Collection")]
    public class ReservationRepositoryTest
    {
        private readonly MainContext _dbContext;
        private readonly IReservationRepository _reservationRepository;
        private readonly IWorkstationRepository _workstationRepository;

        public ReservationRepositoryTest()
        {
            _dbContext = new MainContextHelper().CreateInMemoryMainContext();
            new InitialWorkstationSeed().SeedData(_dbContext);
            _reservationRepository = new ReservationRepository(_dbContext);
            _workstationRepository = new WorkstationRepository(_dbContext);
        }

        [Fact]
        public async Task Get_All_Reserved_Workstations_From_Date_Interval_And_Floor()
        {
            var reservedWorkstations = await _reservationRepository.GetAllReservedWorkstationsFromDateIntervalAndFloor(DateTime.Today, DateTime.Today.AddDays(2).Date, 2);

            reservedWorkstations.Should().NotBeNull();
        }

        [Fact]
        public async Task Verify_If_Workstation_Is_Already_Reserved()
        {
            var workstation = await _workstationRepository.GetById(2);

            var reservedWorkstations = await _reservationRepository.VerifyIfWorkstationIsAlreadyReserved(workstation, DateTime.Today, DateTime.Today.AddDays(2).Date);

            reservedWorkstations.Should().BeFalse();
        }

        [Fact]
        public async Task Get_User_Reservation_By_Id_And_By_UserId()
        {
            var reservation = await _reservationRepository.GetReservationByIdAndUserId(28, "eduardo.zortea@ambevtech.com.br");

            reservation.Should().Be(Arg.Any<Reservation>());
        }

        [Fact]
        public void Delete_Reservations()
        {
            var reservations = new List<Reservation>();
            reservations.Add(new ReservationForTest(1, "teste@hbsis.com.br", "teste",
                new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day), new Workstation("01-01-02-01", true, 1)));

            _reservationRepository.DeleteReservations(reservations);

            var ex = Record.Exception(() => _reservationRepository.DeleteReservations(reservations));
            ex.Should().BeNull();
        }

        [Fact]
        public async Task Get_All_Reservations_From_Date_Interval()
        {
            var reservations = await _reservationRepository.GetAllReservationsFromDateInterval(DateTime.Today, DateTime.Today.AddDays(2).Date, "eduardo.zortea@hbsis.com.br", 2);

            reservations.Should().NotBeNull();
        }

        [Fact]
        public async Task Get_All_Logged_User_Reservations()
        {
            var reservations = await _reservationRepository.CountUserReservationsByDateInterval("eduardo.zortea@hbsis.com.br", DateTime.Today, DateTime.Today.AddDays(3));

            reservations.Should().Be(Arg.Any<int>());
        }

        [Fact]
        public async Task Count_All_Reservations_From_Today()
        {
            var reservations = await _reservationRepository.CountAllReservationsFromToday("teste.mesas@ambevtech.com.br");

            reservations.Should().Be(Arg.Any<int>());
        }

        [Fact]
        public async Task Get_All_Active_Reservation_From_Today()
        {
            var exception = await Record.ExceptionAsync(() => _reservationRepository.GetAllActiveReservationFromToday(0, "teste"));

            exception.Should().BeNull();
        }

        [Fact]
        public async Task Get_All_Reserved_Workstations()
        {

            var reservedWorkstations = await _reservationRepository.GetAllReservedWorkstationsByDateInterval(DateTime.Today, DateTime.Today.AddDays(3));

            Assert.IsType<List<Reservation>>(reservedWorkstations);
            Assert.True(reservedWorkstations.Count() >= 0);
        }

        [Fact]
        public async Task Get_All_Confirmed_Workstations()
        {
            var reservedWorkstations = await _reservationRepository.GetAllConfirmedWorkstationsByDateInterval(DateTime.Today, DateTime.Today.AddDays(3));

            Assert.IsType<List<Reservation>>(reservedWorkstations);
            Assert.True(reservedWorkstations.Count() >= 0);
        }

        [Fact]
        public async Task Get_All_Reservations_By_Next_Day()
        {
            var reservations = await _reservationRepository.GetAllReservationsByNextDay();

            reservations.Should().NotBeNull();
        }

        [Fact]
        public async Task Get_All_Reservations_Not_Attended()
        {
            var reservations = await _reservationRepository.GetAllReservationsNotAttended();

            reservations.Should().NotBeNull();
        }
    }
}
