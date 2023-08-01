using AspNetCoreHero.Abstractions.Domain;
using System.Collections;
using System.Collections.Generic;

namespace AspNetCoreHero.Boilerplate.Domain.Entities.Catalog
{
    public class ProductPromotion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        public int? Type { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Percent { get; set; }
        public Product Product { get; set; }
    }
}