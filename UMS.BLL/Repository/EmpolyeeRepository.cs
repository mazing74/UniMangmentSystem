using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.BLL.InterFace;
using UMS.DAL;
using UMS.DAL.Models;

namespace UMS.BLL.Repository
{
    public class EmpolyeeRepository : GenaricRepository<Employee> ,IEmpolyeeRepository
    {
        private readonly UniDBContext _context;

        public EmpolyeeRepository(UniDBContext context) :base(context)
        {
            _context = context;
        }

        public IQueryable<Employee> SearchByName(string name)
        {
            var employees = _context.Employees.Include(e => e.Department).AsQueryable();
            if (!name.IsNullOrEmpty())
            {
                 employees = employees.Where(e => e.Name.ToLower().Contains(name.ToLower()));
                return employees;
            }
            return employees;

        }
    }
}
