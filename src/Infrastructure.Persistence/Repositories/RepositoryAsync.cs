using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using AutoBogus;
using Bogus;
using Core.Domain.Persistence.Common;
using Core.Domain.Persistence.Contracts;
using Core.Domain.Persistence.Entities;
using Core.Domain.Persistence.Enums;
using Infrastructure.Persistence.Context;
//using LinqToDB;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence.Repositories
{
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : BaseEntity
    {
        private readonly AppDbContext _dbContext;

        public RepositoryAsync(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public virtual IQueryable<T> Entity => _dbContext.Set<T>();

        //public virtual LinqToDB.ITable<T> Table => _dbContext.Linq2Db.GetTable<T>();

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<int> CountTotalAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().CountAsync();
        }

        //public virtual async Task<IReadOnlyList<T>> GetAllAsync()
        //{
        //    return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        //}

        public virtual IQueryable<T> AsQueryable()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        //public virtual async Task<IReadOnlyList<T>> GetPagedAsync(int pageNumber, int drawPerPage, string searchQuery, string orderBy = null, OrderTypeEnum? orderType = null)
        //{
        //    var query = _dbContext.Set<T>().AsQueryable();
        //    if (!string.IsNullOrWhiteSpace(searchQuery))
        //    {
        //        searchQuery = searchQuery.Trim().ToLower();
        //        //query = query.Where(w => w.GetType().GetProperties().Any(f => f.GetValue(w) != null && f.GetValue(w).ToString().ToLower().Contains(searchQuery))).AsQueryable();
        //        if (orderBy != null)
        //        {
        //            if (orderType == OrderTypeEnum.desc)
        //                query = query.OrderByDescending(o => o.GetType().GetProperty(orderBy));
        //            else
        //                query = query.OrderBy(o => o.GetType().GetProperty(orderBy));
        //        }
        //    }
        //    query = query.Skip((pageNumber-1)* drawPerPage).Take(drawPerPage);
        //    return await EntityFrameworkQueryableExtensions.ToListAsync(query);
        //}

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public virtual async Task<List<T>> AddAsync(List<T> entity)
        {
            await _dbContext.Set<T>().AddRangeAsync(entity);
            return entity;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).CurrentValues.SetValues(entity);
            await Task.CompletedTask;
        }

        public virtual async Task UpdateAsync(List<T> entity)
        {
            foreach (var v in entity)
            {
                _dbContext.Entry(v).CurrentValues.SetValues(v);
            }
            await Task.CompletedTask;
        }

        public virtual async Task DeleteAsync(T entity)
        {
            entity.Archived = true;
            //_dbContext.Set<T>().Remove(entity);
            _dbContext.Entry(entity).CurrentValues.SetValues(entity);
            await Task.CompletedTask;
        }
        public virtual async Task DeleteAsync(List<T> entity)
        {
            foreach (var a in entity)
            {
                a.Archived = true;
                _dbContext.Entry(a).CurrentValues.SetValues(a);
            }
            await Task.CompletedTask;
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate);
        }

        public IEnumerable<T> AsEnumerable()
        {
            return _dbContext.Set<T>();
        }

        public IQueryable<T> AsNoTracking()
        {
            return _dbContext.Set<T>().AsNoTracking();
        }
    }
}
