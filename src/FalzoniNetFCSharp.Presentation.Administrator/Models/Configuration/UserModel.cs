using FalzoniNetFCSharp.Presentation.Administrator.Models.Base;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Common;
using FalzoniNetFCSharp.Presentation.Administrator.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FalzoniNetFCSharp.Presentation.Administrator.Models.Configuration
{
    public class UserModel : BaseModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Name { get; set; }

        [DisplayName("E-mail")]
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        public string Email { get; set; }

        [DisplayName("Gênero")]
        [Required(ErrorMessage = "O gênero é obrigatório")]
        public string Gender { get; set; }

        [DisplayName("Data de nascimento")]
        public DateTime DateBirth { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public string PhotoPath { get; set; }

        public string[] Roles { get; set; }



        public virtual FileModel File { get; set; }

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

        //Methods
        public void LoadProfilePhoto()
        {
            PhotoPath = !string.IsNullOrEmpty(PhotoPath) ? $"{PathUtils.GetApiPath()}/{PhotoPath}" : PhotoPath;
        }

    }
}