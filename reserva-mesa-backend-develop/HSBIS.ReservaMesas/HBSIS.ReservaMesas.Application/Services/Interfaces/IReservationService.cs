using HBSIS.ReservaMesas.Application.Models;
using HBSIS.ReservaMesas.Application.Models.Reservations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HBSIS.ReservaMesas.Application.Services.Interfaces
{
    public interface IReservationService
    {
        Task DeleteReservations(CancelReservationRequestModel cancelReservationRequestModel, string userId);
        Task<IEnumerable<string>> GetAllReservedWorkstationsFromDateIntervalAndFloor(DateTime initialDate, DateTime finalDate, int floorId);
        Task<IEnumerable<int>> Create(ReservationRequestModel requestModel, string userId, string userName);
        Task<PageResponseModel<ReservationResponseModel>> GetAllReservationsFromDateInterval(DateTime initialDate, DateTime finalDate, string userId, int currentPage);
        Task<PageResponseModel<CheckInResponseModel>> GetAllActiveReservationsFromToday(int currentPage, string nameFilter);
        Task CheckIn(int reservationId);
        Task<byte[]> GetAllReserverdWorkstationsByDateInterval(DateTime initialDate, DateTime finalDate);
        Task<byte[]> GetAllConfirmedWorkstationsByDateInterval(DateTime initialDate, DateTime finalDate);
    }
}
