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
        public async Task AddAsync(T entity)
        {
           await  _context.AddAsync(entity);
        }

        public void Delete(T entity)
        {
             _context.Remove(entity);

        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            
           return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)=> await _context.Set<T>().FindAsync(id);
           

        public void Ubdate(T entity)
        {
            _context.Update(entity);
        }
    }
}
