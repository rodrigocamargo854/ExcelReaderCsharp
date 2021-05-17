using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HBSIS.ReservaMesas.Domain.Entities
{
    public class Reservation : BaseEntity
    {
        public int WorkstationId { get; protected set; }
        public virtual Workstation Workstation { get; protected set; }
        public string UserId { get; protected set; }
        public string UserName { get; protected set; }
        public DateTime Date { get; protected set; }
        public DateTime CanceledOn { get; protected set; }
        public bool CheckInStatus { get; protected set; }
        public DateTime CheckInDateTime { get; protected set; }

        public void Cancel() => CanceledOn = DateTime.Today;

        public void CheckIn()
        {
            CheckInStatus = true;
            CheckInDateTime = DateTime.Now;
        }

        [NotMapped]
        protected override IValidator Validator => throw new System.NotImplementedException();

        protected Reservation()
        {
        }

        public Reservation(int workstationId, DateTime date, string userId, string userName)
        {
            this.WorkstationId = workstationId;
            this.Date = date;
            this.UserId = userId;
            this.UserName = userName;
        }
    }
}
