using FluentAssertions;
using HBSIS.ReservaMesas.Application.Emails;
using HBSIS.ReservaMesas.Domain.Configurations;
using HBSIS.ReservaMesas.Domain.Entities;
using HBSIS.ReservaMesas.Domain.Interfaces.Repositories;
using HBSIS.ReservaMesas.UnitTests.Domain.EntitiesForTest;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using Xunit;

namespace HBSIS.ReservaMesas.UnitTests.Application.Emails
{
    public class EmailReservationAlertTest
    {
        private readonly IEmailReservationAlertService _emailReservationAlert;
        private readonly IReservationRepository _reservationRepository;
        private readonly IReservationReminderEmail _reservationReminderEmail;
        private readonly EmailConfiguration _emailConfiguration;

        public EmailReservationAlertTest()
        {
            _reservationRepository = Substitute.For<IReservationRepository>();
            _emailConfiguration = Substitute.For<EmailConfiguration>();
            _reservationReminderEmail = Substitute.For<IReservationReminderEmail>();
            _emailReservationAlert = new EmailReservationAlertService(_reservationRepository, _emailConfiguration, _reservationReminderEmail);
        }

        [Fact]
        public async Task Should_Send_Email_Alert()
        {
            var workstation = new WorkstationForTest(2, "01-02-01-02", true, new Floor("Andar 2", true, "01-01", 1));

            List<Reservation> reservations = new List<Reservation>();
            var reservation = new ReservationForTest(1, "vitinho.ligadaslendas@gmail.com", "test", DateTime.Today.AddDays(1), workstation);
            reservations.Add(reservation);

            var emailMessage = new MailMessage()
            {
                From = new MailAddress("reservamesas@gmail.com"),
                Subject = "Teste",
                Body = "<h2>Texto teste</h2>",
                IsBodyHtml = true
            };

            emailMessage.To.Add("vitinho.ligadaslendas@gmail.com");

            _emailConfiguration.Host = "smtp.mailtrap.io";
            _emailConfiguration.Port = 587;
            _emailConfiguration.UserName = "97425b406fcc5f";
            _emailConfiguration.Password = "cd6b1766d91b8f";

            _reservationRepository.GetAllReservationsByNextDay().Returns(reservations);
            _reservationReminderEmail.CreateEmailMessage(Arg.Any<Reservation>(), Arg.Any<DateTime>()).Returns(emailMessage);

            var sentEmail = await _emailReservationAlert.SendEmailAlertOneDayBefore();

            emailMessage.Should().NotBeNull();
            sentEmail.Should().BeTrue();

            _reservationReminderEmail.Received(1).CreateEmailMessage(Arg.Any<Reservation>(), Arg.Any<DateTime>());
            await _reservationRepository.Received(1).GetAllReservationsByNextDay();
        }

        [Fact]
        public async Task Should_Not_Send_Email_Alert()
        {
            List<Reservation> reservations = new List<Reservation>();

            _reservationRepository.GetAllReservationsByNextDay().Returns(reservations);

            var sentEmail = await _emailReservationAlert.SendEmailAlertOneDayBefore();

            sentEmail.Should().BeFalse();

            await _reservationRepository.Received(1).GetAllReservationsByNextDay();
        }

        [Fact]
        public async Task Should_Send_Email_Alert_Doenst_Attended()
        {
            var workstation = new WorkstationForTest(2, "01-02-01-02", true, new Floor("Andar 2", true, "01-01", 1));

            List<Reservation> reservations = new List<Reservation>();
            var reservation = new ReservationForTest(1, "vitinho.ligadaslendas@gmail.com", "test", DateTime.Today.AddDays(1), workstation);
            reservations.Add(reservation);

            var emailMessage = new MailMessage()
            {
                From = new MailAddress("reservamesas@gmail.com"),
                Subject = "Teste",
                Body = "<h2>Texto teste</h2>",
                IsBodyHtml = true
            };

            emailMessage.To.Add("vitinho.ligadaslendas@gmail.com");

            _emailConfiguration.Host = "smtp.mailtrap.io";
            _emailConfiguration.Port = 587;
            _emailConfiguration.UserName = "97425b406fcc5f";
            _emailConfiguration.Password = "cd6b1766d91b8f";

            _reservationRepository.GetAllReservationsNotAttended().Returns(reservations);
            _reservationReminderEmail.CreateEmailMessage(Arg.Any<Reservation>(), Arg.Any<DateTime>()).Returns(emailMessage);

            var sentEmail = await _emailReservationAlert.SendEmailAlertDoesntAttended();

            emailMessage.Should().NotBeNull();
            sentEmail.Should().BeTrue();

            _reservationReminderEmail.Received(1).CreateEmailMessage(Arg.Any<Reservation>(), Arg.Any<DateTime>());
            await _reservationRepository.Received(1).GetAllReservationsNotAttended();
        }

        [Fact]
        public async Task Should_Not_Send_Email_Alert_Doenst_Attended()
        {
            List<Reservation> reservations = new List<Reservation>();

            _reservationRepository.GetAllReservationsNotAttended().Returns(reservations);

            var sentEmail = await _emailReservationAlert.SendEmailAlertDoesntAttended();

            sentEmail.Should().BeFalse();

            await _reservationRepository.Received(1).GetAllReservationsNotAttended();
        }
    }
}
