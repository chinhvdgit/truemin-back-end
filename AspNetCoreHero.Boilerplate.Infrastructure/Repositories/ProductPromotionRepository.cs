using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Repositories
{
    public class ProductPromotionRepository : IProductPromotionRepository
    {
        private readonly IRepositoryAsync<ProductPromotion> _repository;
        private readonly IDistributedCache _distributedCache;

        public ProductPromotionRepository(IDistributedCache distributedCache, IRepositoryAsync<ProductPromotion> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<ProductPromotion> ProductPromotions => _repository.Entities;

        public async Task DeleteAsync(ProductPromotion ProductPromotion)
        {
            await _repository.DeleteAsync(ProductPromotion);
            //await _distributedCache.RemoveAsync(CacheKeys.ProductPromotionCacheKeys.ListKey);
            //await _distributedCache.RemoveAsync(CacheKeys.ProductPromotionCacheKeys.GetKey(ProductPromotion.Id));
        }

        public async Task<ProductPromotion> GetByIdAsync(int ProductPromotionId)
        {
            return await _repository.Entities.Where(p => p.Id == ProductPromotionId).FirstOrDefaultAsync();
        }

        public async Task<List<ProductPromotion>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(ProductPromotion ProductPromotion)
        {
            await _repository.AddAsync(ProductPromotion);
            //await _distributedCache.RemoveAsync(CacheKeys.ProductPromotionCacheKeys.ListKey);
            return ProductPromotion.Id;
        }

        public async Task UpdateAsync(ProductPromotion ProductPromotion)
        {
            await _repository.UpdateAsync(ProductPromotion);
            //await _distributedCache.RemoveAsync(CacheKeys.ProductPromotionCacheKeys.ListKey);
            //await _distributedCache.RemoveAsync(CacheKeys.ProductPromotionCacheKeys.GetKey(ProductPromotion.Id));
        }
    }
}