using FalzoniNetFCSharp.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace FalzoniNetFCSharp.Domain.Entities.Register
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CellPhoneNumber { get; set; }
        public string Document { get; set; }

        public virtual ICollection<CustomerAddress> Addresses { get; set; }
    }
}
