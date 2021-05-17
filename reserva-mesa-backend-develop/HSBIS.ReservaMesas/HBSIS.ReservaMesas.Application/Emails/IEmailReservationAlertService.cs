using System.Threading.Tasks;

namespace HBSIS.ReservaMesas.Application.Emails
{
    public interface IEmailReservationAlertService
    {
        Task<bool> SendEmailAlertOneDayBefore();
        Task<bool> SendEmailAlertDoesntAttended();
    }
}
