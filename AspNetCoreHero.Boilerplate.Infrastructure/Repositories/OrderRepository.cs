using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IRepositoryAsync<Order> _repository;
        private readonly IDistributedCache _distributedCache;

        public OrderRepository(IDistributedCache distributedCache, IRepositoryAsync<Order> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Order> Orders => _repository.Entities;

        public async Task DeleteAsync(Order Order)
        {
            await _repository.DeleteAsync(Order);
            //await _distributedCache.RemoveAsync(CacheKeys.OrderCacheKeys.ListKey);
            //await _distributedCache.RemoveAsync(CacheKeys.OrderCacheKeys.GetKey(Order.Id));
        }

        public async Task<Order> GetByIdAsync(int OrderId)
        {
            return await _repository.Entities.Where(p => p.Id == OrderId).FirstOrDefaultAsync();
        }

        public async Task<List<Order>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Order Order)
        {
            await _repository.AddAsync(Order);
            //await _distributedCache.RemoveAsync(CacheKeys.OrderCacheKeys.ListKey);
            return Order.Id;
        }

        public async Task UpdateAsync(Order Order)
        {
            await _repository.UpdateAsync(Order);
            //await _distributedCache.RemoveAsync(CacheKeys.OrderCacheKeys.ListKey);
            //await _distributedCache.RemoveAsync(CacheKeys.OrderCacheKeys.GetKey(Order.Id));
        }
    }
}