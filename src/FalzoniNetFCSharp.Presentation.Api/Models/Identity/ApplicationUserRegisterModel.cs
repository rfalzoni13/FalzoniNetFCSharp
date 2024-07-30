using FalzoniNetFCSharp.Domain.DTO.Identity;
using FalzoniNetFCSharp.Presentation.Api.Models.Common;
using System;

namespace FalzoniNetFCSharp.Presentation.Api.Models.Identity
{
    public class ApplicationUserRegisterModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime DateBirth { get; set; }
        public bool AcceptTerms { get; set; }
        public string[] Roles { get; set; }

        public virtual FileModel File { get; set; }

        public ApplicationUserRegisterDTO ConvertToDTO()
        {
            return new ApplicationUserRegisterDTO
            {
                ID = this.ID,
                Name = this.Name,
                Email = this.Email,
                UserName = this.UserName,
                PhoneNumber = this.PhoneNumber,
                Gender = this.Gender,
                DateBirth = this.DateBirth.Date,
                Roles = this.Roles,
                AcceptTerms = this.AcceptTerms,
                File = this.File == null ? null : new Domain.DTO.Common.FileDTO
                {
                    FileName = this.File.FileName,
                    Base64String = this.File.Base64String,
                    Format = this.File.Format,
                }
            };
        }
    }
}