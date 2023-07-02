using AspNetCoreHero.Abstractions.Domain;
using System.Collections;
using System.Collections.Generic;

namespace AspNetCoreHero.Boilerplate.Domain.Entities.Catalog
{
    public class Product : AuditableEntity
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public byte[] ImageByte { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
        public IList<ProductCategory> ProductCategories { get; set; }
        public virtual ProductSale Sale { get; set; }

    }
}