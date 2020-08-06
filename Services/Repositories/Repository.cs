using System.Collections.Generic;
using System.Linq;
using BSA_EF.DAL.Interfaces;
using BSA_EF.DAL;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BSA_EF.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        DbContext _context;
        DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }
        public async Task<TEntity> GetAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => {
                _dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            });
           
        }
        public async Task DeleteAsync(int id)
        {
            TEntity entityToDelete = await _dbSet.FindAsync(id);
            _dbSet.Remove(entityToDelete);

        }
        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() => {
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
                _dbSet.Remove(entity);
            });
            
        }
    }
}
