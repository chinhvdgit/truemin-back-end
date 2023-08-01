using AspNetCoreHero.Abstractions.Domain;
using System.Collections;
using System.Collections.Generic;

namespace AspNetCoreHero.Boilerplate.Domain.Entities.Catalog
{
    public class ProductPromotionFree
    {
        public int Id { get; set; }
        public int ProductPromotionId { get; set; }
        public int ProductId { get; set; }
        public decimal? Quantity { get; set; }
        public Product Product { get; set; }
    }
}