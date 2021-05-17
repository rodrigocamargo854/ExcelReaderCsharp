using FluentValidation.TestHelper;
using HBSIS.ReservaMesas.Domain.Entities;
using HBSIS.ReservaMesas.Domain.Validators;
using Xunit;

namespace HBSIS.ReservaMesas.UnitTests.Domain.Validators
{
    public class WorkstationValidatorTest
    {
        private readonly WorkstationValidator _validator;

        public WorkstationValidatorTest()
        {
            _validator = new WorkstationValidator();
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void Should_Valid_Name_About_Null_Or_With_White_Spaces(string name)
        {
            var workstation = new Workstation(name, true, 1);

            _validator.ShouldHaveValidationErrorFor(workstation => workstation.Name, workstation)
                .WithErrorMessage("O nome deve ser preenchido.");
        }

        [Theory]
        [InlineData(2)]
        [InlineData(36)]
        public void Should_Valid_Name_About_Character_Numbers(int characters)
        {
            var name = new string('a', characters);

            var workstation = new Workstation(name, true, 1);

            _validator.ShouldHaveValidationErrorFor(workstation => workstation.Name, workstation)
                .WithErrorMessage("O nome deve conter entre 3 e 35 caracteres!");
        }
    }
}
