using System.Collections.Generic;
using System.Threading.Tasks;
using AutoBogus;
using Core.Domain.Persistence.Common;
using Core.Domain.Persistence.Contracts;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories
{
    public class FakeRepositoryAsync<T> : RepositoryAsync<T> where T : BaseEntity
    {
        private readonly AppDbContext _dbContext;
        public FakeRepositoryAsync(AppDbContext appDbContext) : base(appDbContext)
        {
            _dbContext = appDbContext;
        }

        /// <summary>
        /// Get some fake data for the given type T
        /// </summary>
        /// <returns></returns>
        //public override async Task<IReadOnlyList<T>> GetAllAsync()
        //{
        //    var faker = new AutoFaker<T>();
        //    return faker.Generate(1000);
        //}
    }
}