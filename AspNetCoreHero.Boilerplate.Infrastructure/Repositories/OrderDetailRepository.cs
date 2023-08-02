using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly IRepositoryAsync<OrderDetail> _repository;
        private readonly IDistributedCache _distributedCache;

        public OrderDetailRepository(IDistributedCache distributedCache, IRepositoryAsync<OrderDetail> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<OrderDetail> OrderDetails => _repository.Entities;

        public async Task DeleteAsync(OrderDetail OrderDetail)
        {
            await _repository.DeleteAsync(OrderDetail);
            //await _distributedCache.RemoveAsync(CacheKeys.OrderDetailCacheKeys.ListKey);
            //await _distributedCache.RemoveAsync(CacheKeys.OrderDetailCacheKeys.GetKey(OrderDetail.Id));
        }

        public async Task<OrderDetail> GetByIdAsync(int OrderDetailId)
        {
            return await _repository.Entities.Where(p => p.Id == OrderDetailId).FirstOrDefaultAsync();
        }

        public async Task<List<OrderDetail>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(OrderDetail OrderDetail)
        {
            await _repository.AddAsync(OrderDetail);
            //await _distributedCache.RemoveAsync(CacheKeys.OrderDetailCacheKeys.ListKey);
            return OrderDetail.Id;
        }

        public async Task UpdateAsync(OrderDetail OrderDetail)
        {
            await _repository.UpdateAsync(OrderDetail);
            //await _distributedCache.RemoveAsync(CacheKeys.OrderDetailCacheKeys.ListKey);
            //await _distributedCache.RemoveAsync(CacheKeys.OrderDetailCacheKeys.GetKey(OrderDetail.Id));
        }
    }
}