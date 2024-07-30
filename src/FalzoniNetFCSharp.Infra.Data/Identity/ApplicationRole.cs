using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;

namespace FalzoniNetFCSharp.Infra.Data.Identity
{
    public class ApplicationRole : IdentityRole
    {
        [Required]
        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }
    }
}
