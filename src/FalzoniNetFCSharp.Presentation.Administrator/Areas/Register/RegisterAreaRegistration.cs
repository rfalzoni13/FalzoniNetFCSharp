using System.Web.Mvc;

namespace FalzoniNetFCSharp.Presentation.Administrator.Areas.Register
{
    public class RegisterAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Register";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Register_default",
                "Register/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}