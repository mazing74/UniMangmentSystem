using Microsoft.EntityFrameworkCore;
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
    public class GenaricRepository<T> : IGenaricRepository<T>  where T:class
    {
        private readonly UniDBContext _context;

        public GenaricRepository(UniDBContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);

        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof (Employee))
            {
                return (IEnumerable<T>)_context.Employees.Include(e => e.Department).ToList();
            }
           return _context.Set<T>().ToList();
        }

        public T GetById(int id)=> _context.Set<T>().Find(id);
           

        public void Ubdate(T entity)
        {
            _context.Update(entity);
        }
    }
}
