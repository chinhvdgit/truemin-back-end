using System.Collections.Generic;

namespace AspNetCoreHero.Boilerplate.Application.Features.Products.Queries.GetAllPaged
{
    public class ProductSaleResponse
    {
        public string Type { get; set; }
        public decimal? Percent { get; set; }
        public decimal? Amount { get; set; }
    }
}