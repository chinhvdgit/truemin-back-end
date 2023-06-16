using AspNetCoreHero.Abstractions.Domain;

namespace AspNetCoreHero.Boilerplate.Domain.Entities.Catalog
{
    public class Product : AuditableEntity
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public decimal SalePrice { get; set; }
        public decimal RetailPrice { get; set; }
        public string ProductImg { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
    }
}