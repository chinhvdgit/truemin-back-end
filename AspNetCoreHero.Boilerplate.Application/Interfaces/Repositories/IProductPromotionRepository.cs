using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories
{
    public interface IProductPromotionRepository
    {
        IQueryable<ProductPromotion> ProductPromotions { get; }

        Task<List<ProductPromotion>> GetListAsync();

        Task<ProductPromotion> GetByIdAsync(int ProductPromotionId);

        Task<int> InsertAsync(ProductPromotion ProductPromotion);

        Task UpdateAsync(ProductPromotion ProductPromotion);

        Task DeleteAsync(ProductPromotion ProductPromotion);
    }
}