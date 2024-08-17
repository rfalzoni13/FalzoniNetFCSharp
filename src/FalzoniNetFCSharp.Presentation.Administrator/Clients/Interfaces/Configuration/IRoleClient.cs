using FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Base;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Configuration;
using System.Collections.Generic;

namespace FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Configuration
{
    public interface IRoleClient : IBaseClient<RoleModel>
    {
        List<string> GetAllNames();
    }
}
