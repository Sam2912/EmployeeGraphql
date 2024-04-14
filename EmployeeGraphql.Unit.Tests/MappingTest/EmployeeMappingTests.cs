using AutoMapper;
using EmployeeGraphql.API.Mapping;
using EmployeeGraphql.API.Models;
using EmployeeGraphql.API.Types.Input;
using EmployeeGraphql.API.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeGraphql.Unit.Tests.MappingTest
{
    public class EmployeeMappingTests: MappingBaseTests
    {
        [Fact]
        public void TestFullTimeEmployeeMapping()
        {
            // Arrange
            var input = new FullTimeEmployeeInput { Id = Guid.NewGuid(), Name = "John Doe", Salary = 50000, Type = Employee.FullTime };

            // Act
            var fullTimeEmployee = _mapper.Map<FullTimeEmployee>(input);

            // Assert
            Assert.Equal(input.Id, fullTimeEmployee.Id);
            Assert.Equal(input.Name, fullTimeEmployee.Name);
            Assert.Equal(input.Salary, fullTimeEmployee.Salary);
            Assert.Equal(Employee.FullTime, fullTimeEmployee.Type); // Ensure Type is set correctly
        }

        [Fact]
        public void TestPartTimeEmployeeMapping()
        {
            // Arrange
            var input = new PartTimeEmployeeInput { Id = Guid.NewGuid(), Name = "Jane Doe", HourlyRate = 25, Type = Employee.PartTime };

            // Act
            var partTimeEmployee = _mapper.Map<PartTimeEmployee>(input);

            // Assert
            Assert.Equal(input.Id, partTimeEmployee.Id);
            Assert.Equal(input.Name, partTimeEmployee.Name);
            Assert.Equal(input.HourlyRate, partTimeEmployee.HourlyRate);
            Assert.Equal(Employee.PartTime, partTimeEmployee.Type); // Ensure Type is set correctly
        }
    }
}
