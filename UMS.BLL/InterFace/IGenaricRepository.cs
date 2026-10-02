using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.BLL.InterFace
{
    public interface IGenaricRepository<T>
    {
        public IEnumerable<T> GetAll();
        public T GetById(int id);
        public int Add(T entity);
        public int Ubdate(T entity);
        public int Delete(T entity);

    }
}
