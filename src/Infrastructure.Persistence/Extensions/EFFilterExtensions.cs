using System;
using System.Linq;
using System.Reflection;
using Core.Domain.Persistence.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Extensions
{
    public static class EFFilterExtensions
    {
        public static void SetSoftDeleteFilter(this ModelBuilder modelBuilder, Type entityType)
        {
            SetSoftDeleteFilterMethod.MakeGenericMethod(entityType)
                .Invoke(null, new object[] { modelBuilder });
        }

        static readonly MethodInfo SetSoftDeleteFilterMethod = typeof(EFFilterExtensions)
                   .GetMethods(BindingFlags.Public | BindingFlags.Static)
                   .Single(t => t.IsGenericMethod && t.Name == "SetSoftDeleteFilter");

        public static void SetSoftDeleteFilter<TEntity>(this ModelBuilder modelBuilder)
            where TEntity : BaseEntity
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(x => x.Archived != true);
        }
    }
}