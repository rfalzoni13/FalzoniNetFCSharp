using FalzoniNetFCSharp.Presentation.Administrator.Models.Common;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Configuration;
using System;

namespace FalzoniNetFCSharp.Presentation.Administrator.Models.Identity
{
    public class ApplicationUserModel
    {
        public ApplicationUserModel(UserModel user)
        {
            ID = user.Id == Guid.Empty ? null : user.Id.ToString();
            Name = user.Name;
            Email = user.Email;
            UserName = user.UserName;
            Gender = user.Gender;
            PhoneNumber = user.PhoneNumber;
            DateBirth = user.DateBirth;
            Roles = user.Roles;
            AcceptTerms = false;
            File = user.File == null ? null : new FileModel
            {
                FileName = user.File.FileName,
                Base64String = user.File.Base64String
            };

            if (File != null)
            {
                File.RemoveHeaderBase64();
            }
        }

        public string ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateBirth { get; set; }
        public bool AcceptTerms { get; set; }
        public string[] Roles { get; set; }

        public virtual FileModel File { get; set; }
    }
}