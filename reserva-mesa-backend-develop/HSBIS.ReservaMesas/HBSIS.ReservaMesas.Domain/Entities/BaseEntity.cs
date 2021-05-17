using FluentValidation;
using HBSIS.ReservaMesas.Domain.ExtensionMethods;

namespace HBSIS.ReservaMesas.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool Deleted { get; private set; }
        protected abstract IValidator Validator { get; }

        public void Delete() => Deleted = true;

        public void ValidateEntity()
        {
            var validatorContext = new ValidationContext<object>(this);
            var validationResult = Validator.Validate(validatorContext);
            validationResult.HandleValidationResult();
        }
    }
}
