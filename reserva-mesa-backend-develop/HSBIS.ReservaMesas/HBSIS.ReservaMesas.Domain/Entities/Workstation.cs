using FluentValidation;
using HBSIS.ReservaMesas.Domain.Validators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HBSIS.ReservaMesas.Domain.Entities
{
    public class Workstation : BaseEntity
    {
        public string Name { get; protected set; }
        public bool Active { get; protected set; }
        public int FloorId { get; protected set; }
        public virtual Floor Floor { get; protected set; }
        public IEnumerable<Reservation> Reservations { get; protected set; }
        [NotMapped]
        protected override IValidator Validator => new WorkstationValidator();

        protected Workstation()
        {
        }

        public Workstation(string name, bool active, int floorId)
        {
            this.Name = name;
            this.Active = active;
            this.FloorId = floorId;
        }

        public void Update(bool active)
        {
            this.Active = active;
        }
    }
}
