using FalzoniNetFCSharp.Application.ServiceApplication.Register;
using FalzoniNetFCSharp.Presentation.Api.Attributes;
using FalzoniNetFCSharp.Presentation.Api.Models.Register;
using FalzoniNetFCSharp.Presentation.Api.Utils;
using NLog;
using System.Net.Http;
using System.Net;
using System;
using System.Web.Http;
using System.Linq;

namespace FalzoniNetFCSharp.Presentation.Api.Controllers.Admin.Register
{
    [CustomAuthorize(Roles = "Administrator")]
    [RoutePrefix("Api/Customer")]
    public class CustomerController : ApiController
    {
        #region Attributes
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly CustomerServiceApplication _customerServiceApplication;
        #endregion

        #region Constructor
        public CustomerController(CustomerServiceApplication customerServiceApplication)
        {
            _customerServiceApplication = customerServiceApplication;
        }
        #endregion

        #region Getters
        /// <summary>
        /// Listar todos os clientes
        /// </summary>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Listagem de todos os clientes</remarks>
        /// <returns></returns>
        // GET Api/Customer/GetAll
        [HttpGet]
        [Route("GetAll")]
        public HttpResponseMessage GetAll()
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            try
            {
                _logger.Info(action + " - Iniciado");
                var retorno = _customerServiceApplication.GetAll();

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
        /// Listar cliente pelo Id
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Retorna o cliente através do Id do mesmo</remarks>
        /// <param name="Id">Id do cliente</param>
        /// <returns></returns>
        // GET Api/Customer/Get?id={Id}
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
                    var customer = _customerServiceApplication.Get(Id);

                    if (customer != null)
                    {
                        _logger.Info(action + " - Sucesso!");

                        _logger.Info(action + " - Finalizado");
                        return Request.CreateResponse(HttpStatusCode.OK, customer);
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

        #region Add Customer
        /// <summary>
        /// Inserir cliente
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Insere um novo cliente passando um objeto no body da requisição no método POST</remarks>
        /// <param name="model">Objeto de registro cliente</param>
        /// <returns></returns>
        // POST Api/Customer/Add
        [HttpPost]
        [Route("Add")]
        public HttpResponseMessage Add([FromBody] CustomerModel model)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.Info(action + " - Iniciado");

                    var customerDTO = model.ConvertToDTO();

                    _customerServiceApplication.Add(customerDTO);

                    _logger.Info(action + " - Sucesso!");

                    _logger.Info(action + " - Finalizado");

                    return Request.CreateResponse(HttpStatusCode.Created, "Cliente incluído com sucesso!");
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

        ///// <summary>
        ///// Inserir cliente modo assíncrono
        ///// </summary>
        ///// <response code="400">Bad Request</response>
        ///// <response code="401">Unauthorized</response>
        ///// <response code="404">Not Found</response>
        ///// <response code="500">Internal Server Error</response>
        ///// <remarks>Insere um novo cliente passando um objeto no body da requisição no método POST de forma assíncrona</remarks>
        ///// <param name="model">Objeto de registro cliente</param>
        ///// <returns></returns>
        // POST: Api/Customer/AddAsync
        //[HttpPost]
        //[Route("AddAsync")]
        //public async Task<HttpResponseMessage> AddAsync([FromBody] CustomerModel model)
        //{
        //    string action = this.ActionContext.ActionDescriptor.ActionName;
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _logger.Info(action + " - Iniciado");

        //            var customerDTO = model.ConverterParaDTO();

        //            await _customerServiceApplication.AddAsync(customerDTO);

        //            _logger.Info(action + " - Sucesso!");

        //            _logger.Info(action + " - Finalizado");

        //            return Request.CreateResponse(HttpStatusCode.Created, "Customer incluído com sucesso!");
        //        }
        //        else
        //        {
        //            return ResponseManager.ReturnBadRequest(Request, _logger, action, "Por favor, preencha os campos corretamente!");
        //        }
        //    }
        //    catch (HttpResponseException ex)
        //    {
        //        if (ex.Response.StatusCode == HttpStatusCode.NotFound)
        //        {
        //            return ResponseManager.ReturnExceptionNotFound(ex, Request, _logger, action, "Nenhum registro encontrado!");
        //        }

        //        return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
        //    }

        //    catch (Exception ex)
        //    {
        //        return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
        //    }
        //}
        #endregion

        #region Update Cliente
        /// <summary>
        /// Atualizar cliente
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Atualiza o cliente passando o objeto no body da requisição pelo método PUT</remarks>
        /// <param name="model">Objeto de registro do cliente</param>
        /// <returns></returns>
        // PUT Api/Customer/Update
        [HttpPut]
        [Route("Update")]
        public HttpResponseMessage Update([FromBody] CustomerModel model)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.Info(action + " - Iniciado");

                    var customerDTO = model.ConvertToDTO();

                    _customerServiceApplication.Update(customerDTO);

                    _logger.Info(action + " - Sucesso!");

                    _logger.Info(action + " - Finalizado");

                    return Request.CreateResponse(HttpStatusCode.OK, "Cliente atualizado com sucesso!");
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

        ///// <summary>
        ///// Atualizar cliente modo assíncrono
        ///// </summary>
        ///// <response code="400">Bad Request</response>
        ///// <response code="401">Unauthorized</response>
        ///// <response code="404">Not Found</response>
        ///// <response code="500">Internal Server Error</response>
        ///// <remarks>Atualiza o cliente passando o objeto no body da requisição pelo método PUT de forma assíncrona</remarks>
        ///// <param name="model">Objeto de registro do cliente</param>
        // PUT: Api/Customer/UpdateAsync
        //[HttpPut]
        //[Route("UpdateAsync")]
        //public async Task<HttpResponseMessage> UpdateAsync([FromBody] CustomerModel model)
        //{
        //    string action = this.ActionContext.ActionDescriptor.ActionName;
        //    try
        //    {
        //        _logger.Info(action + " - Iniciado");

        //        var customerDTO = model.ConverterParaDTO();

        //        await _customerServiceApplication.UpdateAsync(customerDTO);

        //        _logger.Info(action + " - Sucesso!");

        //        _logger.Info(action + " - Finalizado");

        //        return Request.CreateResponse(System.Net.HttpStatusCode.OK, "Cliente atualizado com sucesso!");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Fatal(ex, "Erro fatal!");
        //        return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
        //    }
        //}
        #endregion

        #region Delete Customer
        /// <summary>
        /// Excluir cliente
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Exclui o cliente passando o objeto no body da requisição pelo método DELETE</remarks>
        /// <param name="model">Objeto de registro do cliente</param>
        /// <returns></returns>
        // DELETE Api/Customer/Delete
        [HttpDelete]
        [Route("Delete")]
        public HttpResponseMessage Delete([FromBody] CustomerModel model)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            _logger.Info(action + " - Iniciado");
            try
            {
                if (ModelState.IsValid)
                {
                    var customerDTO = model.ConvertToDTO();

                    _customerServiceApplication.Delete(customerDTO);

                    _logger.Info(action + " - Sucesso!");

                    _logger.Info(action + " - Finalizado");

                    return Request.CreateResponse(HttpStatusCode.OK, "Cliente excluído com sucesso!");
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

        ///// <summary>
        ///// Excluir cliente assíncrono
        ///// </summary>
        ///// <response code="400">Bad Request</response>
        ///// <response code="401">Unauthorized</response>
        ///// <response code="500">Internal Server Error</response>
        ///// <remarks>Exclui o cliente passando o objeto no body da requisição pelo método DELETE de forma assíncrona</remarks>
        ///// <param name="model">Objeto de registro do cliente</param>
        ///// <returns></returns>
        //DELETE: Api/Customer/DeleteAsync
        //[HttpDelete]
        //[Route("DeleteAsync")]
        //public async Task<HttpResponseMessage> DeleteAsync([FromBody] CustomerModel model)
        //{
        //    string action = this.ActionContext.ActionDescriptor.ActionName;
        //    try
        //    {
        //        _logger.Info(action + " - Iniciado");

        //        var customerDTO = model.ConvertToDTO();

        //        await _customerServiceApplication.DeleteAsync(customerDTO);

        //        _logger.Info(action + " - Sucesso!");

        //        _logger.Info(action + " - Finalizado");
        //        return Request.CreateResponse(System.Net.HttpStatusCode.OK, "Cliente deletado com sucesso!");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Fatal(ex, "Erro fatal!");
        //        return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
        //    }
        //}
        #endregion
    }
}
