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
    }
}