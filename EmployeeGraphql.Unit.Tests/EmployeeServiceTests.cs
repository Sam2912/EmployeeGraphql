namespace EmployeeGraphql.Unit.Tests;

using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeGraphql.API.Services;
using EmployeeGraphql.API.Models;
using EmployeeGraphql.API.Repositories;

public class EmployeeServiceTests
{
    private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;

    public EmployeeServiceTests()
    {
        _mockEmployeeRepository = new Mock<IEmployeeRepository>();

    }

    [Fact]
    public async Task GetAllEmployeesAsync_ReturnsEmployees()
    {
        // Arrange
        var employees = new List<BaseEmployee>
        {
            new FullTimeEmployee { Id = Guid.NewGuid(), Name = "John Doe" },
            new PartTimeEmployee { Id = Guid.NewGuid(), Name = "Jane Smith" }
        };
        _mockEmployeeRepository.Setup(x => x.GetAllAsync()).ReturnsAsync((IEnumerable<BaseEmployee>)employees);

        var employeeService = new EmployeeService(_mockEmployeeRepository.Object);

        // Act
        var result = await employeeService.GetAllEmployeesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetEmployeeByIdAsync_ReturnsEmployee()
    {
        // Arrange
        var employeeId = Guid.NewGuid();
        var employee = new FullTimeEmployee { Id = employeeId, Name = "John Doe" };
        _mockEmployeeRepository.Setup(x => x.GetByIdAsync(It.Is<Guid>(id => id == employeeId))).ReturnsAsync(employee);

        var employeeService = new EmployeeService(_mockEmployeeRepository.Object);

        // Act
        var result = await employeeService.GetEmployeeByIdAsync(employeeId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(employeeId, result.Id);
    }
}
