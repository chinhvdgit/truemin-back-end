using System.Collections.Generic;

namespace AspNetCoreHero.Boilerplate.Application.Features.Products.Queries.GetById
{
    public class GetProductPromotionByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Percent { get; set; }
        public List<GetProductPromotionFreeResponse> product { get; set; }
    }
    public class GetProductPromotionFreeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Quantity { get; set; }
    }
}