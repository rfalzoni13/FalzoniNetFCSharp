using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Configuration;
using FalzoniNetFCSharp.Utils.Helpers;

namespace FalzoniNetFCSharp.Presentation.Administrator.Attributes
{
    public class UserConfigurationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            IRoleClient roleClient = DependencyResolver.Current.GetService(typeof(IRoleClient)) as IRoleClient;

            if (HttpContext.Current.GetOwinContext().Authentication.User.Identity.IsAuthenticated)
            {
                if (filterContext.Controller.ViewBag.Perfis == null)
                    filterContext.Controller.ViewBag.Perfis = Task.Run(async () => await roleClient.GetAllAsync()).Result;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}