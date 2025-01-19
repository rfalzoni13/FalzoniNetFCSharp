using FalzoniNetFCSharp.Domain.Entities.Register;
using FalzoniNetFCSharp.Domain.Interfaces.Repositories.Register;
using FalzoniNetFCSharp.Infra.Data.Context;
using FalzoniNetFCSharp.Infra.Data.Repositories.Base;
using System;
using System.Data.Entity;
using System.Linq;

namespace FalzoniNetFCSharp.Infra.Data.Repositories.Register
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository()
            :base()
        {
        }

        public CustomerRepository(FalzoniContext falzoniContext)
            :base(falzoniContext)
        {
        }

        public Customer GetWithInclude(Guid Id)
        {
            return context.Set<Customer>()
            .Include(x => x.Addresses)
            .FirstOrDefault(x => x.Id == Id);   
        }
    }
}
