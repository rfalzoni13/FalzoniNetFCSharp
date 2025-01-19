using FalzoniNetFCSharp.Domain.Entities.Register;
using FalzoniNetFCSharp.Test.Utils.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FalzoniNetFCSharp.Test.Utils.Data.Register
{
    public class CustomerData : BaseData<Customer>
    {
        public override IQueryable<Customer> GetData()
        {
            return GetList().AsQueryable();
        }

        public override Customer GetItem(Guid id)
        {
            return GetList().FirstOrDefault(x => x.Id == id);
        }

        public override Customer CreateObject()
        {
            Guid customerId = Guid.NewGuid();

            return new Customer
            {
                Id = customerId,
                Name = "Hyoga de Cisne",
                Document = "767.211.090-14",
                PhoneNumber = "(11) 2334-1212",
                CellPhoneNumber = "(11) 98787-1234",
                DateBirth = new DateTime(2024, 10, 5),
                Email = "cisnehyoga@cdz.com.br",
                Gender = "Masculino",
                Created = DateTime.Now,
                Addresses = new List<CustomerAddress>
                {
                    new CustomerAddress
                    {
                        Id = Guid.NewGuid(),
                        CustomerId = customerId,
                        AddressName = "Rua dos Siberianos",
                        Number = 10,
                        Complement = "BL de Gelo",
                        Neighborhood = "Jardim do Santuário",
                        City = "São Paulo",
                        State = "SP",
                        PostalCode = "04554-010",
                        Created = DateTime.Now
                    }
                }
            };
        }

        public override Customer UpdateObject(Customer obj)
        {
            Guid customerId = obj.Id;
            obj.Name = "Seiya de Pégaso";
            obj.Document = "123.123.123-45";
            obj.Email = "seiyadepegaso@saintseiya.com";
            obj.Modified = DateTime.Now;
            obj.Addresses.Add(
                new CustomerAddress
                {
                    Id = Guid.NewGuid(),
                    CustomerId = customerId,
                    AddressName = "Avenida Grega",
                    Number = 20090,
                    Complement = "BL S AP 13",
                    Neighborhood = "Jardim do Santuário",
                    City = "São Paulo",
                    State = "SP",
                    PostalCode = "04554-010",
                    Created = DateTime.Now
                });

            return obj;
        }

        public override Guid GetGuidFromList()
        {
            List<Customer> list = GetList();
            list = list.Where(x => x.Addresses.Count() > 0).ToList();
            Random rnd = new Random();
            int i = rnd.Next(list.Count());
            return list[i].Id;
        }

        public override Guid GetGuidWithoutInclude()
        {
            List<Customer> list = GetList();
            list = list.Where(x => x.Addresses.Count() == 0).ToList();
            Random rnd = new Random();
            int i = rnd.Next(list.Count());
            return list[i].Id;
        }

        #region Self Methods
        private List<Customer> GetList()
        {
            return new List<Customer>
            {
                new Customer
                {
                    Id = Guid.Parse("c8b3a5a3-4321-47bd-817a-4f63e0b06845"),
                    Name = "Claudio Farias",
                    Document = "321.456.788-99",
                    PhoneNumber = "(22) 2222-2222",
                    CellPhoneNumber = "(45) 92121-1212",
                    DateBirth = new DateTime(1985, 2, 10),
                    Email = "claudio.farias@gmail.com",
                    Gender = "Masculino",
                    Created = DateTime.Now,
                    Addresses = new List<CustomerAddress>
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
                        }
                    }
                },
                new Customer
                {
                    Id = Guid.Parse("282bde21-8fcc-4230-9a2b-3ec0178d5eb1"),
                    Name = "Maria Aparecida",
                    Document = "111.222.333-44",
                    PhoneNumber = "(12) 2121-1221",
                    CellPhoneNumber = "(15) 98888-4645",
                    DateBirth = new DateTime(1967, 5, 7),
                    Email = "maparecida@hotmail.com",
                    Gender = "Feminino",
                    Created = DateTime.Now,
                    Addresses = new List<CustomerAddress>
                    {
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
                        }
                    }
                },
                new Customer
                {
                    Id = Guid.Parse("47a5d987-d6b0-4ad9-9afc-c50eebfdcc6c"),
                    Name = "Carlos Agripino",
                    Document = "331.045.667-81",
                    PhoneNumber = "(31) 3114-5566",
                    CellPhoneNumber = "(32) 98877-1010",
                    DateBirth = new DateTime(1956, 11, 4),
                    Email = "carlosagrinino_agricultor@gmail.com",
                    Gender = "Masculino",
                    Created = DateTime.Now,
                    Addresses = new List<CustomerAddress>
                    {
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
                    }
                },
                new Customer
                {
                    Id = Guid.Parse("9407d7dc-4cde-4880-9665-05cef0396fef"),
                    Name = "Jessica Correa",
                    Document = "456.645.342-00",
                    PhoneNumber = "(51) 3344-4455",
                    CellPhoneNumber = "(51) 98080-0909",
                    DateBirth = new DateTime(1998, 5, 13),
                    Email = "jehcorreia@gmail.com",
                    Gender = "Feminino",
                    Created = DateTime.Now,
                    Addresses = new List<CustomerAddress>()
                }
            };
        }
        #endregion
    }
}
