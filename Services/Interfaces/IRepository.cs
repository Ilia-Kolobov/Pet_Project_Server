using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSA_EF.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        public Task<IEnumerable<TEntity>> GetAsync();
        public Task<TEntity> GetAsync(int id);
        public Task CreateAsync(TEntity entity);
        public Task UpdateAsync(TEntity entity);
        public Task DeleteAsync(int id);
        public Task DeleteAsync(TEntity entity);
    }
}
