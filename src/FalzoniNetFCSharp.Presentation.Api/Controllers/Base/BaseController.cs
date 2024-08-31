using FalzoniNetFCSharp.Presentation.Api.Utils;
using FalzoniNetFCSharp.Service.Base;
using NLog;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FalzoniNetFCSharp.Presentation.Api.Controllers.Base
{
    public class BaseController<TDTO> : ApiController
        where TDTO : class
    {
        #region Attributes
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly ServiceBase<TDTO> _serviceBase;

        #endregion

        #region Constructor
        public BaseController(ServiceBase<TDTO> serviceBase)
        {
            _serviceBase = serviceBase;
        }
        #endregion

        #region Getters
        // GET Api/GetAll
        [HttpGet]
        public virtual HttpResponseMessage GetAll()
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            _logger.Info(action + " - Iniciado");
            try
            {
                var retorno = _serviceBase.GetAll();

                _logger.Info(action + " - Sucesso!");

                _logger.Info(action + " - Finalizado");
                return Request.CreateResponse(HttpStatusCode.OK, retorno);
            }

            catch (Exception ex)
            {
                return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
            }
        }

        // GET Api/Get/{Id}
        [HttpGet]
        public virtual HttpResponseMessage Get(Guid Id)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            _logger.Info(action + " - Iniciado");

            try
            {
                _logger.Info(action + " - Iniciado");
                if (Guid.Equals(Id, Guid.Empty))
                    throw new ApplicationException("Parâmetro incorreto!");

                var product = _serviceBase.Get(Id);

                _logger.Info(action + " - Sucesso!");

                _logger.Info(action + " - Finalizado");
                return Request.CreateResponse(HttpStatusCode.OK, product);
            }
            catch (ApplicationException ex)
            {
                return ResponseManager.ReturnBadRequest(ex, Request, _logger, action);
            }

            catch (Exception ex)
            {
                return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
            }
        }
        #endregion

        #region Add
        // POST Api/Add
        [HttpPost]
        public virtual HttpResponseMessage Add([FromBody] TDTO dto)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.Info(action + " - Iniciado");

                    _serviceBase.Add(dto);

                    _logger.Info(action + " - Sucesso!");

                    _logger.Info(action + " - Finalizado");

                    return Request.CreateResponse(HttpStatusCode.Created, "Registro incluído com sucesso!");
                }
                else
                {
                    throw new ApplicationException("Por favor, preencha os campos corretamente!");
                }
            }

            catch (ApplicationException ex)
            {
                return ResponseManager.ReturnBadRequest(ex, Request, _logger, action);
            }

            catch (Exception ex)
            {
                return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
            }
        }

        // POST: Api/AddAsync
        //[HttpPost]
        //public virtual async Task<HttpResponseMessage> AddAsync([FromBody] TDTO dto)
        //{
        //    string action = this.ActionContext.ActionDescriptor.ActionName;
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _logger.Info(action + " - Iniciado");

        //            await _serviceBase.AddAsync(dto);

        //            _logger.Info(action + " - Sucesso!");

        //            _logger.Info(action + " - Finalizado");

        //            return Request.CreateResponse(HttpStatusCode.Created, "Registro incluído com sucesso!");
        //        }
        //        else
        //        {
        //            throw new ApplicationException("Por favor, preencha os campos corretamente!");
        //        }
        //    }

        //    catch (ApplicationException ex)
        //    {
        //        return ResponseManager.ReturnBadRequest(ex, Request, _logger, action);
        //    }

        //    catch (Exception ex)
        //    {
        //        return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
        //    }
        //}
        #endregion

        #region Update
        // PUT Api/Update
        [HttpPut]
        public virtual HttpResponseMessage Update([FromBody] TDTO dto)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.Info(action + " - Iniciado");

                    _serviceBase.Update(dto);

                    _logger.Info(action + " - Sucesso!");

                    _logger.Info(action + " - Finalizado");

                    return Request.CreateResponse(HttpStatusCode.OK, "Registro atualizado com sucesso!");
                }
                else
                {
                    throw new ApplicationException("Por favor, preencha os campos corretamente!");
                }
            }

            catch (ApplicationException ex)
            {
                return ResponseManager.ReturnBadRequest(ex, Request, _logger, action);
            }

            catch (Exception ex)
            {
                return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
            }
        }
        // PUT: Api/UpdateAsync
        //[HttpPut]
        //public virtual async Task<HttpResponseMessage> UpdateAsync([FromBody] TDTO dto)
        //{
        //    string action = this.ActionContext.ActionDescriptor.ActionName;
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _logger.Info(action + " - Iniciado");

        //            await _serviceBase.UpdateAsync(dto);

        //            _logger.Info(action + " - Sucesso!");

        //            _logger.Info(action + " - Finalizado");

        //            return Request.CreateResponse(HttpStatusCode.OK, "Registro atualizado com sucesso!");
        //        }
        //        else
        //        {
        //            throw new ApplicationException("Por favor, preencha os campos corretamente!");
        //        }
        //    }

        //    catch (ApplicationException ex)
        //    {
        //        return ResponseManager.ReturnBadRequest(ex, Request, _logger, action);
        //    }

        //    catch (Exception ex)
        //    {
        //        return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
        //    }
        //}
        #endregion

        #region Delete 
        // DELETE Api/Delete
        [HttpDelete]
        public virtual HttpResponseMessage Delete([FromUri] Guid Id)
        {
            string action = this.ActionContext.ActionDescriptor.ActionName;
            _logger.Info(action + " - Iniciado");
            try
            {
                if (ModelState.IsValid)
                {
                    _serviceBase.Delete(Id);

                    _logger.Info(action + " - Sucesso!");

                    _logger.Info(action + " - Finalizado");

                    return Request.CreateResponse(HttpStatusCode.OK, "Registro removido com sucesso!");
                }
                else
                {
                    throw new ApplicationException("Por favor, preencha os campos corretamente!");
                }
            }

            catch (ApplicationException ex)
            {
                return ResponseManager.ReturnBadRequest(ex, Request, _logger, action);
            }

            catch (Exception ex)
            {
                return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
            }
        }

        //// DELETE: Api/DeleteAsync
        //[HttpDelete]
        //public virtual async Task<HttpResponseMessage> DeleteAsync([FromUri] Guid Id)
        //{
        //    string action = this.ActionContext.ActionDescriptor.ActionName;
        //    _logger.Info(action + " - Iniciado");
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            await _productService.DeleteAsync(model.Id);

        //            _logger.Info(action + " - Sucesso!");

        //            _logger.Info(action + " - Finalizado");

        //            return Request.CreateResponse(HttpStatusCode.OK, "Registro excluído com sucesso!");
        //        }
        //        else
        //        {
        //            throw new ApplicationException("Por favor, preencha os campos corretamente!");
        //        }
        //    }

        //    catch (ApplicationException ex)
        //    {
        //        return ResponseManager.ReturnBadRequest(ex, Request, _logger, action);
        //    }

        //    catch (Exception ex)
        //    {
        //        return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action);
        //    }
        //}
        #endregion
    }
}
