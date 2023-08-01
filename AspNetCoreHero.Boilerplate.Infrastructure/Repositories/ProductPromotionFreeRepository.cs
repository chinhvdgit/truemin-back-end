using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Repositories
{
    public class ProductPromotionFreeRepository : IProductPromotionFreeRepository
    {
        private readonly IRepositoryAsync<ProductPromotionFree> _repository;
        private readonly IDistributedCache _distributedCache;

        public ProductPromotionFreeRepository(IDistributedCache distributedCache, IRepositoryAsync<ProductPromotionFree> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<ProductPromotionFree> ProductPromotionFrees => _repository.Entities;

        public async Task DeleteAsync(ProductPromotionFree ProductPromotionFree)
        {
            await _repository.DeleteAsync(ProductPromotionFree);
            //await _distributedCache.RemoveAsync(CacheKeys.ProductPromotionFreeCacheKeys.ListKey);
            //await _distributedCache.RemoveAsync(CacheKeys.ProductPromotionFreeCacheKeys.GetKey(ProductPromotionFree.Id));
        }

        public async Task<ProductPromotionFree> GetByIdAsync(int ProductPromotionFreeId)
        {
            return await _repository.Entities.Where(p => p.Id == ProductPromotionFreeId).FirstOrDefaultAsync();
        }

        public async Task<List<ProductPromotionFree>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(ProductPromotionFree ProductPromotionFree)
        {
            await _repository.AddAsync(ProductPromotionFree);
            //await _distributedCache.RemoveAsync(CacheKeys.ProductPromotionFreeCacheKeys.ListKey);
            return ProductPromotionFree.Id;
        }

        public async Task UpdateAsync(ProductPromotionFree ProductPromotionFree)
        {
            await _repository.UpdateAsync(ProductPromotionFree);
            //await _distributedCache.RemoveAsync(CacheKeys.ProductPromotionFreeCacheKeys.ListKey);
            //await _distributedCache.RemoveAsync(CacheKeys.ProductPromotionFreeCacheKeys.GetKey(ProductPromotionFree.Id));
        }
    }
}