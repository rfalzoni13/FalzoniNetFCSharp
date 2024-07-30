using FalzoniNetFCSharp.Domain.DTO.Register;
using FalzoniNetFCSharp.Service.Register;
using System.Collections.Generic;
using System;

namespace FalzoniNetFCSharp.Application.ServiceApplication.Register
{
    public class CustomerServiceApplication
    {
        private readonly CustomerService _customerService;

        public CustomerServiceApplication(CustomerService customerService)
        {
            _customerService = customerService;
        }

        public CustomerDTO Get(Guid Id)
        {
            if(Id == Guid.Empty)
                throw new ApplicationException("Erro ao buscar usuário!");

            return _customerService.Get(Id);
        }

        public List<CustomerDTO> GetAll() => _customerService.GetAll();
        

        public void Add(CustomerDTO customerDTO)
        {
            Validate(customerDTO);

            _customerService.Add(customerDTO);
        }

        public void Update(CustomerDTO customerDTO)
        {
            Validate(customerDTO);

            _customerService.Update(customerDTO);
        }

        public void Delete(CustomerDTO customerDTO)
        {
            if (customerDTO.Id == Guid.Empty)
                throw new ApplicationException("Erro ao buscar usuário!");

            _customerService.Delete(customerDTO);
        }

        // Private METHODS
        private void Validate(CustomerDTO customerDTO)
        {
            if (string.IsNullOrEmpty(customerDTO.Name)) throw new ApplicationException("Necessário informar o nome do Cliente");

            if ((DateTime.Now.Year - customerDTO.DateBirth.Year) < 17) throw new ApplicationException("O Cliente deve ser maior de 18 anos");

            if (string.IsNullOrEmpty(customerDTO.Document)) throw new ApplicationException("Necessário informar um documento válido");

            if (string.IsNullOrEmpty(customerDTO.Gender)) throw new ApplicationException("Necessário informar o gênero");

            if (string.IsNullOrEmpty(customerDTO.CellPhoneNumber)) throw new ApplicationException("Necessário informar o celular");

            if (customerDTO.Addresses == null || customerDTO.Addresses.Count == 0) throw new ApplicationException("Necessário informar ao menos um endereço");
        }
    }
}
