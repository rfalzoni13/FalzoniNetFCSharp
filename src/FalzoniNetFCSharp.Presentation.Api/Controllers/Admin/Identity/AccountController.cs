using FalzoniNetFCSharp.Application.IdentityConfiguration;
using FalzoniNetFCSharp.Application.ServiceApplication.Identity;
using FalzoniNetFCSharp.Presentation.Api.Models.Identity;
using FalzoniNetFCSharp.Presentation.Api.Utils;
using FalzoniNetFCSharp.Utils.Helpers;
using Microsoft.Owin.Security;
using NLog;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System;
using System.Web.Http;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

namespace FalzoniNetFCSharp.Presentation.Api.Controllers.Admin.Identity
{
    [RoutePrefix("Api/Account")]
    public class AccountController : ApiController
    {
        #region Attributes
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly AccountServiceApplication _accountServiceApplication;
        #endregion

        #region Constructor
        public AccountController(AccountServiceApplication accountServiceApplication)
        {
            _accountServiceApplication = accountServiceApplication;
        }
        #endregion

        #region Logout
        /// <summary>
        /// Logout
        /// </summary>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Deslogar do Sistema</remarks>
        /// <returns></returns>
        // POST: /Account/Logout
        [Route("Logout")]
        public HttpResponseMessage Logout()
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;

            try
            {
                _logger.Info(action + " - Iniciado");

                ApplicationOAuthProvider.Logout(Request.GetOwinContext(), CookieAuthenticationDefaults.AuthenticationType);

                _logger.Info(action + " - Sucesso!");

                _logger.Info(action + " - Finalizado");

                return Request.CreateResponse(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Erro fatal!");
                return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
            }
        }

        #endregion

        #region External Logins
        /// <summary>
        /// Login Externo
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        /// <param name="provider"></param>
        /// <param name="error"></param>
        /// <remarks>Efetuar login com provedores externos</remarks>
        /// <returns></returns>
        // GET: /Account/ExternalLogin
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [HttpGet]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<HttpResponseMessage> ExternalLogin(string provider, string error = null)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;

            try
            {
                _logger.Info(action + " - Iniciado");

                if (error != null)
                {
                    throw new Exception(error);
                }

                if (!User.Identity.IsAuthenticated)
                {
                    return new ChallengeResult(provider, this);
                }

                await ApplicationOAuthProvider.ExternalLogin(Request.GetOwinContext(), User, provider);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.Fatal(ex, "Erro Fatal");
                return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
            }
            catch (ApplicationException ex)
            {
                _logger.Fatal(ex, "Erro Fatal");
                return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Erro fatal!");
                return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
            }
        }

        /// <summary>
        /// Obter Logins Externos
        /// </summary>
        /// <response code="500">Internal Server Error</response>
        /// <param name="returnUrl"></param>
        /// <param name="generateState"></param>
        /// <remarks>Obtém todos logins externos vinculados</remarks>
        /// <returns></returns>
        // GET: /Account/GetExtermalLogins
        [HttpGet]
        [Route("GetExtermalLogins")]
        public HttpResponseMessage GetExtermalLogins(string returnUrl, bool generateState = false)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;

            try
            {
                _logger.Info(action + " - Iniciado");

                IEnumerable<AuthenticationDescription> descriptions = ApplicationOAuthProvider.GetExternalAuthenticationTypes(Request.GetOwinContext());
                List<ExternalLoginModel> logins = new List<ExternalLoginModel>();

                string state;

                if (generateState)
                {
                    const int strengthInBits = 256;
                    state = RandomOAuthStateGeneratorHelper.Generate(strengthInBits);
                }
                else
                {
                    state = null;
                }

                foreach (AuthenticationDescription description in descriptions)
                {
                    ExternalLoginModel login = new ExternalLoginModel
                    {
                        Name = description.Caption,
                        Url = Url.Route("ExternalLogin", new
                        {
                            provider = description.AuthenticationType,
                            response_type = "token",
                            client_id = AppBuilderConfiguration.PublicClientId,
                            redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                            state = state
                        }),
                        State = state
                    };
                    logins.Add(login);
                }


                return Request.CreateResponse(HttpStatusCode.OK, logins);
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Erro fatal!");
                return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
            }
        }

        /// <summary>
        /// Adicionar Login Externo
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        /// <param name="model"></param>
        /// <remarks>Adiciona login externo</remarks>
        /// <returns></returns>
        // POST: /Account/AddExternalLogin
        [HttpPost]
        [Route("AddExternalLogin")]
        public async Task<HttpResponseMessage> AddExternalLogin(AddExternalLoginBindingModel model)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;

            try
            {
                _logger.Info(action + " - Iniciado");

                ApplicationOAuthProvider.Logout(Request.GetOwinContext(), DefaultAuthenticationTypes.ExternalCookie);

                var result = await _accountServiceApplication.AddExternalLoginAsync(User.Identity.GetUserId(), model.ExternalAccessToken);
                if (!result.Succeeded)
                {
                    return ResponseManager.ReturnErrorResult(Request, _logger, action, result.Errors);
                }

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Erro fatal!");
                return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
            }
        }

        /// <summary>
        /// Adicionar Usuário ao Login Externo
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        /// <param name="model"></param>
        /// <remarks>Adiciona usuário ao provedor de login externo</remarks>
        /// <returns></returns>
        // POST: /Account/AddExternalUserLogin
        [HttpPost]
        [Route("AddExternalUserLogin")]
        public async Task<HttpResponseMessage> AddExternalUserLogin(RegisterExternalBindingModel model)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;

            try
            {
                _logger.Info(action + " - Iniciado");

                var result = await ApplicationOAuthProvider.RegisterExternal(Request.GetOwinContext(), model.Email, model.Email);
                if (!result.Succeeded)
                {
                    return ResponseManager.ReturnErrorResult(Request, _logger, action, result.Errors);
                }

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Erro fatal!");
                return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
            }
        }

        /// <summary>
        /// Remover Login Externo
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        /// <param name="model"></param>
        /// <remarks>Remove provedor de login externo</remarks>
        /// <returns></returns>
        // POST: /Account/RemoveExternalLogin
        [HttpPost]
        [Route("RemoveExternalLogin")]
        public async Task<HttpResponseMessage> RemoveExternalLogin(RemoveLoginBindingModel model)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;

            try
            {
                _logger.Info(action + " - Iniciado");

                var result = await _accountServiceApplication.RemoveExternalLoginAsync(User.Identity.GetUserId(), model.LoginProvider, model.ProviderKey);
                if (!result.Succeeded)
                {
                    return ResponseManager.ReturnErrorResult(Request, _logger, action, result.Errors);
                }

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Erro fatal!");
                return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
            }
        }
        #endregion
    }
}
