using System.Web.Http;

namespace FalzoniNetFCSharp.Presentation.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Moved to Startup Class
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
