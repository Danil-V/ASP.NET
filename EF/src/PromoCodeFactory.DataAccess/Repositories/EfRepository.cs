using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.DataAccess.EntityFramework;
using PromoCodeFactory.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;

namespace PromoCodeFactory.DataAccess.Repositories
{
    public class EfRepository <T> : IRepository<T> where T : BaseEntity {
        protected List<T> Data { get; set; }

        private readonly DatabaseContext _context;
        private readonly DbSet<T> _dbSet;
       

        public EfRepository(DatabaseContext context) {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstAsync(x => x.Id == id);
        }

        public async Task<T> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            if (includes != null && includes.Length > 0) {
                // Применяем Include для всех переданных навигационных свойств
                foreach (var include in includes) {
                    query = query.Include(include);
                }
                return await query.FirstOrDefaultAsync(e => e.Id == id);
            }
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> CreateAsync(T entity) {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id) {
            var entity = await GetByIdAsync(id);
            if (entity == null) 
                return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

