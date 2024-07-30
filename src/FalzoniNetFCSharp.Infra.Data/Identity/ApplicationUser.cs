using System.Security.Claims;
using System.Threading.Tasks;
using System;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using System.Linq;

namespace FalzoniNetFCSharp.Infra.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(256)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Gender { get; set; }

        [MaxLength(1024)]
        public string PhotoPath { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public DateTime DateBirth { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // authenticationType deve corresponder a um definido em CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

            // Adicione declarações de usuários aqui
            userIdentity.AddClaim(new Claim(ClaimTypes.Name, userIdentity.GetUserName()));
            userIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userIdentity.GetUserId()));

            var roles = manager.GetRoles(userIdentity.GetUserId()).ToList();
            if (roles != null && roles.Count > 0)
            {
                foreach (var role in roles)
                {
                    userIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
                }
            }
            return userIdentity;
        }
    }
}
