using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HBSIS.ReservaMesas.Application.Models;
using HBSIS.ReservaMesas.Application.Models.Reservations;
using HBSIS.ReservaMesas.Application.Reports;
using HBSIS.ReservaMesas.Application.Services.Interfaces;
using HBSIS.ReservaMesas.Domain.Entities;
using HBSIS.ReservaMesas.Domain.Exceptions;
using HBSIS.ReservaMesas.Domain.Interfaces.Repositories;

namespace HBSIS.ReservaMesas.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IWorkstationRepository _workstationRepository;

        public ReservationService(IReservationRepository reservationRepository, IWorkstationRepository workstationRepository)
        {
            this._reservationRepository = reservationRepository;
            this._workstationRepository = workstationRepository;
        }

        public async Task<IEnumerable<string>> GetAllReservedWorkstationsFromDateIntervalAndFloor(DateTime initialDate, DateTime finalDate, int floorId)
        {
            VerifyDates(initialDate, finalDate);
            var reservedWorkstations = await _reservationRepository.GetAllReservedWorkstationsFromDateIntervalAndFloor(initialDate, finalDate, floorId);

            return reservedWorkstations.Select(reservedWorkstations => reservedWorkstations.Name);
        }

        public async Task<PageResponseModel<ReservationResponseModel>> GetAllReservationsFromDateInterval(DateTime initialDate, DateTime finalDate, string userId, int currentPage)
        {
            VerifyDatesForUserReservations(initialDate, finalDate);
            var userReservations = await _reservationRepository.GetAllReservationsFromDateInterval(initialDate, finalDate, userId, currentPage);

            var userReservationModels = userReservations.Select(reservation => new ReservationResponseModel(reservation.Workstation.Name, reservation.Date, reservation.Id));

            var userReservationsTotalCount = await _reservationRepository.CountUserReservationsByDateInterval(userId, initialDate, finalDate);

            return new PageResponseModel<ReservationResponseModel>(currentPage, userReservationsTotalCount, userReservationModels);
        }

        private void VerifyDatesForUserReservations(DateTime initialDate, DateTime finalDate)
        {
            if (initialDate == default(DateTime))
            {
                throw new CustomValidationException("Data Inicial deve ser informada");
            }

            if (CheckMaxDate(initialDate))
            {
                throw new CustomValidationException("Data Inicial não pode estar a mais de 2 semanas no futuro");
            }

            if (finalDate == default(DateTime))
            {
                throw new CustomValidationException("Data Final deve ser informada");
            }

            if (initialDate.CompareTo(finalDate) > 0)
            {
                throw new CustomValidationException("Data Inicial não pode ser maior que Data Final");
            }
        }

        private void VerifyDates(DateTime initialDate, DateTime finalDate)
        {
            VerifyDatesForUserReservations(initialDate, finalDate);

            if (CheckMaxDate(finalDate))
            {
                throw new CustomValidationException("Data Final não pode estar a mais de 2 semanas no futuro");
            }

            if (CheckMinDate(initialDate))
            {
                throw new CustomValidationException("Data Inicial não pode estar no passado");
            }
        }

        private void VerifyDatesToGenarateReport(DateTime initialDate, DateTime finalDate)
        {
            if (initialDate == default(DateTime))
            {
                throw new CustomValidationException("Data Inicial deve ser informada");
            }

            if (finalDate == default(DateTime))
            {
                throw new CustomValidationException("Data Final deve ser informada");
            }

            if (finalDate.Date < initialDate.Date)
            {
                throw new CustomValidationException("Data Final não pode ser anterior a data inicial");
            }
        }

        private bool CheckMinDate(DateTime dateTime)
        {
            return dateTime.CompareTo(DateTime.Today) < 0;
        }

        private bool CheckMaxDate(DateTime dateTime)
        {
            return dateTime.CompareTo(DateTime.Today.AddDays(7 - (int)DateTime.Today.DayOfWeek).AddDays(6)) > 0;
        }

        public async Task<byte[]> GetAllReserverdWorkstationsByDateInterval(DateTime initialDate, DateTime finalDate)
        {
            VerifyDatesToGenarateReport(initialDate, finalDate);

            var reservedWorkstations =
                (await _reservationRepository.GetAllReservedWorkstationsByDateInterval(initialDate, finalDate))
                    .Select(x => new CsvReportModel
                        (x.Date, x.Workstation.Floor.Name, x.Workstation.Name, x.UserName, x.Workstation.Active ? "Ativo" : "Desativado"));

            return new CsvReportsManager().GenerateCsvReport(reservedWorkstations);
        }

        public async Task<IEnumerable<int>> Create(ReservationRequestModel requestModel, string userId, string userName)
        {
            var workstation = await _workstationRepository.GetByName(requestModel.WorkstationName);

            if (workstation == null)
            {
                throw new NotFoundException("Estação de trabalho não encontrada.");
            }

            VerifyDates(requestModel.InitialDate, requestModel.FinalDate);

            await VerifyIfWorkstationIsAlreadyReserved(workstation, requestModel.InitialDate, requestModel.FinalDate);

            var days = (requestModel.FinalDate - requestModel.InitialDate).TotalDays;

            var reservationIds = new List<int>();

            for (int i = 0; i <= days; i++)
            {
                var reservedDate = requestModel.InitialDate.Date.AddDays(i);
                var reservation = new Reservation(workstation.Id, reservedDate, userId, userName);
                await _reservationRepository.Create(reservation);
                reservationIds.Add(reservation.Id);
            }

            try
            {
                await _reservationRepository.Save();
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("IX_Reservation_WorkstationId_Date"))
                {
                    throw new UniqueKeyConstraintErrorException("A estação de trabalho informada já foi reservada neste intervalo de tempo!");
                }
            }

            return reservationIds;
        }

        private async Task VerifyIfWorkstationIsAlreadyReserved(Workstation workstation, DateTime initialDate, DateTime finalDate)
        {
            if (await _reservationRepository.VerifyIfWorkstationIsAlreadyReserved(workstation, initialDate, finalDate))
            {
                throw new CustomValidationException("A estação de trabalho informada já foi reservada neste intervalo de tempo!");
            }
        }

        public async Task DeleteReservations(CancelReservationRequestModel cancelReservationRequestModel, string userId)
        {
            var reservationsToDelete = new List<Reservation>();

            if (cancelReservationRequestModel.ReservationIds != null && cancelReservationRequestModel.ReservationIds.Any())
            {
                foreach (int reservationId in cancelReservationRequestModel.ReservationIds)
                {
                    var reservation = await _reservationRepository.GetReservationByIdAndUserId(reservationId, userId);

                    if (reservation != null && !CheckMinDate(reservation.Date.Date))
                    {
                        reservation.Delete();
                        reservation.Cancel();
                        reservationsToDelete.Add(reservation);
                    }
                    else
                    {
                        throw new NotFoundException("Reserva não existe ou é de um dia anterior ao dia de hoje.");
                    }
                }
            }
            else
            {
                throw new CustomValidationException("Lista de IDs vazia ou inexistente.");
            }

            _reservationRepository.DeleteReservations(reservationsToDelete);
            await _reservationRepository.Save();
        }

        public async Task<PageResponseModel<CheckInResponseModel>> GetAllActiveReservationsFromToday(int currentPage, string nameFilter)
        {
            var todayReservations = await _reservationRepository.GetAllActiveReservationFromToday(currentPage, nameFilter);

            var todayReservationModels = todayReservations.Select(reservation => new CheckInResponseModel(reservation.Id, reservation.UserName, reservation.CheckInStatus, reservation.Workstation.Name));

            return new PageResponseModel<CheckInResponseModel>(currentPage, await _reservationRepository.CountAllReservationsFromToday(nameFilter), todayReservationModels);
        }

        public async Task CheckIn(int reservationId)
        {
            var reservation = await _reservationRepository.GetById(reservationId);
            ValidateCheckIn(reservation);
            reservation.CheckIn();
            _reservationRepository.Update(reservation);
            await _reservationRepository.Save();
        }

        private void ValidateCheckIn(Reservation reservation)
        {
            if (reservation == null)
            {
                throw new CustomValidationException("Reservation solicitada não existe");
            }
            if (reservation.Date.Date.CompareTo(DateTime.Today.Date) != 0)
            {
                throw new CustomValidationException("Check-in pode ser feito somente com reservas do dia atual");
            }
        }

        public async Task<byte[]> GetAllConfirmedWorkstationsByDateInterval(DateTime initialDate, DateTime finalDate)
        {
            VerifyDatesToGenarateReport(initialDate, finalDate);

            var confirmedWorkstations = await _reservationRepository.GetAllConfirmedWorkstationsByDateInterval(initialDate, finalDate);

            var reportModelConfirmedWorkstations = confirmedWorkstations.Select(x => new CsvConfirmedWorkstationsReportModel
                        (x.Date, x.Workstation.Floor.Name, x.Workstation.Name, x.UserId));

            return new CsvReportsManager().GenerateCsvReport(reportModelConfirmedWorkstations);
        }
    }
}
