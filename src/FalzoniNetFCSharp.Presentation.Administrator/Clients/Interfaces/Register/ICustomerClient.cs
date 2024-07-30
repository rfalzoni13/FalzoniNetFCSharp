using FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Base;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Register;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Tables.Register;

namespace FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Register
{
    public interface ICustomerClient : IBaseClient<CustomerModel, CustomerTableModel>
    {
    }
}
