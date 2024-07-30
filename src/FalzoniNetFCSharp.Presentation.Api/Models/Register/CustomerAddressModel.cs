using FalzoniNetFCSharp.Domain.DTO.Register;
using FalzoniNetFCSharp.Presentation.Api.Models.Base;
using System;

namespace FalzoniNetFCSharp.Presentation.Api.Models.Register
{
    public class CustomerAddressModel : BaseModel
    {
        public CustomerAddressModel(CustomerAddressDTO addressDTO)
        {
            this.Id = addressDTO.Id;
            this.CustomerId = addressDTO.CustomerId;
            this.PostalCode = addressDTO.PostalCode;
            this.AddressName = addressDTO.AddressName;
            this.Number = addressDTO.Number;
            this.Complement = addressDTO.Complement;
            this.Neighborhood = addressDTO.Neighborhood;
            this.City = addressDTO.City;
            this.State = addressDTO.State;
            this.Created = addressDTO.Created;
            this.Modified = addressDTO.Modified;
            this.Removed = addressDTO.Removed;
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
        public CustomerAddressDTO ConvertToDTO()
        {
            return new CustomerAddressDTO
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
                Modified = this.Modified,
                Removed = this.Removed,
            };
        }
        #endregion
    }
}