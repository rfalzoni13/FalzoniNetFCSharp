using FalzoniNetFCSharp.Application.ServiceApplication.Identity;
using FalzoniNetFCSharp.Domain.DTO.Identity;
using FalzoniNetFCSharp.Presentation.Api.Attributes;
using FalzoniNetFCSharp.Presentation.Api.Models.Identity;
using FalzoniNetFCSharp.Presentation.Api.Utils;
using NLog;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System;
using System.Web.Http;

namespace FalzoniNetFCSharp.Presentation.Api.Controllers.Admin.Identity
{
    [RoutePrefix("Api/IdentityUtility")]
    public class IdentityUtilityController : ApiController
    {
        #region Attributes
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IdentityUtilityServiceApplication _identityUtilityServiceApplication;
        #endregion

        #region Constructor
        public IdentityUtilityController(IdentityUtilityServiceApplication identityUtilityServiceApplication)
        {
            _identityUtilityServiceApplication = identityUtilityServiceApplication;
        }
        #endregion

        #region Two Factors
        /// <summary>
        /// Obter Autenticação Dois Fatores
        /// </summary>
        /// <response code="500">Internal Server Error</response>
        /// <param name="email"></param>
        /// <param name="returnUrl"></param>
        /// <remarks>Obtém as opções de autencicação de dois fatores</remarks>
        /// <returns></returns>
        // GET: /IdentityUtitlity/GetTwoFactorProviders
        [HttpGet]
        [Route("GetTwoFactorProviders")]
        public async Task<HttpResponseMessage> GetTwoFactorProviders(string email, string returnUrl = null)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;

            try
            {
                var userFactors = await _identityUtilityServiceApplication.GetTwoFactorProvidersAsync(email);

                return Request.CreateResponse(HttpStatusCode.OK, userFactors);
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Erro fatal!");
                return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
            }
        }

