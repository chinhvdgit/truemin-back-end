using AspNetCoreHero.Abstractions.Domain;
using System.Collections;
using System.Collections.Generic;

namespace AspNetCoreHero.Boilerplate.Domain.Entities.Catalog
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? OrderPrice { get; set; }
        public decimal? Amount { get; set; }
        public bool IsFree { get; set; }
        public Order Order { get; set; }
    }
}