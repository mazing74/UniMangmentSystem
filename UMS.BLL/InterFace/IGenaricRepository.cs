using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.BLL.InterFace
{
    public interface IGenaricRepository<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
        public Task  AddAsync(T entity);
        public void Ubdate(T entity);
        public void Delete(T entity);

    }
}
