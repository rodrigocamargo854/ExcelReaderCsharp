using FluentValidation;
using HBSIS.ReservaMesas.Domain.Entities;

namespace HBSIS.ReservaMesas.Domain.Validators
{
    public class FloorValidator : AbstractValidator<Floor>
    {
        public FloorValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome deve ser preenchido.")
                .Length(3, 35).WithMessage("O nome deve conter entre 3 e 35 caracteres!");
        }
    }
}
