using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.BLL.InterFace;
using UMS.DAL;

namespace UMS.BLL.Repository
{
    public class UnitOfWork : IunitOfWork ,IDisposable
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

        public async Task<int> CompeleteAsync()
        {
          return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
