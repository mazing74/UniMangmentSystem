using Microsoft.EntityFrameworkCore;
using UMS.BLL.InterFace;
using UMS.BLL.Repository;
using UMS.DAL;
using UMS.DAL.Models;

namespace UMS.BLL.TESTS.RepositoriesTests
{
    public class GenaricRepositoryTests
    {
        private readonly IGenaricRepository<Employee> _genaric;
        private readonly UniDBContext _context;
        public GenaricRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<UniDBContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _context = new UniDBContext(options);
            _genaric = new GenaricRepository<Employee>(_context);
        }

        #region Helpers

        // Helper: builds a valid employee so each test stays short
        private static Employee CreateEmployee(string email = "Mezo@gmail.com") => new Employee()
        {
            Salary = 1000,
            Age = 15,
            Address = "Zifa",
            Email = email,
            PhoneNumber = "0123456789",
            IsActive = true,
        };

        // Helper: saves employees directly with the context (not with the repository),
        // so the test only checks the method we are testing
        private async Task SeedAsync(params Employee[] employees)
        {
            _context.Set<Employee>().AddRange(employees);
            await _context.SaveChangesAsync();
        }
        #endregion

        [Fact]
        public async Task AddAsync_ValidEmployee_AddsEmployeeToContext()
        {
            // Arrange
            var emp = new Employee()
            {
                Salary = 1000,
                Age = 15,
                Address = "Zifa",
                Email = "Mezo@gmail.com",
                PhoneNumber = "0123456789",
                IsActive = true,
            };

            // Act
            await _genaric.AddAsync(emp);

            // Assert
            Assert.Equal(EntityState.Added, _context.Entry(emp).State);
        }



        [Fact]
        public async Task GetAllAsync_EmployeesExist_ReturnsAllEmployees()
        {
            // Arrange
            await SeedAsync(CreateEmployee("one@gmail.com"), CreateEmployee("two@gmail.com"));

            // Act
            var result = await _genaric.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ReturnsEmployee()
        {
            // Arrange
            var emp = CreateEmployee();
            await SeedAsync(emp);

            // Act
            var result = await _genaric.GetByIdAsync(emp.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(emp.Email, result.Email);
        }

    }
}