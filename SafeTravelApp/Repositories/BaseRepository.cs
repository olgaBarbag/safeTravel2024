using Microsoft.EntityFrameworkCore;
using SafeTravelApp.Data;
using System;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SafeTravelApp.Repositories
{
    public abstract class BaseRepository<T, TKey> : IBaseRepository<T, TKey> where T : class
    {
        protected readonly SafeTravelAppDbContext context;
        protected readonly DbSet<T> dbSet;

        protected BaseRepository(SafeTravelAppDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

    //-----------------------------------------------------------------------------------------------
        public virtual async Task AddAsync(T entity) => await dbSet.AddAsync(entity);

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
            => await dbSet.AddRangeAsync(entities);

    //-----------------------------------------------------------------------------------------------
        public virtual Task UpdateAsync(T entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public virtual async Task<bool> DeleteAsync(TKey id)
        {
            T? existingEntity = await GetByIdAsync(id);
            if (existingEntity is null) return false;
            dbSet.Remove(existingEntity);
            return true;
        }
              
        
    //-----------------------------------------------------------------------------------------------

        public virtual async Task<T?> GetByIdAsync(TKey id) => await dbSet.FindAsync(id);

        public virtual async Task<IEnumerable<T>?> GetAllAsync() => await dbSet.ToListAsync();
           
        public virtual async Task<IEnumerable<T>?> FindFilteredAsync(List<Func<T, bool>> predicates)
        {

            if (predicates != null && predicates.Any())
            {
                return await dbSet.Where(u => predicates.All(predicate => predicate(u))).ToListAsync();
            }
            return await GetAllAsync();
        }

        //-----------------------------------------------------------------------------------------------

        public virtual async Task<int> GetCountAsync() => await dbSet.CountAsync();
    }
}
