
using AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using AspNetCoreHero.Boilerplate.Infrastructure.CacheKeys;
using AspNetCoreHero.Extensions.Caching;
using AspNetCoreHero.ThrowR;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Infrastructure.CacheRepositories
{
    public class CategoryCacheRepository : ICategoryCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ICategoryRepository _CategoryRepository;

        public CategoryCacheRepository(IDistributedCache distributedCache, ICategoryRepository CategoryRepository)
        {
            _distributedCache = distributedCache;
            _CategoryRepository = CategoryRepository;
        }

        public async Task<Category> GetByIdAsync(int CategoryId)
        {
            string cacheKey = CategoryCacheKeys.GetKey(CategoryId);
            var Category = await _distributedCache.GetAsync<Category>(cacheKey);
            if (Category == null)
            {
                Category = await _CategoryRepository.GetByIdAsync(CategoryId);
                Throw.Exception.IfNull(Category, "Category", "No Category Found");
                await _distributedCache.SetAsync(cacheKey, Category);
            }
            return Category;
        }

        public async Task<List<Category>> GetCachedListAsync()
        {
            string cacheKey = CategoryCacheKeys.ListKey;
            var CategoryList = await _distributedCache.GetAsync<List<Category>>(cacheKey);
            if (CategoryList == null)
            {
                CategoryList = await _CategoryRepository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, CategoryList);
            }
            return CategoryList;
        }
    }
}