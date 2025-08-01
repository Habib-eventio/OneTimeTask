using CamcoTasks.Infrastructure.Entities.TaskInfo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CamcoTasks.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(List<TEntity> entities);
        Task<List<TEntity>> GetListAsync();
        Task<TEntity?> GetAsync(long id);
        Task<TEntity?> GetAsync(short id);
        Task<TEntity?> GetAsync(string id);
        Task<TEntity?> GetAsync(int id);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync();
        Task<List<TEntity>> GetAllUsingSkipAndTakeAsync(int skip, int limit);
        Task<List<TEntity>> GetAllAsNoTrackingUsingSkipAndTakeAsync(int skip, int limit);
        //Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> FindAllAsNoTrackingAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> FindAllWithOrderByAscendingAsync<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderExpression);
        Task<List<TEntity>> FindAllWithOrderByDescendingAsync<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderExpression);
        Task RemoveAsync(TEntity entity);
        Task RemoveRange(IEnumerable<TEntity> entities);
        Task<TProperty?> GetMaxAsync<TProperty>(Expression<Func<TEntity, TProperty>> selector);
        Task<TProperty?> GetMaxWhereAsync<TProperty>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> selector);
        Task<TProperty?> GetMinAsync<TProperty>(Expression<Func<TEntity, TProperty>> selector);
        Task<TProperty?> GetMinWhereAsync<TProperty>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> selector);
        IQueryable<TEntity> GetQueryable();
#pragma warning disable CS0672 // for obsolete members
        Task UpdateAsync(TEntity entity);
        void Add(TEntity entity);
        List<TEntity> GetList();
        Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> GetAllUsingSkipAndTake(int skip, int limit);
        List<TEntity> GetAllAsNoTrackingUsingSkipAndTake(int skip, int limit);
        List<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);
        TEntity? Find(Expression<Func<TEntity, bool>> predicate);
        
#pragma warning restore CS0672
    }
}
