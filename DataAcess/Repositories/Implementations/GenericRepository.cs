using DataAcess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly HotelDbContext _Dbcontext;
        private readonly DbSet<T> _DbSet;

        public GenericRepository(HotelDbContext dbContext ) {
        
            _Dbcontext = dbContext;
            _DbSet = _Dbcontext.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await _DbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _DbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> FindAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return await _DbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _DbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _DbSet.FindAsync(id);
        }

        public Task SaveAsync()
        {
            
            return _Dbcontext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
           _DbSet.Update(entity);
        }
    }
}
