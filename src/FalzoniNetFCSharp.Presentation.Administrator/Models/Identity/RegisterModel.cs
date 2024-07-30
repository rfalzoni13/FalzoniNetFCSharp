using System;

namespace FalzoniNetFCSharp.Presentation.Administrator.Models.Identity
{
    public class RegisterModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public DateTime DateBirth { get; set; }
        public Guid RoleId { get; set; }
        public string PhotoPath { get; set; }
        public bool AcceptTerms { get; set; }

    }
}