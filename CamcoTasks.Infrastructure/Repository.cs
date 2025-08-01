using CamcoTasks.Infrastructure.Entities.TaskInfo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CamcoTasks.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DatabaseContext Context;

        public Repository(DatabaseContext context)
        {
            Context = context;
        }

        public Task AddAsync(T entity)
        {
            Context.Set<T>().Add(entity);
            return Task.CompletedTask;
        }

        public Task AddRangeAsync(List<T> entities)
        {
            Context.Set<T>().AddRange(entities);
            return Task.CompletedTask;
        }

        public Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate).ToListAsync();
        }

        public Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public Task<T?> GetAsync(int id) => Context.Set<T>().FindAsync(id).AsTask();
        public Task<T?> GetAsync(long id) => Context.Set<T>().FindAsync(id).AsTask();
        public Task<T?> GetAsync(short id) => Context.Set<T>().FindAsync(id).AsTask();
        public Task<T?> GetAsync(string id) => Context.Set<T>().FindAsync(id).AsTask();

        public Task RemoveAsync(T entity)
        {
            Context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public Task<List<T>> GetListAsync()
        {
            return Context.Set<T>().ToListAsync();
        }

        public Task<List<T>> FindAllAsNoTrackingAsync(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate).AsNoTracking().ToListAsync();
        }

        public Task<int> CountAsync() => Context.Set<T>().CountAsync();

        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) => Context.Set<T>().AnyAsync(predicate);

        public Task<List<T>> GetAllUsingSkipAndTakeAsync(int skip, int limit) => Context.Set<T>().Skip(skip).Take(limit).ToListAsync();

        public Task<List<T>> GetAllAsNoTrackingUsingSkipAndTakeAsync(int skip, int limit) => Context.Set<T>().AsNoTracking().Skip(skip).Take(limit).ToListAsync();

        public Task<List<T>> FindAllWithOrderByAscendingAsync<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderExpression)
        {
            return Context.Set<T>().Where(predicate).OrderBy(orderExpression).ToListAsync();
        }

        public Task<List<T>> FindAllWithOrderByDescendingAsync<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderExpression)
        {
            return Context.Set<T>().Where(predicate).OrderByDescending(orderExpression).ToListAsync();
        }

        public Task RemoveRange(IEnumerable<T> entities)
        {
            Context.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }

        public Task<TProperty?> GetMaxAsync<TProperty>(Expression<Func<T, TProperty>> selector)
        {
            return Context.Set<T>().MaxAsync(selector);
        }

        public Task<TProperty?> GetMaxWhereAsync<TProperty>(Expression<Func<T, bool>> predicate, Expression<Func<T, TProperty>> selector)
        {
            return Context.Set<T>().Where(predicate).MaxAsync(selector);
        }

        public Task<TProperty?> GetMinAsync<TProperty>(Expression<Func<T, TProperty>> selector)
        {
            return Context.Set<T>().MinAsync(selector);
        }

        public Task<TProperty?> GetMinWhereAsync<TProperty>(Expression<Func<T, bool>> predicate, Expression<Func<T, TProperty>> selector)
        {
            return Context.Set<T>().Where(predicate).MinAsync(selector);
        }

        public IQueryable<T> GetQueryable() => Context.Set<T>();

        [Obsolete]
        public Task UpdateAsync(T entity)
        {
            Context.Set<T>().Update(entity);
            return Task.CompletedTask;
        }

        [Obsolete]
        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        [Obsolete]
        public List<T> GetList()
        {
            return Context.Set<T>().ToList();
        }

        [Obsolete]
        public List<T> GetAllUsingSkipAndTake(int skip, int limit)
        {
            return Context.Set<T>().Skip(skip).Take(limit).ToList();
        }

        [Obsolete]
        public List<T> GetAllAsNoTrackingUsingSkipAndTake(int skip, int limit)
        {
            return Context.Set<T>().AsNoTracking().Skip(skip).Take(limit).ToList();
        }

        [Obsolete]
        public List<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate).ToList();
        }

        [Obsolete]
        public T? Find(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().FirstOrDefault(predicate);
        }
        public Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().CountAsync(predicate);
        }
    }
}
