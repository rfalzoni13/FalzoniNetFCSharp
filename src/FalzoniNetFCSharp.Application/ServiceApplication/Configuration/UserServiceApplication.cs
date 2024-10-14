using FalzoniNetFCSharp.Application.IdentityConfiguration;
using FalzoniNetFCSharp.Domain.DTO.Identity;
using FalzoniNetFCSharp.Infra.Data.Identity;
using FalzoniNetFCSharp.Utils.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FalzoniNetFCSharp.Application.ServiceApplication.Configuration
{
    public class UserServiceApplication
    {
        #region Attributes
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private IdentityOfWork _identityOfWork;

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

        #region Constructor
        public UserServiceApplication(IdentityOfWork identityWork)
        {
            _identityOfWork = identityWork;
        }
        #endregion

        #region Getters
        public ICollection<ApplicationUserDTO> GetAll()
        {
            var users = UserManager.Users;

            return users.ToList().ConvertAll(u => new ApplicationUserDTO
            {
                Id = GuidHelper.StringToGuid(u.Id),
                Name = u.FullName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                PhotoPath = u.PhotoPath,
                Gender = u.Gender,
                DateBirth = u.DateBirth,
                UserName = u.UserName,
                Created = u.Created,
                Modified = u.Modified,
                Roles = UserManager.GetRoles(u.Id).ToArray()
            });
        }

        public ApplicationUserDTO Get(Guid Id)
        {
            var user = UserManager.FindById(GuidHelper.GuidToString(Id));

            return new ApplicationUserDTO
            {
                Id = GuidHelper.StringToGuid(user.Id),
                Name = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                PhotoPath = user.PhotoPath,
                Gender = user.Gender,
                DateBirth = user.DateBirth,
                UserName = user.UserName,
                Created = user.Created,
                Modified = user.Modified,
                Roles = UserManager.GetRoles(user.Id).ToArray()
            };
        }
        #endregion

        #region Services
        public void Add(ApplicationUserRegisterDTO register)
        {
            using (DbContextTransaction transaction = _identityOfWork.BeginTransaction())
            {
                try
                {
                    var user = UserManager.FindByName(register.UserName);
                    // Get user and rollback if exists

                    if (user != null)
                    {
                        throw new ApplicationException("Já existe um usuário com estas informações!");
                    }

                    user = new ApplicationUser()
                    {
                        FullName = register.Name,
                        Email = register.Email,
                        PhoneNumber = register.PhoneNumber,
                        DateBirth = register.DateBirth,
                        Gender = register.Gender,
                        UserName = register.UserName,
                        Active = true,
                        Created = DateTime.Now
                    };

                    // Add profile photo
                    if (register.File != null)
                    {
                        string path = $"Attachments\\User\\{user.FullName}";

                        ImageHelper.SaveImageFile(register.File.Base64String, RequestHelper.GetApplicationPath() + path, register.File.FileName, register.File.Format);

                        user.PhotoPath = $"{path}\\{register.File.FileName}";
                    }

                    // Add user
                    var result = UserManager.Create(user);

                    if (!result.Succeeded)
                    {
                        throw new DbUpdateException("Erro ao incluir usuário!");
                    }

                    // Add role
                    int i = 0;

                    while (i < register.Roles.Count())
                    {
                        var role = RoleManager.FindById(register.Roles[i]);
                        if (role == null)
                        {
                            throw new ArgumentNullException("Nenhum registro de permissão de acesso encontrado!");
                        }

                        result = UserManager.AddToRole(user.Id, role.Name);

                        if (!result.Succeeded)
                        {
                            throw new DbUpdateException("Erro ao incluir perfil de acesso!");
                        }

                        i++;
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void Delete(string Id)
        {
            using (var transaction = _identityOfWork.BeginTransaction())
            {
                try
                {
                    // Get user and rollback if not exists
                    var user = UserManager.FindById(Id);

                    if (user == null)
                    {
                        throw new ArgumentNullException("Nenhum usuário encontrado!");
                    }

                    FileHelper.DeleteFile(RequestHelper.GetApplicationPath() + user.PhotoPath);

                    // Delete user
                    var result = UserManager.Delete(user);

                    if (!result.Succeeded)
                    {
                        throw new DbUpdateException("Erro ao excluir usuário!");
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void Update(ApplicationUserRegisterDTO register)
        {
            using (var transaction = _identityOfWork.BeginTransaction())
            {
                try
                {
                    // Get user and rollback if not exists
                    var user = UserManager.FindById(register.ID);

                    if (user == null)
                    {
                        throw new ApplicationException("Nenhum usuário encontrado!");
                    }

                    user.FullName = register.Name;
                    user.Email = register.Email;
                    user.UserName = register.UserName;
                    user.DateBirth = register.DateBirth;
                    user.PhoneNumber = register.PhoneNumber;
                    user.Modified = DateTime.Now;

                    // Update profile photo
                    if (register.File != null)
                    {
                        string path = $"Attachments\\User\\{user.FullName}";

                        FileHelper.DeleteFile($"{RequestHelper.GetApplicationPath() + user.PhotoPath}");

                        ImageHelper.SaveImageFile(register.File.Base64String, RequestHelper.GetApplicationPath() + path, register.File.FileName, register.File.Format);

                        user.PhotoPath = $"{path}\\{register.File.FileName}";
                    }

                    // Update user
                    var result = UserManager.Update(user);

                    if (!result.Succeeded)
                    {
                        throw new DbUpdateException("Erro ao atualizar usuário!");
                    }

                    // Update roles
                    int i = 0;

                    do
                    {
                        var role = RoleManager.FindById(register.Roles[i]);
                        if (role == null)
                        {
                            throw new ArgumentNullException("Nenhum registro de permissão de acesso encontrado!");
                        }

                        var roles = UserManager.GetRoles(user.Id);
                        if (roles == null || roles.Count() <= 0)
                        {
                            throw new ApplicationException("Erro ao cadastrar novo perfil de acesso!");
                        }

                        result = UserManager.RemoveFromRoles(user.Id, roles.ToArray());

                        if (!result.Succeeded)
                        {
                            throw new DbUpdateException("Erro ao realizar manutenção de acesso!");
                        }

                        result = UserManager.AddToRole(user.Id, role.Name);

                        if (!result.Succeeded)
                        {
                            throw new DbUpdateException("Erro ao atualizar acesso!");
                        }

                        i++;
                    }
                    while (i < register.Roles.Count());

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        #endregion

        #region Async Services
        public async Task AddAsync(ApplicationUserRegisterDTO register)
        {
            using (DbContextTransaction transaction = _identityOfWork.BeginTransaction())
            {
                try
                {
                    // Get user and rollback if exists
                    var user = await UserManager.FindByNameAsync(register.UserName);

                    if (user != null)
                    {
                        throw new ApplicationException("Já existe um usuário com estas informações!");
                    }

                    user = new ApplicationUser()
                    {
                        FullName = register.Name,
                        Email = register.Email,
                        PhoneNumber = register.PhoneNumber,
                        DateBirth = register.DateBirth,
                        Gender = register.Gender,
                        UserName = register.UserName,
                        Active = true,
                        Created = DateTime.Now
                    };

                    // Add profile photo
                    if (register.File != null)
                    {
                        string path = $"Attachments\\User\\{user.FullName}";

                        ImageHelper.SaveImageFile(register.File.Base64String, RequestHelper.GetApplicationPath() + path, register.File.FileName, register.File.Format);

                        user.PhotoPath = $"{path}\\{register.File.FileName}";
                    }

                    // Add User
                    var result = await UserManager.CreateAsync(user);

                    if (!result.Succeeded)
                    {
                        throw new DbUpdateException("Erro ao incluir usuário!");
                    }

                    // Add Role
                    int i = 0;

                    while (i < register.Roles.Count())
                    {
                        var role = await RoleManager.FindByIdAsync(register.Roles[i]);
                        if (role == null)
                        {
                            throw new ArgumentNullException("Nenhum registro de permissão de acesso encontrado!");
                        }

                        result = await UserManager.AddToRoleAsync(user.Id, role.Name);

                        if (!result.Succeeded)
                        {
                            throw new DbUpdateException("Erro ao incluir perfil de acesso!");
                        }

                        i++;
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public async Task DeleteAsync(string Id)
        {
            using (DbContextTransaction transaction = _identityOfWork.BeginTransaction())
            {
                try
                {
                    // Get user and rollback if not exists
                    var user = await UserManager.FindByIdAsync(Id);

                    if (user == null)
                    {
                        throw new ArgumentNullException("Nenhum usuário encontrado!");
                    }

                    FileHelper.DeleteFile(RequestHelper.GetApplicationPath() + user.PhotoPath);

                    // Delete user
                    var result = await UserManager.DeleteAsync(user);

                    if (!result.Succeeded)
                    {
                        throw new DbUpdateException("Erro ao excluir usuário!");
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public async Task UpdateAsync(ApplicationUserRegisterDTO register)
        {
            using (DbContextTransaction transaction = _identityOfWork.BeginTransaction())
            {
                try
                {
                    // Get user and rollback if not exists
                    var user = await UserManager.FindByIdAsync(register.ID);

                    if (user == null)
                    {
                        throw new ApplicationException("Nenhum usuário encontrado!");
                    }

                    user.FullName = register.Name;
                    user.Email = register.Email;
                    user.UserName = register.UserName;
                    user.DateBirth = register.DateBirth;
                    user.PhoneNumber = register.PhoneNumber;
                    user.Modified = DateTime.Now;

                    // Update profile photo
                    if (register.File != null)
                    {
                        string path = $"Attachments\\User\\{user.FullName}";

                        FileHelper.DeleteFile($"{RequestHelper.GetApplicationPath() + user.PhotoPath}");

                        ImageHelper.SaveImageFile(register.File.Base64String, RequestHelper.GetApplicationPath() + path, register.File.FileName, register.File.Format);

                        user.PhotoPath = $"{path}\\{register.File.FileName}";
                    }

                    // Update user
                    var result = await UserManager.UpdateAsync(user);

                    if (!result.Succeeded)
                    {
                        throw new DbUpdateException("Erro ao atualizar usuário!");
                    }

                    // Update role
                    int i = 0;

                    do
                    {
                        var role = await RoleManager.FindByIdAsync(register.Roles[i]);
                        if (role == null)
                        {
                            throw new ArgumentNullException("Nenhum registro de permissão de acesso encontrado!");
                        }

                        var roles = await UserManager.GetRolesAsync(user.Id);
                        if (roles == null || roles.Count() <= 0)
                        {
                            throw new ApplicationException("Erro ao cadastrar novo perfil de acesso!");
                        }

                        result = await UserManager.RemoveFromRolesAsync(user.Id, roles.ToArray());

                        if (!result.Succeeded)
                        {
                            throw new DbUpdateException("Erro ao realizar manutenção de acesso!");
                        }

                        result = await UserManager.AddToRoleAsync(user.Id, role.Name);

                        if (!result.Succeeded)
                        {
                            throw new DbUpdateException("Erro ao atualizar acesso!");
                        }

                        i++;
                    }
                    while (i < register.Roles.Count());

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            RoleManager.Dispose();
            SignInManager.Dispose();
            UserManager.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
