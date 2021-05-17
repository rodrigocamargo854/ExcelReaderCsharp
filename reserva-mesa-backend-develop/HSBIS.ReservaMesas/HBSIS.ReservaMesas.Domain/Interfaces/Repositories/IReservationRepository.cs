using HBSIS.ReservaMesas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HBSIS.ReservaMesas.Domain.Interfaces.Repositories
{
    public interface IReservationRepository : IGenericRepository<Reservation>
    {
        public Task<IEnumerable<Workstation>> GetAllReservedWorkstationsFromDateIntervalAndFloor(DateTime initialDate, DateTime finalDate, int floorId);
        public Task<bool> VerifyIfWorkstationIsAlreadyReserved(Workstation workstation, DateTime initialDate, DateTime finalDate);
        public void DeleteReservations(List<Reservation> reservations);
        public Task<Reservation> GetReservationByIdAndUserId(int reservationId, string userId);
        public Task<IEnumerable<Reservation>> GetAllReservationsFromDateInterval(DateTime initialDate, DateTime finalDate, string userId, int currentPage);
        public Task<int> CountUserReservationsByDateInterval(string userId, DateTime initialDate, DateTime finalDate);
        public Task<IEnumerable<Reservation>> GetAllActiveReservationFromToday(int currentPage, string nameFilter);
        public Task<int> CountAllReservationsFromToday(string nameFilter);
        public Task<IEnumerable<Reservation>> GetAllReservedWorkstationsByDateInterval(DateTime initialDate, DateTime finalDate);
        public Task<IEnumerable<Reservation>> GetAllConfirmedWorkstationsByDateInterval(DateTime initialDate, DateTime finalDate);
        public Task<IEnumerable<Reservation>> GetAllReservationsByNextDay();
        public Task<IEnumerable<Reservation>> GetAllReservationsNotAttended();
      }
}
