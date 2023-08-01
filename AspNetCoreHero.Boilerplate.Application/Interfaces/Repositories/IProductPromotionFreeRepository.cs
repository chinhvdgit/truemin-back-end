using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories
{
    public interface IProductPromotionFreeRepository
    {
        IQueryable<ProductPromotionFree> ProductPromotionFrees { get; }

        Task<List<ProductPromotionFree>> GetListAsync();

        Task<ProductPromotionFree> GetByIdAsync(int ProductPromotionFreeId);

        Task<int> InsertAsync(ProductPromotionFree ProductPromotionFree);

        Task UpdateAsync(ProductPromotionFree ProductPromotionFree);

        Task DeleteAsync(ProductPromotionFree ProductPromotionFree);
    }
}