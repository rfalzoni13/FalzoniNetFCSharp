using FalzoniNetFCSharp.Utils.Helpers;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using System.Configuration;
using System.Web.Http;

namespace FalzoniNetFCSharp.Presentation.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Serviços e configuração da API da Web
            ConfigurationHelper.ProviderName = ConfigurationManager.AppSettings["ProviderName"];

            // Configuração e serviços de API Web
            // Configure a API Web para usar somente a autenticação de token de portador.
            config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new CustomAuthorizeAttribute());
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "Api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Redirecionar automaticamente para o Swagger
            config.Routes.MapHttpRoute(
                name: "Swagger",
                routeTemplate: string.Empty,
                defaults: null,
                constraints: null,
                handler: new Swashbuckle.Application.RedirectHandler((message => message.RequestUri.ToString()), "swagger")
            );

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("multipart/form-data"));
        }
    }
}
