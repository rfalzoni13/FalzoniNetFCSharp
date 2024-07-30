using FalzoniNetFCSharp.Infra.Data.Context;
using FalzoniNetFCSharp.Infra.Data.Identity;
using FalzoniNetFCSharp.Service.Senders.Email;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace FalzoniNetFCSharp.Application.IdentityConfiguration
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var appDbContext = context.Get<FalzoniContext>();
            var appUserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(appDbContext));

            //appUserManager.UserValidator = new UserValidator<ApplicationUser>(appUserManager)
            //{
            //    AllowOnlyAlphanumericUserNames = false,
            //    RequireUniqueEmail = true
            //};

            //appUserManager.PasswordValidator = new PasswordValidator
            //{
            //    RequiredLength = 6,
            //    RequireNonLetterOrDigit = true,
            //    RequireDigit = true,
            //    RequireLowercase = true,
            //    RequireUppercase = true,
            //};

            appUserManager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Seu código de segurança é: {0}"
            });

            appUserManager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Código de segurança",
                BodyFormat = "Seu código de segurança é: {0}"
            });

            appUserManager.EmailService = new EmailIdentityMessageService();
            //appUserManager.SmsService = new SmsIdentityMessageService();

            var provider = new DpapiDataProtectionProvider("Projeto Falzoni");

            appUserManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>
            (provider.Create("Projeto Falzoni"));

            return appUserManager;
        }
    }
}
