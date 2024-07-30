using FalzoniNetFCSharp.Domain.Entities.Base;
using FalzoniNetFCSharp.Domain.Entities.Stock;

namespace FalzoniNetFCSharp.Domain.Entities.Register
{
    public class ProductCategory : BaseEntity
    {
        public string Name { get; set; }


        public virtual Product Product { get; set; }
    }
}
