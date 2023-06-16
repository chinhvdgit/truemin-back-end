
using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories
{
    public interface ICategoryCacheRepository
    {
        Task<List<Category>> GetCachedListAsync();

        Task<Category> GetByIdAsync(int CategoryId);
    }
}