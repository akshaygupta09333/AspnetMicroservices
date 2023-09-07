using Ordering.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ordering.Domain.Entities;

namespace Ordering.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : EntityBase
    {
        Task<List<Order>> GetAllAsync();
        Task<List<Order>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<List<Order>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        string includeString = null,
                                        bool disableTracking = true);
        Task<List<Order>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                       Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                       List<Expression<Func<T, object>>> includes = null,
                                       bool disableTracking = true);
        Task<Order> GetByIdAsync(int id);
        Task<Order> AddAsync(Order entity);
        Task UpdateAsync(Order entity);
        Task DeleteAsync(Order entity);
    }
}