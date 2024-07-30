using FalzoniNetFCSharp.Presentation.Administrator.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace FalzoniNetFCSharp.Presentation.Administrator.Models.Register
{
    public class CustomerModel : BaseModel
    {
        public CustomerModel() 
        {
            Addresses = new List<CustomerAddressModel>();
        }

        [Required(ErrorMessage = "O nome do cliente é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        [DisplayName("Data de nascimento")]
        public DateTime DateBirth { get; set; }

        [Required(ErrorMessage = "Favor informar o gênero do cliente")]
        [DisplayName("Gênero")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "O e-mail do cliente é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de E-mail inválido!")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "O celular do cliente é obrigatório")]
        public string CellPhoneNumber { get; set; }

        [Required(ErrorMessage = "O documento do cliente é obrigatório")]
        public string Document { get; set; }



        public virtual List<CustomerAddressModel> Addresses { get; set; }

        public virtual List<SelectListItem> Genders
        {
            get
            {
                return new List<SelectListItem>
                (
                    new SelectListItem[]
                    {
                        new SelectListItem
                        {
                            Text = "Masculino",
                            Value = "Masculino"
                        },
                        new SelectListItem
                        {
                            Text = "Feminino",
                            Value = "Feminino"
                        }
                    }
                );
            }
        }
    }
}