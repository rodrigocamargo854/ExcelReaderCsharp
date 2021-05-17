using System;

namespace HBSIS.ReservaMesas.Application.Models.Reservations
{
    public class CheckInResponseModel
    {
        public int ReservationId { get; set; }
        public string UserName { get; set; }
        public bool CheckInStatus { get; set; }
        public string WorkstationName { get; set; }

        public CheckInResponseModel(int reservationId, string userName, bool checkInStatus, string workstationName)
        {
            ReservationId = reservationId;
            UserName = userName;
            CheckInStatus = checkInStatus;
            WorkstationName = workstationName;
        }
    }
}
