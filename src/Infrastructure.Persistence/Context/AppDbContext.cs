using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Application.Contracts.Interfaces;
using Core.Domain.Persistence.Common;
using Core.Domain.Persistence.Entities;
using Core.Domain.Persistence.Enums;
using Infrastructure.Persistence.Extensions;
using Infrastructure.Persistence.Helpers;
using LinqToDB.Data;
using LinqToDB.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUser _authenticatedUser;

        /// <summary>
        ///     Linq2Db instance of DbContext. Use it for bulk insert and bulk fetch.
        /// </summary>
        public DataConnection Linq2Db { get; }

        public AppDbContext(DbContextOptions<AppDbContext> options, IDateTimeService dateTime, IAuthenticatedUser authenticatedUser)
            : base(options)
        {
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
            Linq2Db = options.CreateLinqToDbConnection();
        }

        public DbSet<Audit> Audits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<PersonClientMapping>()
            //    .HasKey(o => new { o.ClientId, o.PersonId });

            //modelBuilder.Entity<ServiceDependency>()
            //    .HasKey(o => new { o.ServiceId, o.DependentServiceId });



            #region Soft delete setup
            foreach (var type in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(type.ClrType))
                    modelBuilder.SetSoftDeleteFilter(type.ClrType);
            }
            #endregion

            base.OnModelCreating(modelBuilder);
        }

        public async Task<int> SaveChangesAsync()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreationDate = _dateTime.NowUtc;
                        entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastUpdatedDate = _dateTime.NowUtc;
                        entry.Entity.LastUpdatedBy = _authenticatedUser.UserId;
                        break;
                }
            }

            if (_authenticatedUser.UserId != null) await AuditLogging();
            return await base.SaveChangesAsync();
        }

        public async Task AuditLogging()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                var auditEntry = new AuditEntry(entry)
                {
                    TableName = entry.Entity.GetType().Name,
                    UserId = _authenticatedUser.UserId,
                    UserName = _authenticatedUser.UserName
                };
                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    var propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }
            foreach (var auditEntry in auditEntries)
            {
                await Audits.AddAsync(auditEntry.ToAudit());
            }
        }
    }
}
