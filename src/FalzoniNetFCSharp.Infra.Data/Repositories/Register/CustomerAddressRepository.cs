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
        public void RemoveRange(ICollection<Guid> ids)
        {
            using (var context = FalzoniContext.Create())
            {
                ICollection<CustomerAddress> enderecos = context.CustomerAddress.Where(x => !ids.Contains(x.Id)).ToList();
                if (enderecos.Any())
                {
                    context.CustomerAddress.RemoveRange(enderecos);
                    context.SaveChanges();
                }
            }
        }
    }
}
