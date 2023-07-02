using AspNetCoreHero.Abstractions.Domain;
using System.Collections.Generic;

namespace AspNetCoreHero.Boilerplate.Domain.Entities.Catalog
{
    public class Category : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public decimal Tax { get; set; }
        public IList<ProductCategory> ProductCategories { get; set; }

    }
}