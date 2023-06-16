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
        public decimal RetailPrice { get; set; }
        public string ProductImg { get; set; }
        public int CategoryId { get; set; }
    }
}