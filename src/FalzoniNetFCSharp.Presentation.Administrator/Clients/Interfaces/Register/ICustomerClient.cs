using FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Base;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Register;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Tables.Register;
using System.Threading.Tasks;

namespace FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Register
{
    public interface ICustomerClient : IBaseClient<CustomerModel>
    {
        Task<CustomerTableModel> GetTableAsync(string url);
    }
}
