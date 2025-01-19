using FalzoniNetFCSharp.Domain.Entities.Stock;
using FalzoniNetFCSharp.Domain.Interfaces.Repositories.Stock;
using FalzoniNetFCSharp.Infra.Data.Context;
using FalzoniNetFCSharp.Infra.Data.Repositories.Base;

namespace FalzoniNetFCSharp.Infra.Data.Repositories.Stock
{
    public class ProductCategoryRepository : BaseRepository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository()
            : base()
        {
        }

        public ProductCategoryRepository(FalzoniContext falzoniContext)
            : base(falzoniContext)
        {
        }
    }
}
