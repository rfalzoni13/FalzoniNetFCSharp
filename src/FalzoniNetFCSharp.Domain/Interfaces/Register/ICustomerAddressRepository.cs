using FalzoniNetFCSharp.Domain.Entities.Register;
using FalzoniNetFCSharp.Domain.Interfaces.Base;
using System.Collections.Generic;
using System;

namespace FalzoniNetFCSharp.Domain.Interfaces.Register
{
    public interface ICustomerAddressRepository : IBaseRepository<CustomerAddress>
    {
        void RemoveRange(ICollection<Guid> ids);
    }
}
