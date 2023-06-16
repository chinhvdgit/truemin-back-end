using AspNetCoreHero.Abstractions.Domain;

namespace AspNetCoreHero.Boilerplate.Domain.Entities.Catalog
{
    public class Category : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Tax { get; set; }
    }
}