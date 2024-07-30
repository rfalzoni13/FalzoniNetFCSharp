using FalzoniNetFCSharp.Application.IdentityConfiguration;
using FalzoniNetFCSharp.Utils.Helpers;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Web;
using System;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using FalzoniNetFCSharp.Domain.DTO.Identity;
using Microsoft.AspNet.Identity;

namespace FalzoniNetFCSharp.Application.ServiceApplication.Configuration
{
    public class RoleServiceApplication
    {
        #region Attributes
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        protected ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            }
            set
            {
                _roleManager = value;
            }
        }

        protected ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        protected ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            set
            {
                _signInManager = value;
            }
        }
        #endregion

        #region Getters
        public List<string> GelAllNames()
        {
            var roles = RoleManager.Roles;

            return roles.Select(x => x.Name).Distinct().ToList();
        }

        public ICollection<ApplicationRoleDTO> GetAll()
        {
            var roles = RoleManager.Roles;

            return roles.ToList().ConvertAll(r => new ApplicationRoleDTO
            {
                Id = GuidHelper.StringToGuid(r.Id),
                Name = r.Name,
                Created = r.Created,
                Modified = r.Modified,
            });
        }

        public ApplicationRoleDTO Get(Guid Id)
        {
            var role = RoleManager.FindById(GuidHelper.GuidToString(Id));

            return new ApplicationRoleDTO
            {
                Id = GuidHelper.StringToGuid(role.Id),
                Name = role.Name,
                Created = role.Created,
                Modified = role.Modified,
            };
        }
        #endregion

    }
}
