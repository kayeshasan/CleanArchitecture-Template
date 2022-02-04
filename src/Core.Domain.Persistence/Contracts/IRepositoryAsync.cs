using Core.Domain.Persistence.Common;
using Core.Domain.Persistence.Entities;
using Core.Domain.Persistence.Enums;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Domain.Persistence.Contracts
{
    public interface IRepositoryAsync<T> where T : BaseEntity
    {
        /// <summary>
        ///     Iqueryable entity of Entity Framework. Use this to execute query in database level.
        /// </summary>
        IQueryable<T> Entity { get; }

        Task<T> GetByIdAsync(int id);

        Task<int> CountTotalAsync();

        Task<T> AddAsync(T entity);

        Task<List<T>> AddAsync(List<T> entity);

        Task UpdateAsync(T entity);
        Task UpdateAsync(List<T> entity);

        Task DeleteAsync(T entity);
        Task DeleteAsync(List<T> entity);

        IQueryable<T> AsQueryable();
        IQueryable<T> AsNoTracking();
        IEnumerable<T> Where(Expression<Func<T, bool>> predicate);
        IEnumerable<T> AsEnumerable();
    }
}