        /// <summary>
        /// Enviar código de dois fatores
        /// </summary>
        /// <response code="500">Internal Server Error</response>
        /// <param name="sendCodeModel"></param>
        /// <remarks>Efetua o envio do código de dois fatores</remarks>
        /// <returns></returns>
        // POST: /IdentityUtitlity/SendTwoFactorProviderCode
        [CustomAuthorize]
        [HttpPost]
        [Route("SendTwoFactorProviderCode")]
        public async Task<HttpResponseMessage> SendTwoFactorProviderCode(SendCodeModel sendCodeModel)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            try
            {
                _logger.Info(action + " - Iniciado");
                var sendCodeDTO = new SendCodeDTO
                {
                    UserId = sendCodeModel.UserId,
                    SelectedProvider = sendCodeModel.SelectedProvider
                };

                await _identityUtilityServiceApplication.SendCodeAsync(sendCodeDTO);

                _logger.Info(action + " - Sucesso!");

                _logger.Info(action + " - Finalizado");
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, "Código enviado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Erro fatal!");
                return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
            }
        }

        /// <summary>
        /// Verificar código de dois fatores
        /// </summary>
        /// <response code="500">Internal Server Error</response>
        /// <param name="verifiyCodeModel"></param>
        /// <remarks>Efetua verificação de código de dois fatores</remarks>
        /// <returns></returns>
        // POST: /IdentityUtitlity/VerifyCodeTwoFactor
        [CustomAuthorize]
        [HttpPost]
        [Route("VerifyCodeTwoFactor")]
        public async Task<HttpResponseMessage> VerifyCodeTwoFactor(VerifyCodeModel verifiyCodeModel)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    throw new Exception("Usuário não autenticado!");
                }
                _logger.Info(action + " - Iniciado");
                var verifiyCodeDTO = new VerifyCodeDTO
                {
                    UserId = verifiyCodeModel.UserId,
                    Code = verifiyCodeModel.Code,
                    Provider = verifiyCodeModel.Provider,
                    ReturnUrl = verifiyCodeModel.ReturnUrl
                };

                var retornoCodigo = await _identityUtilityServiceApplication.VerifyCodeAsync(verifiyCodeDTO);

                _logger.Info(action + " - Sucesso!");

                _logger.Info(action + " - Finalizado");
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, retornoCodigo);
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Erro fatal!");
                return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
            }
        }
        #endregion

        #region Phone and E-mail confirmation
        /// <summary>
        /// Enviar Código de Confirmação de E-mail
        /// </summary>
        /// <response code="500">Internal Server Error</response>
        /// <param name="generateTokenEmailModel"></param>
        /// <remarks>Envio de código de confirmação de e-mail</remarks>
        /// <returns></returns>
        // POST: /IdentityUtitlity/SendEmailConfirmationCode
        [HttpPost]
        [Route("SendEmailConfirmationCode")]
        public async Task<HttpResponseMessage> SendEmailConfirmationCode(GenerateTokenEmailModel generateTokenEmailModel)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            try
            {
                _logger.Info(action + " - Iniciado");
                var generateTokenEmailDTO = new GenerateTokenEmailDTO
                {
                    UserId = generateTokenEmailModel.UserId,
                    Url = generateTokenEmailModel.Url
                };

                await _identityUtilityServiceApplication.SendEmailConfirmationCodeAsync(generateTokenEmailDTO);

                _logger.Info(action + " - Sucesso!");

                _logger.Info(action + " - Finalizado");
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, "Código enviado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Erro fatal!");
                return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
            }
        }

        /// <summary>
        /// Enviar Código de Confirmação de Telefone
        /// </summary>
        /// <response code="500">Internal Server Error</response>
        /// <param name="generateTokenPhoneModel"></param>
        /// <remarks>Envio de código de confirmação de telefone</remarks>
        /// <returns></returns>
        // POST: /IdentityUtitlity/SendPhoneConfirmationCode
        [HttpPost]
        [Route("SendPhoneConfirmationCode")]
        public async Task<HttpResponseMessage> SendPhoneConfirmationCode(GenerateTokenPhoneModel generateTokenPhoneModel)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            try
            {
                _logger.Info(action + " - Iniciado");
                var generateTokenPhoneDTO = new GenerateTokenPhoneDTO
                {
                    UserId = generateTokenPhoneModel.UserId,
                    Phone = generateTokenPhoneModel.Phone
                };

                await _identityUtilityServiceApplication.SendPhoneConfirmationCodeAsync(generateTokenPhoneDTO);

                _logger.Info(action + " - Sucesso!");

                _logger.Info(action + " - Finalizado");
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, "Código enviado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Erro fatal!");
                return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
            }
        }

        /// <summary>
        /// Verificar Código de Confirmação de E-mail
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        /// <param name="confirmEmailCodeModel"></param>
        /// <remarks>Verificação de código de confirmação do e-mail do usuário</remarks>
        /// <returns></returns>
        // POST: /IdentityUtitlity/VerifyEmailConfirmationCode
        [HttpPost]
        [Route("VerifyEmailConfirmationCode")]
        public async Task<HttpResponseMessage> VerifyEmailConfirmationCode(ConfirmEmailCodeModel confirmEmailCodeModel)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            try
            {
                _logger.Info(action + " - Iniciado");
                var confirmEmailCodeDTO = new ConfirmEmailCodeDTO
                {
                    UserId = confirmEmailCodeModel.UserId,
                    Code = confirmEmailCodeModel.Code
                };

                var result = await _identityUtilityServiceApplication.VerifyEmailConfirmationCodeAsync(confirmEmailCodeDTO);
                if (!result.Succeeded)
                {
                    return ResponseManager.ReturnErrorResult(Request, _logger, action, result.Errors);
                }

                _logger.Info(action + " - Sucesso!");

                _logger.Info(action + " - Finalizado");
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, "Email confirmado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Erro fatal!");
                return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
            }
        }

        /// <summary>
        /// Verificar Código de Confirmação de Telefone
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        /// <param name="confirmPhoneCodeModel"></param>
        /// <remarks>Verificação de código de confirmação do telefone do usuário</remarks>
        /// <returns></returns>
        // POST: /IdentityUtitlity/VerifyPhoneConfirmationCode
        [HttpPost]
        [Route("VerifyPhoneConfirmationCode")]
        public async Task<HttpResponseMessage> VerifyPhoneConfirmationCode(ConfirmPhoneCodeModel confirmPhoneCodeModel)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            try
            {
                _logger.Info(action + " - Iniciado");
                var confirmPhoneCodeDTO = new ConfirmPhoneCodeDTO
                {
                    UserId = confirmPhoneCodeModel.UserId,
                    Phone = confirmPhoneCodeModel.Phone,
                    Code = confirmPhoneCodeModel.Code
                };

                var result = await _identityUtilityServiceApplication.VerifyPhoneConfirmationCodeAsync(confirmPhoneCodeDTO);
                if (!result.Succeeded)
                {
                    return ResponseManager.ReturnErrorResult(Request, _logger, action, result.Errors);
                }

                _logger.Info(action + " - Sucesso!");

                _logger.Info(action + " - Finalizado");
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, "Telefone confirmado com sucesso!");
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
