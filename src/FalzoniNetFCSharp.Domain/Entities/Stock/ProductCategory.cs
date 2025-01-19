using FalzoniNetFCSharp.Domain.Entities.Base;

namespace FalzoniNetFCSharp.Domain.Entities.Stock
{
    public class ProductCategory : BaseEntity
    {
        public string Name { get; set; }


        public virtual Product Product { get; set; }
    }
}
