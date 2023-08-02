using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories
{
    public interface IOrderDetailRepository
    {
        IQueryable<OrderDetail> OrderDetails { get; }

        Task<List<OrderDetail>> GetListAsync();

        Task<OrderDetail> GetByIdAsync(int OrderDetailId);

        Task<int> InsertAsync(OrderDetail OrderDetail);

        Task UpdateAsync(OrderDetail OrderDetail);

        Task DeleteAsync(OrderDetail OrderDetail);
    }
}