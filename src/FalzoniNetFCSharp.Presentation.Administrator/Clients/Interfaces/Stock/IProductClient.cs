using FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Base;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Stock;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Tables.Stock;

namespace FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Stock
{
    public interface IProductClient : IBaseClient<ProductModel, ProductTableModel>
    {
    }
}