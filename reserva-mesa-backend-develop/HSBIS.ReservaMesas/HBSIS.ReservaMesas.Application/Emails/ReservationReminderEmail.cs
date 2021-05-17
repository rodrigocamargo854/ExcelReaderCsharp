using HBSIS.ReservaMesas.Domain.Configurations;
using HBSIS.ReservaMesas.Domain.Entities;
using System;
using System.Net.Mail;

namespace HBSIS.ReservaMesas.Application.Emails
{
    public class ReservationReminderEmail : IReservationReminderEmail
    {
        private readonly UrlConfiguration _urlConfiguration;

        public ReservationReminderEmail(UrlConfiguration urlConfiguration)
        {
            _urlConfiguration = urlConfiguration;
        }

        public MailMessage CreateEmailMessage(Reservation reservation, DateTime dateReservation)
        {
            var link = _urlConfiguration.Development;

            var floorName = reservation.Workstation.Floor.Name;
            var workstationName = reservation.Workstation.Name;

            MailMessage mail = new MailMessage
            {
                From = new MailAddress("reservamesas@ambevtech.com.br")
            };

            mail.To.Add(reservation.UserId);
            mail.Subject = "Lembrete de Reserva";
            mail.Body = "<h1>Olá, Colaborador</h1> \r\n"
                + "<h2> No dia " + dateReservation + " você tem uma reserva de estação de trabalho, na AMBEV TECH, no " 
                + floorName + " e mesa " + workstationName + "</h2> \r\n"
                + "<p> <a href='" + link + "'> Clique aqui </a> para visualizar a sua reserva ou cancelar sua reserva caso você não vá para a empresa. </p>";

            mail.IsBodyHtml = true;

            return mail;
        }
    }
}
