using FalzoniNetFCSharp.Domain.DTO.Common;
using System;

namespace FalzoniNetFCSharp.Domain.DTO.Identity
{
    public class ApplicationUserRegisterDTO
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateBirth { get; set; }
        public bool AcceptTerms { get; set; }
        public string[] Roles { get; set; }

        public virtual FileDTO File { get; set; }
    }
}
