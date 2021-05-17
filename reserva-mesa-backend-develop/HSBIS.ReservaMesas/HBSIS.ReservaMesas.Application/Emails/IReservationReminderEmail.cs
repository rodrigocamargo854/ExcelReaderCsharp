using HBSIS.ReservaMesas.Domain.Entities;
using System;
using System.Net.Mail;

namespace HBSIS.ReservaMesas.Application.Emails
{
    public interface IReservationReminderEmail
    {
        MailMessage CreateEmailMessage(Reservation reservation, DateTime dateReservation);
    }
}
