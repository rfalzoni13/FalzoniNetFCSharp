using FalzoniNetFCSharp.Application.ServiceApplication.Stock;
using FalzoniNetFCSharp.Presentation.Api.Attributes;
using FalzoniNetFCSharp.Presentation.Api.Models.Stock;
using FalzoniNetFCSharp.Presentation.Api.Utils;
using NLog;
using System.Net.Http;
using System.Net;
using System;
using System.Web.Http;
using System.Linq;

namespace FalzoniNetFCSharp.Presentation.Api.Controllers.Admin.Stock
{
    [CustomAuthorize(Roles = "Administrator")]
    [RoutePrefix("Api/Product")]
    public class ProductController : ApiController
    {
        #region Attributes
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly ProductServiceApplication _productServiceApplication;
        #endregion

        #region Constructor
        public ProductController(ProductServiceApplication productServiceApplication)
        {
            _productServiceApplication = productServiceApplication;
        }
        #endregion

        #region Getters
        /// <summary>
        /// Listar todos os produtos
        /// </summary>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Listagem de todos os produtos</remarks>
        /// <returns></returns>
        // GET Api/Product/GetAll
        [HttpGet]
        [Route("GetAll")]
        public HttpResponseMessage GetAll()
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            _logger.Info(action + " - Iniciado");
            try
            {
                var retorno = _productServiceApplication.GetAll();

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
        /// Listar produto pelo Id
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Retorna o produto através do Id do mesmo</remarks>
        /// <param name="Id">Id do produto</param>
        /// <returns></returns>
        // GET Api/Product/Get?id={Id}
        [HttpGet]
        [Route("Get")]
        public HttpResponseMessage Get(Guid Id)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            _logger.Info(action + " - Iniciado");
            try
            {
                if (!Guid.Equals(Id, Guid.Empty))
                {
                    var product = _productServiceApplication.Get(Id);

                    if (product != null)
                    {
                        _logger.Info(action + " - Sucesso!");

                        _logger.Info(action + " - Finalizado");
                        return Request.CreateResponse(HttpStatusCode.OK, product);
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

        #region Add Product
        /// <summary>
        /// Inserir produto
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Insere um novo produto passando um objeto no body da requisição no método POST</remarks>
        /// <param name="model">Objeto de registro produto</param>
        /// <returns></returns>
        // POST Api/Product/Add
        [HttpPost]
        [Route("Add")]
        public HttpResponseMessage Add([FromBody] ProductModel model)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.Info(action + " - Iniciado");

                    var productDTO = model.ConvertToDTO();

                    _productServiceApplication.Add(productDTO);

                    _logger.Info(action + " - Sucesso!");

                    _logger.Info(action + " - Finalizado");

                    return Request.CreateResponse(HttpStatusCode.Created, "Produto incluído com sucesso!");
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
        ///// Inserir produto modo assíncrono
        ///// </summary>
        ///// <response code="400">Bad Request</response>
        ///// <response code="401">Unauthorized</response>
        ///// <response code="404">Not Found</response>
        ///// <response code="500">Internal Server Error</response>
        ///// <remarks>Insere um novo produto passando um objeto no body da requisição no método POST de forma assíncrona</remarks>
        ///// <param name="model">Objeto de registro produto</param>
        ///// <returns></returns>
        // POST: Api/Product/AddAsync
        //[HttpPost]
        //[Route("AddAsync")]
        //public async Task<HttpResponseMessage> AddAsync([FromBody] ProductModel model)
        //{
        //    string action = this.ActionContext.ActionDescriptor.ActionName;
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _logger.Info(action + " - Iniciado");

        //            var productDTO = model.ConverterParaDTO();

        //            await _productServiceApplication.AddAsync(productDTO);

        //            _logger.Info(action + " - Sucesso!");

        //            _logger.Info(action + " - Finalizado");

        //            return Request.CreateResponse(HttpStatusCode.Created, "Product incluído com sucesso!");
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

        #region Update Product
        /// <summary>
        /// Atualizar produto
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Atualiza o produto passando o objeto no body da requisição pelo método PUT</remarks>
        /// <param name="model">Objeto de registro do produto</param>
        /// <returns></returns>
        // PUT Api/Product/Update
        [HttpPut]
        [Route("Update")]
        public HttpResponseMessage Update([FromBody] ProductModel model)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.Info(action + " - Iniciado");

                    var productDTO = model.ConvertToDTO();

                    _productServiceApplication.Update(productDTO);

                    _logger.Info(action + " - Sucesso!");

                    _logger.Info(action + " - Finalizado");

                    return Request.CreateResponse(HttpStatusCode.OK, "Produto atualizado com sucesso!");
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
        ///// Atualizar produto modo assíncrono
        ///// </summary>
        ///// <response code="400">Bad Request</response>
        ///// <response code="401">Unauthorized</response>
        ///// <response code="404">Not Found</response>
        ///// <response code="500">Internal Server Error</response>
        ///// <remarks>Atualiza o produto passando o objeto no body da requisição pelo método PUT de forma assíncrona</remarks>
        ///// <param name="model">Objeto de registro do produto</param>
        // PUT: Api/Product/UpdateAsync
        //[HttpPut]
        //[Route("UpdateAsync")]
        //public async Task<HttpResponseMessage> UpdateAsync([FromBody] ProductModel model)
        //{
        //    string action = this.ActionContext.ActionDescriptor.ActionName;
        //    try
        //    {
        //        _logger.Info(action + " - Iniciado");

        //        var productDTO = model.ConverterParaDTO();

        //        await _productServiceApplication.UpdateAsync(productDTO);

        //        _logger.Info(action + " - Sucesso!");

        //        _logger.Info(action + " - Finalizado");

        //        return Request.CreateResponse(System.Net.HttpStatusCode.OK, "Produto atualizado com sucesso!");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Fatal(ex, "Erro fatal!");
        //        return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
        //    }
        //}
        #endregion

        #region Delete Product
        /// <summary>
        /// Excluir produto
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Exclui o produto passando o objeto no body da requisição pelo método DELETE</remarks>
        /// <param name="model">Objeto de registro do produto</param>
        /// <returns></returns>
        // DELETE Api/Product/Delete
        [HttpDelete]
        [Route("Delete")]
        public HttpResponseMessage Delete([FromBody] ProductModel model)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            _logger.Info(action + " - Iniciado");
            try
            {
                if (ModelState.IsValid)
                {
                    var productDTO = model.ConvertToDTO();

                    _productServiceApplication.Delete(productDTO);

                    _logger.Info(action + " - Sucesso!");

                    _logger.Info(action + " - Finalizado");

                    return Request.CreateResponse(HttpStatusCode.OK, "Produto excluído com sucesso!");
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
        ///// Excluir produto assíncrono
        ///// </summary>
        ///// <response code="400">Bad Request</response>
        ///// <response code="401">Unauthorized</response>
        ///// <response code="500">Internal Server Error</response>
        ///// <remarks>Exclui o produto passando o objeto no body da requisição pelo método DELETE de forma assíncrona</remarks>
        ///// <param name="model">Objeto de registro do produto</param>
        ///// <returns></returns>
        //// DELETE: Api/Product/DeleteAsync
        //[HttpDelete]
        //[Route("DeleteAsync")]
        //public async Task<HttpResponseMessage> DeleteAsync([FromBody] ProductModel model)
        //{
        //    string action = this.ActionContext.ActionDescriptor.ActionName;
        //    _logger.Info(action + " - Iniciado");
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var productDTO = model.ConvertToDTO();

        //            await _productServiceApplication.DeleteAsync(productDTO);

        //            _logger.Info(action + " - Sucesso!");

        //            _logger.Info(action + " - Finalizado");

        //            return Request.CreateResponse(HttpStatusCode.OK, "Produto excluído com sucesso!");
        //        }
        //        else
        //        {
        //            return ResponseManager.ReturnBadRequest(Request, _logger, action, "Por favor, preencha os campos corretamente!");
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
        //    }
        //}
        #endregion
    }
}
