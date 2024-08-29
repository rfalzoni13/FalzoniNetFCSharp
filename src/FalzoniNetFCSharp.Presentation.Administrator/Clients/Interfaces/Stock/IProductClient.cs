using FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Base;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Stock;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Tables.Stock;
using System.Threading.Tasks;

namespace FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Stock
{
    public interface IProductClient : IBaseClient<ProductModel>
    {
        Task<ProductTableModel> GetTableAsync(string url);
    }
}