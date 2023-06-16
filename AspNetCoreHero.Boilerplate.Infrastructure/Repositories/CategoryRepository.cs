using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IRepositoryAsync<Category> _repository;
        private readonly IDistributedCache _distributedCache;

        public CategoryRepository(IDistributedCache distributedCache, IRepositoryAsync<Category> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Category> Categorys => _repository.Entities;

        public async Task DeleteAsync(Category Category)
        {
            await _repository.DeleteAsync(Category);
            await _distributedCache.RemoveAsync(CacheKeys.CategoryCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.CategoryCacheKeys.GetKey(Category.Id));
        }

        public async Task<Category> GetByIdAsync(int CategoryId)
        {
            return await _repository.Entities.Where(p => p.Id == CategoryId).FirstOrDefaultAsync();
        }

        public async Task<List<Category>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Category Category)
        {
            await _repository.AddAsync(Category);
            await _distributedCache.RemoveAsync(CacheKeys.CategoryCacheKeys.ListKey);
            return Category.Id;
        }

        public async Task UpdateAsync(Category Category)
        {
            await _repository.UpdateAsync(Category);
            await _distributedCache.RemoveAsync(CacheKeys.CategoryCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.CategoryCacheKeys.GetKey(Category.Id));
        }
    }
}