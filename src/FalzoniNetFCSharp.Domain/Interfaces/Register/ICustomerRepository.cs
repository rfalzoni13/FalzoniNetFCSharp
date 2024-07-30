using FalzoniNetFCSharp.Domain.Entities.Register;
using FalzoniNetFCSharp.Domain.Interfaces.Base;
using System;

namespace FalzoniNetFCSharp.Domain.Interfaces.Register
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Customer GetWithInclude(Guid Id);
    }
}
