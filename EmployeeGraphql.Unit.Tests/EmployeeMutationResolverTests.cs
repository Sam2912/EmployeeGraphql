using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeGraphql.Unit.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using EmployeeGraphql.API.Models;
    using EmployeeGraphql.API.Mutation;
    using EmployeeGraphql.API.Services;
    using EmployeeGraphql.API.Types.Input;
    using EmployeeGraphql.API.Types;
    using FluentValidation;
    using FluentValidation.Results;
    using HotChocolate.Resolvers;
    using Moq;
    using Xunit;

    public class EmployeeMutationResolverTests
    {
        [Fact]
        public async Task CreateEmployeeAsync_ValidInput_ReturnsEmployee()
        {
            // Arrange
            var mockEmployeeService = new Mock<IEmployeeService>();
            var mockMapper = new Mock<IMapper>();
            var mockValidator = new Mock<IValidator<EmployeeInput>>();
            var mockResolverContext = new Mock<IResolverContext>();

            var employeeId= Guid.NewGuid();
            var createEmployeeInput = new EmployeeInput { FullTimeEmployeeInput = new FullTimeEmployeeInput() {Id= employeeId, Name = "John Doe", Department = Department.IT } };
            var expectedResult = new FullTimeEmployee { Id = employeeId, Name = "John Doe", Department = Department.IT, Type = Employee.FullTime };

            mockValidator.Setup(v => v.ValidateAsync(It.IsAny<EmployeeInput>(),It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new ValidationResult(new List<ValidationFailure>()));

            mockMapper.Setup(m => m.Map<FullTimeEmployee>(It.IsAny<FullTimeEmployeeInput>()))
                .Returns(expectedResult);

            mockEmployeeService.Setup(s => s.AddEmployeeAsync(It.IsAny<BaseEmployee>()))
                .ReturnsAsync(expectedResult);

            var resolver = new EmployeeMutationResolver(mockEmployeeService.Object, mockMapper.Object, mockValidator.Object);

            // Act
            var result = await resolver.CreateEmployeeAsync(createEmployeeInput, mockResolverContext.Object);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEmployee>(result);
            Assert.Equal(expectedResult.Id, result.Id);
            Assert.Equal(expectedResult.Name, result.Name);
            Assert.Equal(expectedResult.Department, result.Department);
            Assert.Equal(expectedResult.Type, result.Type);
        }

        [Fact]
        public async Task CreateEmployeeAsync_InvalidInput_ReturnsNull()
        {
            // Arrange
            var mockEmployeeService = new Mock<IEmployeeService>();
            var mockMapper = new Mock<IMapper>();
            var mockValidator = new Mock<IValidator<EmployeeInput>>();
            var mockResolverContext = new Mock<IResolverContext>();

            var createEmployeeInput = new EmployeeInput(); // Invalid input

            var validationFailures = new List<ValidationFailure>
                {
                    new ValidationFailure("Name", "Name is required."),
                    new ValidationFailure("Department", "Department is required.")
                };

            var validationResult = new ValidationResult(validationFailures);
            mockValidator.Setup(v => v.ValidateAsync(It.IsAny<EmployeeInput>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(validationResult);

            var resolver = new EmployeeMutationResolver(mockEmployeeService.Object, mockMapper.Object, mockValidator.Object);

            // Act
            var result = await resolver.CreateEmployeeAsync(createEmployeeInput, mockResolverContext.Object);

            // Assert
            Assert.Null(result);
            mockResolverContext.Verify(rc => rc.ReportError(It.IsAny<string>()), Times.Exactly(validationFailures.Count));
        }
    }


}
