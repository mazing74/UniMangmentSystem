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
    public class DepartmentRepository :GenaricRepository<Department> ,IDepartmentRepository
    {
        private readonly UniDBContext _context;

        public DepartmentRepository(UniDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
