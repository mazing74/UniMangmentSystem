using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.BLL.InterFace;
using UMS.DAL;

namespace UMS.BLL.Repository
{
    public class UnitOfWork : IunitOfWork
    {
        private readonly UniDBContext _context;

        public IEmpolyeeRepository EmpolyeeRepository { get ; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }

        public UnitOfWork(UniDBContext context)
        {
            EmpolyeeRepository = new EmpolyeeRepository(context);
            DepartmentRepository = new DepartmentRepository(context);
            _context = context;
        }

        public int Compelete()
        {
          return _context.SaveChanges();
        }
    }
}
