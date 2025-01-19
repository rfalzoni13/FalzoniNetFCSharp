using FalzoniNetFCSharp.Domain.Entities.Register;
using FalzoniNetFCSharp.Domain.Interfaces.Repositories.Register;
using FalzoniNetFCSharp.Infra.Data.Context;
using FalzoniNetFCSharp.Infra.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FalzoniNetFCSharp.Infra.Data.Repositories.Register
{
    public class CustomerAddressRepository : BaseRepository<CustomerAddress>, ICustomerAddressRepository
    {
        public CustomerAddressRepository() 
            :base()
        {
        }

        public CustomerAddressRepository(FalzoniContext falzoniContext)
            :base(falzoniContext)
        {
        }

        public void RemoveRange(ICollection<Guid> ids)
        {
            ICollection<CustomerAddress> enderecos = context.Set<CustomerAddress>().Where(x => !ids.Contains(x.Id)).ToList();
            if (enderecos.Any())
            {
                context.Set<CustomerAddress>().RemoveRange(enderecos);
                context.SaveChanges();
            }
        }
    }
}
