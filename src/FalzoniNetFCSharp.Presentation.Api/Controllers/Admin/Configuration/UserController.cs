using FalzoniNetFCSharp.Application.ServiceApplication.Configuration;
using FalzoniNetFCSharp.Presentation.Api.Attributes;
using FalzoniNetFCSharp.Presentation.Api.Models.Identity;
using FalzoniNetFCSharp.Presentation.Api.Utils;
using NLog;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FalzoniNetFCSharp.Presentation.Api.Controllers.Admin.Configuration
{
    [CustomAuthorize(Roles = "Administrator")]
    [RoutePrefix("Api/User")]
    public class UserController : ApiController
    {
        #region Attributes
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly UserServiceApplication _userServiceApplication;
        #endregion

        #region Constructor
        public UserController(UserServiceApplication userServiceApplication)
        {
            _userServiceApplication = userServiceApplication;
        }
        #endregion

        #region Getters
        /// <summary>
        /// Listar todos os usuarios
        /// </summary>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Listagem de todos os usuarios</remarks>
        /// <returns></returns>
        // GET Api/User/GetAll
        [HttpGet]
        [Route("GetAll")]
        public HttpResponseMessage GetAll()
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            try
            {
                _logger.Info(action + " - Iniciado");
                var retorno = _userServiceApplication.GetAll();

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
        // GET Api/User/Get?id={Id}
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
                    var usuario = _userServiceApplication.Get(Id);

                    if (usuario != null)
                    {
                        _logger.Info(action + " - Sucesso!");

                        _logger.Info(action + " - Finalizado");
                        return Request.CreateResponse(HttpStatusCode.OK, usuario);
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

        #region Add User
        /// <summary>
        /// Inserir usuário
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Insere um novo usuário passando um objeto no body da requisição no método POST</remarks>
        /// <param name="applicationUserRegisterModel">Objeto de registro usuário</param>
        /// <returns></returns>
        // POST Api/User/Add
        [HttpPost]
        [Route("Add")]
        public HttpResponseMessage Add([FromBody] ApplicationUserRegisterModel applicationUserRegisterModel)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.Info(action + " - Iniciado");

                    var userDto = applicationUserRegisterModel.ConvertToDTO();

                    _userServiceApplication.Add(userDto);

                    _logger.Info(action + " - Sucesso!");

                    _logger.Info(action + " - Finalizado");

                    return Request.CreateResponse(HttpStatusCode.Created, "Usuário incluído com sucesso!");
                }
                else
                {
                    return ResponseManager.ReturnBadRequest(Request, _logger, action, "Por favor, preencha os campos corretamente!");
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
        /// Inserir usuário modo assíncrono
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Insere um novo usuário passando um objeto no body da requisição no método POST de forma assíncrona</remarks>
        /// <param name="applicationUserRegisterModel">Objeto de registro usuário</param>
        /// <returns></returns>
        // POST: Api/User/AddAsync
        [HttpPost]
        [Route("AddAsync")]
        public async Task<HttpResponseMessage> AddAsync([FromBody] ApplicationUserRegisterModel applicationUserRegisterModel)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.Info(action + " - Iniciado");

                    var userDto = applicationUserRegisterModel.ConvertToDTO();

                    await _userServiceApplication.AddAsync(userDto);

                    _logger.Info(action + " - Sucesso!");

                    _logger.Info(action + " - Finalizado");

                    return Request.CreateResponse(HttpStatusCode.Created, "Usuário incluído com sucesso!");
                }
                else
                {
                    return ResponseManager.ReturnBadRequest(Request, _logger, action, "Por favor, preencha os campos corretamente!");
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

        #region Update User
        /// <summary>
        /// Atualizar usuário
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Atualiza o usuário passando o objeto no body da requisição pelo método PUT</remarks>
        /// <param name="applicationUserRegisterModel">Objeto de registro do usuário</param>
        /// <returns></returns>
        // PUT Api/User/Update
        [HttpPut]
        [Route("Update")]
        public HttpResponseMessage Update([FromBody] ApplicationUserRegisterModel applicationUserRegisterModel)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            _logger.Info(action + " - Iniciado");
            try
            {
                if (ModelState.IsValid)
                {
                    var userDto = applicationUserRegisterModel.ConvertToDTO();

                    _userServiceApplication.Update(userDto);

                    _logger.Info(action + " - Sucesso!");

                    _logger.Info(action + " - Finalizado");

                    return Request.CreateResponse(HttpStatusCode.OK, "Usuário atualizado com sucesso!");
                }
                else
                {
                    return ResponseManager.ReturnBadRequest(Request, _logger, action, "Por favor, preencha os campos corretamente!");
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
        /// Atualizar usuário modo assíncrono
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Atualiza o usuário passando o objeto no body da requisição pelo método PUT de forma assíncrona</remarks>
        /// <param name="applicationUserRegisterModel">Objeto de registro do usuário</param>
        // PUT: Api/User/UpdateAsync
        [HttpPut]
        [Route("UpdateAsync")]
        public async Task<HttpResponseMessage> UpdateAsync([FromBody] ApplicationUserRegisterModel applicationUserRegisterModel)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            _logger.Info(action + " - Iniciado");
            try
            {
                if (ModelState.IsValid)
                {
                    var userDto = applicationUserRegisterModel.ConvertToDTO();

                    await _userServiceApplication.UpdateAsync(userDto);

                    _logger.Info(action + " - Sucesso!");

                    _logger.Info(action + " - Finalizado");

                    return Request.CreateResponse(HttpStatusCode.OK, "Usuário atualizado com sucesso!");
                }
                else
                {
                    return ResponseManager.ReturnBadRequest(Request, _logger, action, "Por favor, preencha os campos corretamente!");
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

        #region Delete User
        /// <summary>
        /// Excluir usuario
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Exclui o usuario passando o objeto no body da requisição pelo método DELETE</remarks>
        /// <param name="applicationUserRegisterModel">Objeto de registro do usuario</param>
        /// <returns></returns>
        // DELETE Api/User/Delete
        [HttpDelete]
        [Route("Delete")]
        public HttpResponseMessage Delete([FromBody] ApplicationUserRegisterModel applicationUserRegisterModel)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            _logger.Info(action + " - Iniciado");
            try
            {
                if (ModelState.IsValid)
                {
                    var userDto = applicationUserRegisterModel.ConvertToDTO();

                    _userServiceApplication.Delete(userDto);

                    _logger.Info(action + " - Sucesso!");

                    _logger.Info(action + " - Finalizado");


                    return Request.CreateResponse(HttpStatusCode.OK, "Usuário excluído com sucesso!");
                }
                else
                {
                    return ResponseManager.ReturnBadRequest(Request, _logger, action, "Por favor, preencha os campos corretamente!");
                }
            }

            catch (Exception ex)
            {
                return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
            }
        }

        /// <summary>
        /// Excluir usuario assíncrono
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Exclui o usuario passando o objeto no body da requisição pelo método DELETE de forma assíncrona</remarks>
        /// <param name="applicationUserRegisterModel">Objeto de registro do usuario</param>
        /// <returns></returns>
        //DELETE: Api/User/DeleteAsync
        [HttpDelete]
        [Route("DeleteAsync")]
        public async Task<HttpResponseMessage> DeleteAsync([FromBody] ApplicationUserRegisterModel applicationUserRegisterModel)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            try
            {
                _logger.Info(action + " - Iniciado");

                var userDto = applicationUserRegisterModel.ConvertToDTO();

                await _userServiceApplication.DeleteAsync(userDto);

                _logger.Info(action + " - Sucesso!");

                _logger.Info(action + " - Finalizado");
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, "Usuário deletado com sucesso!");
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
