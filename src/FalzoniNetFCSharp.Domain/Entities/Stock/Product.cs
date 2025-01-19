using FalzoniNetFCSharp.Domain.Entities.Base;
using System;

namespace FalzoniNetFCSharp.Domain.Entities.Stock
{
    public class Product : BaseEntity
    {
        public Guid CategoryId { get; set; }

        public string Name { get; set; }

        public float Code { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }


        public virtual ProductCategory Category { get; set; }
    }
}
