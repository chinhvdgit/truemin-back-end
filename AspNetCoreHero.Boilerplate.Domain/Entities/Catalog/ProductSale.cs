using AspNetCoreHero.Abstractions.Domain;

namespace AspNetCoreHero.Boilerplate.Domain.Entities.Catalog
{
    public class ProductSale
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Type { get; set; }
        public decimal? Percent { get; set; }
        public decimal? Amount { get; set; }
        public virtual Product Product { get; set; }
    }
}