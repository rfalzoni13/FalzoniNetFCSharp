using FalzoniNetFCSharp.Domain.DTO.Base;
using System;

namespace FalzoniNetFCSharp.Domain.DTO.Identity
{
    public class ApplicationUserDTO : BaseDTO
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public DateTime DateBirth { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string PhotoPath { get; set; }

        public string PhoneNumber { get; set; }

        public string[] Roles { get; set; }

    }
}
