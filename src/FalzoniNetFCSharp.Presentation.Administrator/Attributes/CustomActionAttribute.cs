using FalzoniNetFCSharp.Presentation.Administrator.Clients.Identity;
using FalzoniNetFCSharp.Utils.Helpers;
using Microsoft.Owin;
using System;
using System.Web;
using System.Web.Mvc;

namespace FalzoniNetFCSharp.Presentation.Administrator.Attributes
{
    public class CustomActionAttribute : ActionFilterAttribute
    {
        private readonly AccountClient _accountClient;

        public CustomActionAttribute(AccountClient accountClient)
        {
            _accountClient = accountClient;
        }

        public override async void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!GetOwinContext().Authentication.User.Identity.IsAuthenticated) return;

            DateTime dateLimit = Convert.ToDateTime(RequestHelper.GetTokenExpire());

            if(dateLimit >= DateTime.Now)
            {
                await _accountClient.RefreshToken();
            }

            base.OnActionExecuting(filterContext);
        }

        private IOwinContext GetOwinContext()
        {
            return HttpContext.Current.GetOwinContext();
        }
    }
}