using FalzoniNetFCSharp.Domain.Entities.Register;
using FalzoniNetFCSharp.Test.Utils.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FalzoniNetFCSharp.Test.Utils.Data.Register
{
    public class CustomerAddressData : BaseData<CustomerAddress>
    {
        public override IQueryable<CustomerAddress> GetData()
        {
            return GetList().AsQueryable();
        }

        public override CustomerAddress GetItem(Guid id)
        {
            return GetList().FirstOrDefault(x => x.Id == id);
        }

        public override CustomerAddress CreateObject()
        {
            return new CustomerAddress
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.Parse("282bde21-8fcc-4230-9a2b-3ec0178d5eb1"),
                AddressName = "Rua do Paraíso",
                Number = 1000,
                Complement = "5 ANDAR AP 55",
                Neighborhood = "Tatuapé",
                City = "São Paulo",
                State = "SP",
                PostalCode = "022100-040",
                Created = DateTime.Now
            };
        }

        public override CustomerAddress UpdateObject(CustomerAddress obj)
        {
            obj.CustomerId = Guid.Parse("c8b3a5a3-4321-47bd-817a-4f63e0b06845");
            obj.AddressName = "Avenida dos Marabáis";
            obj.Number = 44;
            obj.Complement = null;
            obj.Neighborhood = "São Geraldo";
            obj.City = "Minas Gerais";
            obj.State = "MG";
            obj.PostalCode = "32210-040";
            obj.Modified = DateTime.Now;

            return obj;
        }

        public override Guid GetGuidFromList()
        {
            List<CustomerAddress> list = GetList();
            Random rnd = new Random();
            int i = rnd.Next(list.Count());
            return list[i].Id;
        }

        public override Guid GetGuidWithoutInclude()
        {
            return GetGuidFromList();
        }

        #region Self Methods
        private List<CustomerAddress> GetList()
        {
            return new List<CustomerAddress>
            {
                new CustomerAddress
                {
                    Id = Guid.Parse("a05d9fb4-f19d-432a-b243-719e87876194"),
                    CustomerId = Guid.Parse("c8b3a5a3-4321-47bd-817a-4f63e0b06845"),
                    AddressName = "Rua dos Anfíbios",
                    Number = 34,
                    Complement = null,
                    Neighborhood = "Jardim Suspenso",
                    City = "Rio de Janeiro",
                    State = "RJ",
                    PostalCode = "02212-222",
                    Created = DateTime.Now
                },
                new CustomerAddress
                {
                    Id = Guid.Parse("775cc393-431a-4a32-a043-ee4d75a4493f"),
                    CustomerId = Guid.Parse("282bde21-8fcc-4230-9a2b-3ec0178d5eb1"),
                    AddressName = "Rua das Fofoqueiras",
                    Number = 14,
                    Complement = "BL B AP 49",
                    Neighborhood = "Cubatão",
                    City = "Botucatu",
                    State = "SP",
                    PostalCode = "04556-010",
                    Created = DateTime.Now
                },
                new CustomerAddress
                {
                    Id = Guid.Parse("51b55915-986c-4069-a2cd-b621a41552e9"),
                    CustomerId = Guid.Parse("47a5d987-d6b0-4ad9-9afc-c50eebfdcc6c"),
                    AddressName = "Rod. Fernão Dias Km 122",
                    Number = 0,
                    Complement = "S/N KM 22",
                    Neighborhood = "Meio do Nada",
                    City = "Camanducaia",
                    State = "MG",
                    PostalCode = "09881-000",
                    Created = DateTime.Now
                }
            };
        }
        #endregion
    }
}
