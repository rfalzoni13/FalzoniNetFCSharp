using FalzoniNetFCSharp.Application.IdentityConfiguration;
using FalzoniNetFCSharp.Infra.IoC;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System;
using System.Threading.Tasks;
using System.Web.Http;

[assembly: OwinStartup(typeof(FalzoniNetFCSharp.Presentation.Api.Startup))]

namespace FalzoniNetFCSharp.Presentation.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //Register configurations
            HttpConfiguration config = new HttpConfiguration();

            //GlobalConfiguration.Configure(WebApiConfig.Register);
            WebApiConfig.Register(config);

            //Injeção de dependência
            UnityConfig.Register(config);

            //Configure Owin            
            AppBuilderConfiguration.ConfigureAuth(app);
            app.UseCors(CorsOptions.AllowAll);
            AppBuilderConfiguration.ActivateAccessToken(app);

            app.UseWebApi(config);
            //AppBuilderConfiguration.ConfigureCors(app);
        }
    }
}
