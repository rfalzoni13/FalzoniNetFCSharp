using FalzoniNetFCSharp.Domain.Entities.Register;
using FalzoniNetFCSharp.Domain.Interfaces.Repositories.Base;
using System.Collections.Generic;
using System;

namespace FalzoniNetFCSharp.Domain.Interfaces.Repositories.Register
{
    public interface ICustomerAddressRepository : IBaseRepository<CustomerAddress>
    {
        void RemoveRange(ICollection<Guid> ids);
    }
}
