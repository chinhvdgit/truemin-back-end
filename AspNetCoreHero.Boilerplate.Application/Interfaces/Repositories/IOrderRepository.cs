using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }

        Task<List<Order>> GetListAsync();

        Task<Order> GetByIdAsync(int OrderId);

        Task<int> InsertAsync(Order Order);

        Task UpdateAsync(Order Order);

        Task DeleteAsync(Order Order);
    }
}