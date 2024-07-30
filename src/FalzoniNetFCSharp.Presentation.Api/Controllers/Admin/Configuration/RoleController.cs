using FalzoniNetFCSharp.Application.ServiceApplication.Configuration;
using FalzoniNetFCSharp.Presentation.Api.Attributes;
using FalzoniNetFCSharp.Presentation.Api.Utils;
using NLog;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FalzoniNetFCSharp.Presentation.Api.Controllers.Admin.Configuration
{
    [CustomAuthorize(Roles = "Administrator")]
    [RoutePrefix("Api/Role")]
    public class RoleController : ApiController
    {
        #region Attributes
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly RoleServiceApplication _roleServiceApplication;
        #endregion

        #region Constructor
        public RoleController(RoleServiceApplication roleServiceApplication)
        {
            _roleServiceApplication = roleServiceApplication;
        }
        #endregion

        #region Getters
        /// <summary>
        /// Listar nomes de Acessos
        /// </summary>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Listagem de todos os acessos pelos nomes</remarks>
        /// <returns></returns>
        // GET Api/Role/GelAllNames
        [HttpGet]
        [Route("GelAllNames")]
        public HttpResponseMessage GelAllNames()
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            try
            {
                _logger.Info(action + " - Iniciado");
                var retorno = _roleServiceApplication.GelAllNames();

                if (retorno != null && retorno.Count() > 0)
                {
                    _logger.Info(action + " - Sucesso!");

                    _logger.Info(action + " - Finalizado");
                    return Request.CreateResponse(HttpStatusCode.OK, retorno);
                }
                else
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
            }

            catch (HttpResponseException ex)
            {
                if (ex.Response.StatusCode == HttpStatusCode.NotFound)
                {
                    return ResponseManager.ReturnExceptionNotFound(ex, Request, _logger, action, "Nenhum registro encontrado!");
                }

                return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
            }

            catch (Exception ex)
            {
                return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
            }
        }

        /// <summary>
        /// Listar todos os acessos
        /// </summary>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Listagem de todos os acessos</remarks>
        /// <returns></returns>
        // GET Api/Role/GetAll
        [HttpGet]
        [Route("GetAll")]
        public HttpResponseMessage GetAll()
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            try
            {
                _logger.Info(action + " - Iniciado");
                var retorno = _roleServiceApplication.GetAll();

                if (retorno != null && retorno.Count() > 0)
                {
                    _logger.Info(action + " - Sucesso!");

                    _logger.Info(action + " - Finalizado");
                    return Request.CreateResponse(HttpStatusCode.OK, retorno);
                }
                else
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
            }

            catch (HttpResponseException ex)
            {
                if (ex.Response.StatusCode == HttpStatusCode.NotFound)
                {
                    return ResponseManager.ReturnExceptionNotFound(ex, Request, _logger, action, "Nenhum registro encontrado!");
                }

                return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
            }

            catch (Exception ex)
            {
                return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
            }
        }

        /// <summary>
        /// Listar usuário pelo Id
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Retorna o usuário através do Id do mesmo</remarks>
        /// <param name="Id">Id do usuário</param>
        /// <returns></returns>
        // GET Api/Role/Get?id={Id}
        [HttpGet]
        [Route("Get")]
        public HttpResponseMessage Get(Guid Id)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            try
            {
                _logger.Info(action + " - Iniciado");
                if (!Guid.Equals(Id, Guid.Empty))
                {
                    var role = _roleServiceApplication.Get(Id);

                    if (role != null)
                    {
                        _logger.Info(action + " - Sucesso!");

                        _logger.Info(action + " - Finalizado");
                        return Request.CreateResponse(HttpStatusCode.OK, role);
                    }
                    else
                    {
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    }
                }
                else
                {
                    return ResponseManager.ReturnBadRequest(Request, _logger, action, "Parâmetro incorreto!");
                }
            }
            catch (HttpResponseException ex)
            {
                if (ex.Response.StatusCode == HttpStatusCode.NotFound)
                {
                    return ResponseManager.ReturnExceptionNotFound(ex, Request, _logger, action, "Nenhum registro encontrado!");
                }

                return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
            }

            catch (Exception ex)
            {
                return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
            }
        }
        #endregion
    }
}
