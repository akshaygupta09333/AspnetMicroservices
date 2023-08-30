using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;

namespace Ordering.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : EntityBase
    {
        private readonly IConfiguration _configuration2;
        public RepositoryBase(IConfiguration configuration2)
        {
            _configuration2 = configuration2;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return null;
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return null;
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            return null;
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
            return null;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return null;
        }

        public async Task<T> AddAsync(T entity)
        {
            return null;
        }

        public async Task UpdateAsync(T entity)
        {
        }

        public async Task DeleteAsync(T entity)
        {

        }
    }
}
