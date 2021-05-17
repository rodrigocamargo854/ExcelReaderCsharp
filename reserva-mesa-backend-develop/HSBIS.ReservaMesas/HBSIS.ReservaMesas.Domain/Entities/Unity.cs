using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HBSIS.ReservaMesas.Domain.Entities
{
    public class Unity : BaseEntity
    {
        public string Name { get; protected set; }
        public bool Active { get; protected set; }
        public IEnumerable<Floor> Floors { get; protected set; }
        [NotMapped]
        protected override IValidator Validator => throw new NotImplementedException();

        public Unity(string name, bool active)
        {
            Name = name;
            Active = active;
        }
    }
}
