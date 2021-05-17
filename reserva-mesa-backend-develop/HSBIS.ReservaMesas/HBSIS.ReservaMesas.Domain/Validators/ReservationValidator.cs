using FluentValidation;
using HBSIS.ReservaMesas.Domain.Entities;

namespace HBSIS.ReservaMesas.Domain.Validators
{
    public class ReservationValidator : AbstractValidator<Reservation>
    {
        public ReservationValidator()
        {
            RuleFor(x => x.Date).NotEmpty().WithMessage("Data inválida ou não preenchida!");
        }
    }
}
