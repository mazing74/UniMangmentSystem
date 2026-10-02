using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.DAL.Models;

namespace UMS.DAL
{
    public class UniDBContext : DbContext       
    {
        public UniDBContext(DbContextOptions<UniDBContext> options):base(options){}

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
