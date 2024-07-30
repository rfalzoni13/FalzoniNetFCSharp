using FalzoniNetFCSharp.Domain.DTO.Base;
using FalzoniNetFCSharp.Domain.Entities.Register;
using System;

namespace FalzoniNetFCSharp.Domain.DTO.Register
{
    public class CustomerAddressDTO : BaseDTO
    {
        public CustomerAddressDTO()
        {
        }

        public CustomerAddressDTO(CustomerAddress address)
        {
            this.Id = address.Id;
            this.CustomerId = address.CustomerId;
            this.PostalCode = address.PostalCode;
            this.AddressName = address.AddressName;
            this.Number = address.Number;
            this.Complement = address.Complement;
            this.Neighborhood = address.Neighborhood;
            this.City = address.City;
            this.State = address.State;
            this.Created = address.Created;
            this.Modified = address.Modified;
        }

        public Guid CustomerId { get; set; }

        public string PostalCode { get; set; }

        public string AddressName { get; set; }

        public int Number { get; set; }

        public string Complement { get; set; }

        public string Neighborhood { get; set; }

        public string City { get; set; }

        public string State { get; set; }


        public bool Removed { get; set; }


        #region Methods
        public void ConfigureNewEntity()
        {
            Id = Guid.NewGuid();
        }

        public CustomerAddress ConvertToEntity()
        {
            return new CustomerAddress
            {
                Id = this.Id,
                CustomerId = this.CustomerId,
                PostalCode = this.PostalCode,
                AddressName = this.AddressName,
                Number = this.Number,
                Complement = this.Complement,
                Neighborhood = this.Neighborhood,
                City = this.City,
                State = this.State,
                Created = this.Created,
                Modified = this.Modified
            };
        }

        #endregion
    }
}
