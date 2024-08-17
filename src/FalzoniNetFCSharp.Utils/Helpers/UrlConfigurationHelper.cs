using System.Configuration;
using System.Diagnostics;

namespace FalzoniNetFCSharp.Utils.Helpers
{
    public class UrlConfigurationHelper
    {
        #region Base
        public static string PathUrl;
        #endregion

        #region Account
        public static string AccountLogin;
        public static string AccountLogout;
        public static string AccountExternalLogin;
        public static string AccountGetExternalLogins;
        public static string AccountAddExternalLogin;
        public static string AccountAddUserExternalLogin;
        public static string AccountRemoveExternalLogin;
        public static string AccountChangePassword;
        public static string AccountForgotPassword;
        public static string AccountResetPassword;
        #endregion

        #region IdentityUtility
        public static string IdentityUtilityGetTwoFactorProviders;
        public static string IdentityUtilitySendTwoFactorProviderCode;
        public static string IdentityUtilityVerifyCodeTwoFactor;
        public static string IdentityUtilitySendEmailConfirmationCode;
        public static string IdentityUtilitySendPhoneConfirmationCode;
        public static string IdentityUtilityVerifyEmailConfirmationCode;
        public static string IdentityUtilityVerifyPhoneConfirmationCode;
        #endregion

        #region Role
        public static string RoleGetAllNames;
        public static string RoleGetAll;
        public static string RoleGet;
        public static string RoleCreate;
        public static string RoleEdit;
        public static string RoleDelete;
        #endregion

        #region User
        public static string UserGetAll;
        public static string UserGet;
        public static string UserCreate;
        public static string UserEdit;
        public static string UserDelete;
        #endregion

        #region Customer
        public static string CustomerGetAll;
        public static string CustomerGet;
        public static string CustomerCreate;
        public static string CustomerEdit;
        public static string CustomerDelete;
        #endregion

        #region Product
        public static string ProductGetAll;
        public static string ProductGet;
        public static string ProductCreate;
        public static string ProductEdit;
        public static string ProductDelete;
        #endregion

