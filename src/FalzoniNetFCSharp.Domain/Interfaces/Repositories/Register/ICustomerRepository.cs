using FalzoniNetFCSharp.Domain.Entities.Register;
using FalzoniNetFCSharp.Domain.Interfaces.Repositories.Base;
using System;

namespace FalzoniNetFCSharp.Domain.Interfaces.Repositories.Register
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Customer GetWithInclude(Guid Id);
    }
}
