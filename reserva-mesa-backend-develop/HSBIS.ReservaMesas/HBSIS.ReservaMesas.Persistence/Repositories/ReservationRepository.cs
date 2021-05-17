using HBSIS.ReservaMesas.Domain.Entities;
using HBSIS.ReservaMesas.Domain.Interfaces.Repositories;
using HBSIS.ReservaMesas.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HBSIS.ReservaMesas.Persistence.Repositories
{
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(MainContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Workstation>> GetAllReservedWorkstationsFromDateIntervalAndFloor(DateTime initialDate, DateTime finalDate, int floorId)
        {
            return await Query()
                .Where(r => r.Workstation.FloorId == floorId && r.Date.Date.CompareTo(initialDate.Date) >= 0 && r.Date.Date.CompareTo(finalDate.Date) <= 0)
                .Select(workstation => workstation.Workstation)
                .Distinct()
                .ToListAsync();
        }

        public async Task<bool> VerifyIfWorkstationIsAlreadyReserved(Workstation workstation, DateTime initialDate, DateTime finalDate)
        {
            return await Query()
                .Where(r => r.WorkstationId == workstation.Id && r.Date.Date.CompareTo(initialDate.Date) >= 0 && r.Date.Date.CompareTo(finalDate.Date) <= 0)
                .AnyAsync();
        }

        public async Task<Reservation> GetReservationByIdAndUserId(int reservationId, string userId)
        {
            return await Query().SingleOrDefaultAsync(r => r.Id == reservationId && r.UserId == userId);
        }

        public void DeleteReservations(List<Reservation> reservations)
        {
            _dbSet.UpdateRange(reservations);
        }

        public async Task<IEnumerable<Reservation>> GetAllReservationsFromDateInterval(DateTime initialDate, DateTime finalDate, string userId, int currentPage)
        {
            return await Query()
                .Where(reservation => reservation.Date.Date.CompareTo(initialDate.Date) >= 0 && reservation.Date.Date.CompareTo(finalDate.Date) <= 0 && reservation.UserId == userId)
                .Include(reservation => reservation.Workstation)
                .Skip(currentPage * 14)
                .Take(14)
                .ToListAsync();
        }

        public async Task<int> CountUserReservationsByDateInterval(string userId, DateTime initialDate, DateTime finalDate)
        {
            return await Query()
                .Where(reservation => reservation.Date.Date.CompareTo(initialDate.Date) >= 0 && reservation.Date.Date.CompareTo(finalDate.Date) <= 0 && reservation.UserId == userId)
                .CountAsync();
        }

        public async Task<IEnumerable<Reservation>> GetAllActiveReservationFromToday(int currentPage, string nameFilter) 
        {
            nameFilter = nameFilter == null ? "" : nameFilter;

            return await Query()
                .Where(reservation => reservation.UserName.Contains(nameFilter) 
                && reservation.Date.Date.CompareTo(DateTime.Today.Date) == 0
                && reservation.CanceledOn.CompareTo(DateTime.MinValue) == 0)
                .Include(reservation => reservation.Workstation)
                .Skip(currentPage * 14)
                .Take(14)
                .ToListAsync();
        }

        public async Task<int> CountAllReservationsFromToday(string nameFilter) {
            nameFilter = nameFilter == null ? "" : nameFilter;

            return await Query()
                .Where(reservation => reservation.UserId.Contains(nameFilter)
                && reservation.Date.Date.CompareTo(DateTime.Today.Date) == 0
                && reservation.CanceledOn.CompareTo(DateTime.MinValue) == 0)
                .CountAsync();
        }
        
        public async Task<IEnumerable<Reservation>> GetAllReservedWorkstationsByDateInterval(DateTime initialDate, DateTime finalDate)
        {
            return await Query()
                     .Include(x => x.Workstation)
                     .Include(x => x.Workstation.Floor)
                     .Where(x => x.Date.Date.CompareTo(initialDate.Date) >= 0 && x.Date.Date.CompareTo(finalDate.Date) <= 0)
                     .OrderBy(x => x.Date)
                     .ThenBy(x => x.Workstation.Floor.Id)
                     .ThenBy(x => x.Workstation.Id)
                     .Distinct()
                     .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetAllReservationsByNextDay()
        {
            return await Query()
                .Include(reservation => reservation.Workstation)
                .ThenInclude(workstation => workstation.Floor)
                .Where(reservation => reservation.Date.Date == DateTime.Today.AddDays(1))
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetAllReservationsNotAttended()
        {
            return await Query()
                .Include(reservation => reservation.Workstation)
                .ThenInclude(workstation => workstation.Floor)
                .Where(reservation => reservation.Date.Date == DateTime.Today && !reservation.CheckInStatus)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetAllConfirmedWorkstationsByDateInterval(DateTime initialDate, DateTime finalDate)
        {
            return await Query()
                      .Include(x => x.Workstation)
                      .Include(x => x.Workstation.Floor)
                      .Where(x => x.Date.Date.CompareTo(initialDate.Date) >= 0 && x.Date.Date.CompareTo(finalDate.Date) <= 0 && x.CheckInStatus)
                      .OrderBy(x => x.Date)
                      .ThenBy(x => x.Workstation.Floor.Id)
                      .ThenBy(x => x.Workstation.Id)
                      .Distinct()
                      .ToListAsync();
        }
    }
}
