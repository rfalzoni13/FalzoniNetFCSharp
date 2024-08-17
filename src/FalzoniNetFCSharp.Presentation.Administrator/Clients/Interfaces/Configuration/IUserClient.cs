using FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Base;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Configuration;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Tables.Configuration;
using System.Threading.Tasks;

namespace FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Configuration
{
    public interface IUserClient : IBaseClient<UserModel>
    {
        Task<UserTableModel> GetTableAsync(string url);
    }
}
