using System.Collections.Generic;

namespace HBSIS.ReservaMesas.Application.Models.Reservations
{
    public class CancelReservationRequestModel
    {
        public IEnumerable<int> ReservationIds { get; set; }
    }
}
