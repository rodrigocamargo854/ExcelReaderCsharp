using FluentValidation.Results;
using HBSIS.ReservaMesas.Domain.Exceptions;
using System.Collections.Generic;

namespace HBSIS.ReservaMesas.Domain.ExtensionMethods
{
    public static class ValidationResultExtension
    {
        public static void HandleValidationResult(this ValidationResult result)
        {
            if (!result.IsValid)
            {
                var validationExceptionList = new HashSet<CustomValidationException>();
                foreach (var validationError in result.Errors)
                {
                    validationExceptionList.Add(new CustomValidationException(validationError.ErrorMessage));
                }
                throw new CustomValidationException(validationExceptionList);
            }

            return;
        }
    }
}
