using FalzoniNetFCSharp.Presentation.Administrator.Models.Common;
using FalzoniNetFCSharp.Utils.Helpers;
using System.Threading.Tasks;
using System.Web;
using System;
using System.Web.Mvc;
using System.Web.Routing;
using FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Configuration;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Configuration;
using System.Linq;

namespace FalzoniNetFCSharp.Presentation.Administrator.Attributes
{
    public class ProfileActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.GetOwinContext().Authentication.User.Identity.IsAuthenticated)
            {
                IUserClient userClient = DependencyResolver.Current.GetService(typeof(IUserClient)) as IUserClient;

                string userId = HttpContext.Current.Request.GetOwinContext().Authentication.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value;

                string token = RequestHelper.GetAccessToken();

                if (string.IsNullOrEmpty(token)) throw new Exception("Não autorizado!");

                try
                {
                    var user = filterContext.HttpContext.Session["UserData"] as UserModel;
                    if (user == null)
                    {
                        user = Task.Run(async () => await userClient.GetAsync(userId)).Result;
                    }

                    filterContext.HttpContext.Session["UserData"] = user;

                    filterContext.Controller.ViewBag.Usuario = StringHelper.SetDashboardName(user.Name);
                    filterContext.Controller.ViewBag.Perfil = user.Roles[0];
                    filterContext.Controller.ViewBag.Photo = user.PhotoPath;
                }
                catch (Exception ex)
                {
                    filterContext.HttpContext.Session.Clear();
                    filterContext.HttpContext.GetOwinContext().Authentication.SignOut("ApplicationCookie");
                    filterContext.Controller.TempData["Return"] = new ReturnModel
                    {
                        Type = "Error",
                        Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message
                    };
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        area = string.Empty,
                        controller = "Account",
                        action = "Login"
                    }));
                }
            }
        }
    }
}