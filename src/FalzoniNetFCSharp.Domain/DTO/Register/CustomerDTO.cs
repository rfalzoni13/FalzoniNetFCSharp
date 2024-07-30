using FalzoniNetFCSharp.Domain.DTO.Base;
using FalzoniNetFCSharp.Domain.Entities.Register;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FalzoniNetFCSharp.Domain.DTO.Register
{
    public class CustomerDTO : BaseDTO
    {
        public CustomerDTO() 
        {
        }

        public CustomerDTO(Customer customer)
        {
            this.Id = customer.Id;
            this.Name = customer.Name;
            this.DateBirth = customer.DateBirth;
            this.Gender = customer.Gender;
            this.Email = customer.Email;
            this.PhoneNumber = customer.PhoneNumber;
            this.CellPhoneNumber = customer.CellPhoneNumber;
            this.Document = customer.Document;
            this.Created = customer.Created;
            this.Modified = customer.Modified;
            this.Addresses = customer.Addresses == null
            ? null
            : customer.Addresses.ToList().ConvertAll(e => new CustomerAddressDTO(e));
        }

        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CellPhoneNumber { get; set; }
        public string Document { get; set; }

        public virtual List<CustomerAddressDTO> Addresses { get; set; }


        #region Methods
        public void ConfigureNewEntity()
        {
            Id = Guid.NewGuid();
            Created = DateTime.Now;

            if (Addresses != null && Addresses.Count > 0)
            {
                Addresses.ForEach(e => 
                {
                    e.Id = Guid.NewGuid();
                    e.CustomerId = Id;
                    e.Created = DateTime.Now;
                });
            }
        }

        public Customer ConvertToEntity()
        {
            return new Customer
            {
                Id = this.Id,
                Name = this.Name,
                DateBirth = this.DateBirth,
                Gender = this.Gender,
                PhoneNumber = this.PhoneNumber,
                CellPhoneNumber = this.CellPhoneNumber,
                Document = this.Document,
                Email = this.Email,
                Created = this.Created,
                Modified = this.Modified,
                Addresses = this.Addresses == null
                ? null
                : this.Addresses.ToList().ConvertAll(e => new CustomerAddress
                {
                    Id = e.Id,
                    CustomerId = e.CustomerId,
                    PostalCode = e.PostalCode,
                    AddressName = e.AddressName,
                    Number = e.Number,
                    Complement = e.Complement,
                    Neighborhood = e.Neighborhood,
                    City = e.City,
                    State = e.State,
                    Created = e.Created,
                    Modified = e.Modified
                }),
            };
        }
        #endregion
    }
}
