using FalzoniNetFCSharp.Domain.Entities.Stock;
using FalzoniNetFCSharp.Domain.Interfaces.Repositories.Stock;
using FalzoniNetFCSharp.Infra.Data.Context;
using FalzoniNetFCSharp.Infra.Data.Repositories.Base;

namespace FalzoniNetFCSharp.Infra.Data.Repositories.Stock
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository()
            :base()
        {
        }

        public ProductRepository(FalzoniContext falzoniContext) 
            :base(falzoniContext)
        {
        }
    }
}
