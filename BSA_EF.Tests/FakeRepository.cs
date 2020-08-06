using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BSA_EF.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BSA_EF.BLL.Tests
{
    public class FakeRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        FakeSqlServerDbContext _context;
        DbSet<TEntity> _dbSet;

        public FakeRepository(FakeSqlServerDbContext context)
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
            await Task.Run(() =>
            {
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
            await Task.Run(() =>
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
                _dbSet.Remove(entity);
            });

        }
    }
}
