using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeGraphql.Unit.Tests.ValidationTest
{
    using AutoFixture;
    using EmployeeGraphql.API.Types.Input;
    using EmployeeGraphql.API.Validations;
    using FluentValidation.TestHelper;
    using Xunit;

    public class EmployeePartTimeInputValidatorTests
    {
        private readonly EmployeePartTimeInputValidator _validator = new EmployeePartTimeInputValidator();
        private readonly PartTimeEmployeeInput partTimeEmployeeInput;
        public EmployeePartTimeInputValidatorTests()
        {
            var fixture = new Fixture();
            fixture.Customize<PartTimeEmployeeInput>(composer =>
                                                            composer.Without(e => e.Id)
                                                                    .With(e => e.Name, ""));
             partTimeEmployeeInput = fixture.Create<PartTimeEmployeeInput>();
        }
        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
           var result= _validator.TestValidate(partTimeEmployeeInput);
            result.ShouldHaveValidationErrorFor(emp => emp.Name);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Should_Have_Error_When_Name_Is_Null_Or_Empty(string name)
        {
            partTimeEmployeeInput.Name = name;

            var result = _validator.TestValidate(partTimeEmployeeInput);
            result.ShouldHaveValidationErrorFor(emp => emp.Name)
                .WithErrorMessage("Name is required.");
        }

        [Fact]
        public void Should_Have_Error_When_HourlyRate_Is_Not_Greater_Than_Zero()
        {
            partTimeEmployeeInput.HourlyRate = 0;

            var result = _validator.TestValidate(partTimeEmployeeInput);
            result.ShouldHaveValidationErrorFor(emp => emp.HourlyRate)
                .WithErrorMessage("Salary should be greater than 0");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Should_Not_Have_Error_When_HourlyRate_Is_Greater_Than_Zero(decimal hourlyRate)
        {
            partTimeEmployeeInput.HourlyRate = hourlyRate;
            var result = _validator.TestValidate(partTimeEmployeeInput);
            result.ShouldNotHaveValidationErrorFor(emp => emp.HourlyRate);
        }
    }

   
}
