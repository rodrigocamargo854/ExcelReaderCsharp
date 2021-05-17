using FluentValidation;
using HBSIS.ReservaMesas.Domain.Validators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HBSIS.ReservaMesas.Domain.Entities
{
    public class Floor : BaseEntity
    {
        public string Name { get; protected set; }
        public bool Active { get; protected set; }
        public int UnityId { get; protected set; }
        public virtual Unity Unity { get; protected set; }
        public IEnumerable<Workstation> Workstations { get; protected set; }
        public string Code { get; protected set; }

        [NotMapped]
        protected override IValidator Validator => new FloorValidator();

        protected Floor()
        {
        }

        public Floor(string name, bool active, string code, int unityId)
        {
            Name = name;
            Active = active;
            Code = code;
            UnityId = unityId;
        }

        public void Update(string name, bool active, int unityId)
        {
            this.Name = name;
            this.Active = active;
            this.UnityId = unityId;
        }
    }
}
