using FluentValidation.Results;
using HBSIS.ReservaMesas.Domain.Entities;

namespace HBSIS.ReservaMesas.Domain.Interfaces.Validators
{
    public interface IValidator
    {
        public ValidationResult Validate(BaseEntity entity);
    }
}