using FalzoniNetFCSharp.Domain.DTO.Register;
using FalzoniNetFCSharp.Presentation.Api.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FalzoniNetFCSharp.Presentation.Api.Models.Register
{
    public class CustomerModel : BaseModel
    {
        public CustomerModel(CustomerDTO customerDTO)
        {
            this.Id = customerDTO.Id;
            this.Name = customerDTO.Name;
            this.DateBirth = customerDTO.DateBirth;
            this.Gender = customerDTO.Gender;
            this.PhoneNumber = customerDTO.PhoneNumber;
            this.CellPhoneNumber = customerDTO.CellPhoneNumber;
            this.Document = customerDTO.Document;
            this.Created = customerDTO.Created;
            this.Modified = customerDTO.Modified;
            this.Addresses = customerDTO.Addresses == null
            ? null
            : customerDTO.Addresses.ConvertAll(e => new CustomerAddressModel(e));
        }

        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string CellPhoneNumber { get; set; }
        public string Document { get; set; }

        public virtual List<CustomerAddressModel> Addresses { get; set; }


        #region Methods
        public CustomerDTO ConvertToDTO()
        {
            return new CustomerDTO
            {
                Id = this.Id,
                Name = this.Name,
                DateBirth = this.DateBirth,
                Gender = this.Gender,
                PhoneNumber = this.PhoneNumber,
                CellPhoneNumber = this.CellPhoneNumber,
                Document = this.Document,
                Created = this.Created,
                Modified = this.Modified,
                Addresses = this.Addresses == null
                ? null
                : this.Addresses.ToList().ConvertAll(e => new CustomerAddressDTO
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