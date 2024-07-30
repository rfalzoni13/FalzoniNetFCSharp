using FalzoniNetFCSharp.Presentation.Administrator.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace FalzoniNetFCSharp.Presentation.Administrator.Models.Register
{
    public class CustomerAddressModel : BaseModel
    {
        public Guid CustomerId { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório!")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "O Logradouro é obrigatório!")]
        public string AddressName { get; set; }

        [Required(ErrorMessage = "O Número é obrigatório!")]
        public int Number { get; set; }

        public string Complement { get; set; }

        [Required(ErrorMessage = "O Bairro é obrigatório!")]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "A Cidade é obrigatória!")]
        public string City { get; set; }

        [Required(ErrorMessage = "O Estado é obrigatório!")]
        public string State { get; set; }


        public bool Removed { get; set; }

    }
}