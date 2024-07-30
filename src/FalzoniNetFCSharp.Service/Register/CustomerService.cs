using FalzoniNetFCSharp.Domain.Interfaces.Register;
using FalzoniNetFCSharp.Domain.DTO.Register;
using FalzoniNetFCSharp.Domain.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FalzoniNetFCSharp.Service.Register
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerAddressRepository _customerAddressRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(ICustomerRepository customerRepository, 
            ICustomerAddressRepository customerAddressRepository,
            IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _customerAddressRepository = customerAddressRepository;
            _unitOfWork = unitOfWork;
        }

        public CustomerDTO Get(Guid Id)
        {
            var customer = _customerRepository.GetWithInclude(Id);

            return new CustomerDTO(customer);
        }

        public List<CustomerDTO> GetAll()
        {
            var customers = _customerRepository.GetAll();

            return customers.ToList().ConvertAll(c => new CustomerDTO(c));
        }

        public void Add(CustomerDTO customerDTO)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    customerDTO.ConfigureNewEntity();

                    var customer = customerDTO.ConvertToEntity();

                    _customerRepository.Add(customer);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void Update(CustomerDTO customerDTO)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var customer = _customerRepository.Get(customerDTO.Id);

                    //Update principal data
                    customer.Name = customerDTO.Name;
                    customer.Document = customerDTO.Document;
                    customer.DateBirth = customerDTO.DateBirth;
                    customer.CellPhoneNumber = customerDTO.CellPhoneNumber;
                    customer.PhoneNumber = customerDTO.PhoneNumber;
                    customer.Email = customerDTO.Email;
                    customer.Gender = customerDTO.Gender;

                    // Remove exclude addresses
                    RemoveAddresses(customerDTO);

                    // Update addresses
                    customerDTO.Addresses.ForEach(e => {
                        e.Id = e.Id == Guid.Empty ? Guid.NewGuid() : e.Id;
                        e.CustomerId = customer.Id;
                        e.Created = e.Created == DateTime.MinValue ? DateTime.Now : e.Created;
                        e.Modified = DateTime.Now;
                    });

                    customer.Addresses = customerDTO.Addresses.ToList().ConvertAll(e => e.ConvertToEntity());

                    // Update modified entity data
                    customer.Modified = DateTime.Now;

                    _customerRepository.Update(customer);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void Delete(CustomerDTO customerDTO)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    _customerRepository.Delete(customerDTO.Id);

                    transaction.Commit();
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }


        #region private Methods
        private void RemoveAddresses(CustomerDTO customerDTO)
        {
            var ids = customerDTO.Addresses.Where(x => x.Removed).Select(x => x.Id).ToList();

            if (ids.Any())
            {
                _customerAddressRepository.RemoveRange(ids);
            }
        }

        #endregion

    }
}
