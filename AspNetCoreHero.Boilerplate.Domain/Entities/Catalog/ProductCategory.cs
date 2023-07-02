using AspNetCoreHero.Abstractions.Domain;

namespace AspNetCoreHero.Boilerplate.Domain.Entities.Catalog
{
    public class ProductCategory 
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Category Category { get; set; }
    }
}