using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.BLL.InterFace
{
    public interface IunitOfWork
    {
        public IEmpolyeeRepository EmpolyeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }
        public int Compelete();
    }
}
