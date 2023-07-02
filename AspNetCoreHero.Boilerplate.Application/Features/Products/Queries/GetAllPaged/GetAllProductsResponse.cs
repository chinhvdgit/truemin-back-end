using System.Collections.Generic;

namespace AspNetCoreHero.Boilerplate.Application.Features.Products.Queries.GetAllPaged
{
    public class GetAllProductsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public ProductSaleResponse Sale { get; set; }
        public List<int> CategoryId { get; set; }
    }
}