        public static void SetUrlList()
        {
            #region Path
            PathUrl = !Debugger.IsAttached ? ConfigurationManager.AppSettings["UrlApiProd"] : ConfigurationManager.AppSettings["UrlApiDev"];
            #endregion

            #region Account
            AccountLogin = $"{PathUrl}/{ConfigurationManager.AppSettings["AccountUrl"]}/Login";
            AccountLogout = $"{PathUrl}/{ConfigurationManager.AppSettings["AccountUrl"]}/Logout";
            AccountExternalLogin = $"{PathUrl}/{ConfigurationManager.AppSettings["AccountUrl"]}/ExternalLogin";
            AccountGetExternalLogins = $"{PathUrl}/{ConfigurationManager.AppSettings["AccountUrl"]}/GetExternalLogins";
            AccountAddExternalLogin = $"{PathUrl}/{ConfigurationManager.AppSettings["AccountUrl"]}/AddExternalLogin";
            AccountAddUserExternalLogin = $"{PathUrl}/{ConfigurationManager.AppSettings["AccountUrl"]}/AddExternalUserLogin";
            AccountRemoveExternalLogin = $"{PathUrl}/{ConfigurationManager.AppSettings["AccountUrl"]}/RemoveExternalLogin";
            AccountChangePassword = $"{PathUrl}/{ConfigurationManager.AppSettings["AccountUrl"]}/ChangePassword";
            AccountForgotPassword = $"{PathUrl}/{ConfigurationManager.AppSettings["AccountUrl"]}/ForgotPassword";
            AccountResetPassword = $"{PathUrl}/{ConfigurationManager.AppSettings["AccountUrl"]}/RecoverPassword";
            #endregion

            #region IdentityUtility
            IdentityUtilityGetTwoFactorProviders = $"{PathUrl}/{ConfigurationManager.AppSettings["IdentityUtilityUrl"]}/GetTwoFactorProviders";
            IdentityUtilitySendTwoFactorProviderCode = $"{PathUrl}/{ConfigurationManager.AppSettings["IdentityUtilityUrl"]}/SendTwoFactorProviderCode";
            IdentityUtilityVerifyCodeTwoFactor = $"{PathUrl}/{ConfigurationManager.AppSettings["IdentityUtilityUrl"]}/VerifyCodeTwoFactor";
            IdentityUtilitySendEmailConfirmationCode = $"{PathUrl}/{ConfigurationManager.AppSettings["IdentityUtilityUrl"]}/SendEmailConfirmationCode";
            IdentityUtilitySendPhoneConfirmationCode = $"{PathUrl}/{ConfigurationManager.AppSettings["IdentityUtilityUrl"]}/SendPhoneConfirmationCode";
            IdentityUtilityVerifyEmailConfirmationCode = $"{PathUrl}/{ConfigurationManager.AppSettings["IdentityUtilityUrl"]}/VerifyEmailConfirmationCode";
            IdentityUtilityVerifyPhoneConfirmationCode = $"{PathUrl}/{ConfigurationManager.AppSettings["IdentityUtilityUrl"]}/VerifyPhoneConfirmationCode";
            #endregion

            #region Role
            RoleGetAllNames = $"{PathUrl}/{ConfigurationManager.AppSettings["RoleUrl"]}/GetAllNames";
            RoleGetAll = $"{PathUrl}/{ConfigurationManager.AppSettings["RoleUrl"]}/GetAll";
            RoleGet = $"{PathUrl}/{ConfigurationManager.AppSettings["RoleUrl"]}/Get";
            RoleCreate = $"{PathUrl}/{ConfigurationManager.AppSettings["RoleUrl"]}/Create";
            RoleEdit = $"{PathUrl}/{ConfigurationManager.AppSettings["RoleUrl"]}/Update";
            RoleDelete = $"{PathUrl}/{ConfigurationManager.AppSettings["RoleUrl"]}/Delete";
            #endregion

            #region User
            UserGetAll = $"{PathUrl}/{ConfigurationManager.AppSettings["UserUrl"]}/GetAll";
            UserGet = $"{PathUrl}/{ConfigurationManager.AppSettings["UserUrl"]}/Get";
            UserCreate = $"{PathUrl}/{ConfigurationManager.AppSettings["UserUrl"]}/Create";
            UserEdit = $"{PathUrl}/{ConfigurationManager.AppSettings["UserUrl"]}/Update";
            UserDelete = $"{PathUrl}/{ConfigurationManager.AppSettings["UserUrl"]}/Delete";
            #endregion

            #region Customer
            CustomerGetAll = $"{PathUrl}/{ConfigurationManager.AppSettings["CustomerUrl"]}/GetAll";
            CustomerGet = $"{PathUrl}/{ConfigurationManager.AppSettings["CustomerUrl"]}/Get";
            CustomerCreate = $"{PathUrl}/{ConfigurationManager.AppSettings["CustomerUrl"]}/Create";
            CustomerEdit = $"{PathUrl}/{ConfigurationManager.AppSettings["CustomerUrl"]}/Update";
            CustomerDelete = $"{PathUrl}/{ConfigurationManager.AppSettings["CustomerUrl"]}/Delete";
            #endregion

            #region Product
            ProductGetAll = $"{PathUrl}/{ConfigurationManager.AppSettings["ProductUrl"]}/GetAll";
            ProductGet = $"{PathUrl}/{ConfigurationManager.AppSettings["ProductUrl"]}/Get";
            ProductCreate = $"{PathUrl}/{ConfigurationManager.AppSettings["ProductUrl"]}/Create";
            ProductEdit = $"{PathUrl}/{ConfigurationManager.AppSettings["ProductUrl"]}/Update";
            ProductDelete = $"{PathUrl}/{ConfigurationManager.AppSettings["ProductUrl"]}/Delete";
            #endregion

        }

    }
}
