using FalzoniNetFCSharp.Domain.Entities.Register;
using FalzoniNetFCSharp.Domain.Interfaces.Register;
using FalzoniNetFCSharp.Infra.Data.Repositories.Base;

namespace FalzoniNetFCSharp.Infra.Data.Repositories.Register
{
    public class ProductCategoryRepository : BaseRepository<ProductCategory>, IProductCategoryRepository
    {
    }
}
