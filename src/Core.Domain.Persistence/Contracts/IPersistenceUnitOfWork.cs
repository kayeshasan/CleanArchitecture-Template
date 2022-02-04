using System;
using System.Threading.Tasks;
using Core.Domain.Persistence.Entities;
using LinqToDB.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Core.Domain.Persistence.Contracts
{
    public interface IPersistenceUnitOfWork : IDisposable
    {
        

        /// <summary>
        ///     Linq2Db instance of current database. Use it for bulk insert and bulk fetch.
        /// </summary>
        DataConnection Linq2Db { get; }

        Task<int> SaveChangesAsync();

        Task<IDbContextTransaction> BeginTranscationAsync();

        Task CommitTransactionAsync();

        Task RollbackTransactionAsync();
    }
}
