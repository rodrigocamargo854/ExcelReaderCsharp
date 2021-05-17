using HBSIS.ReservaMesas.Domain.Configurations;
using HBSIS.ReservaMesas.Domain.Interfaces.Repositories;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HBSIS.ReservaMesas.Application.Emails
{
    public class EmailReservationAlertService : IEmailReservationAlertService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IReservationReminderEmail _reservationReminderEmail;
        private readonly EmailConfiguration _emailConfiguration;

        public EmailReservationAlertService(IReservationRepository reservationRepository,
            EmailConfiguration emailConfiguration,
            IReservationReminderEmail reservationReminderEmail)
        {
            _reservationRepository = reservationRepository;
            _emailConfiguration = emailConfiguration;
            _reservationReminderEmail = reservationReminderEmail;
        }

        public async Task<bool> SendEmailAlertOneDayBefore()
        {
            var reservations = await _reservationRepository.GetAllReservationsByNextDay();

            if (!reservations.Any()) return false;

            foreach (var item in reservations)
            {
                var dateEmail = DateTime.Today.AddDays(1);
                var emailMessage = _reservationReminderEmail.CreateEmailMessage(item, dateEmail);

                using SmtpClient smtp = new SmtpClient(_emailConfiguration.Host, _emailConfiguration.Port)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_emailConfiguration.UserName, _emailConfiguration.Password),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                smtp.Send(emailMessage);
            }

            return true;

        }

        public async Task<bool> SendEmailAlertDoesntAttended()
        {
            var reservations = await _reservationRepository.GetAllReservationsNotAttended();

            if (!reservations.Any()) return false;

            foreach (var item in reservations)
            {
                var dateEmail = DateTime.Today;
                var emailMessage = _reservationReminderEmail.CreateEmailMessage(item, dateEmail);

                using SmtpClient smtp = new SmtpClient(_emailConfiguration.Host, _emailConfiguration.Port)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_emailConfiguration.UserName, _emailConfiguration.Password),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                smtp.Send(emailMessage);
            }

            return true;

        }
    }
}
