using FalzoniNetFCSharp.Domain.DTO.Register;
using FalzoniNetFCSharp.Presentation.Api.Attributes;
using FalzoniNetFCSharp.Presentation.Api.Controllers.Base;
using FalzoniNetFCSharp.Service.Register;
using System;
using System.Net.Http;
using System.Web.Http;

namespace FalzoniNetFCSharp.Presentation.Api.Controllers.Admin.Register
{
    [CustomAuthorize(Roles = "Administrator")]
    public class CustomerController : BaseController<CustomerDTO>
    {
        #region Constructor
        public CustomerController(CustomerService customerService)
            :base(customerService)
        {
        }
        #endregion

        #region Getters
        /// <summary>
        /// Listar todos os clientes
        /// </summary>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Listagem de todos os clientes</remarks>
        /// <returns></returns>
        // GET Api/Customer/GetAll
        [HttpGet]
        public override HttpResponseMessage GetAll()
        {
            return base.GetAll();
        }

        /// <summary>
        /// Listar cliente pelo Id
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Retorna o cliente através do Id do mesmo</remarks>
        /// <param name="Id">Id do cliente</param>
        /// <returns></returns>
        // GET Api/Customer/Get/{Id}
        [HttpGet]
        public override HttpResponseMessage Get(Guid Id)
        {
            return base.Get(Id);
        }
        #endregion

        #region Add Customer
        /// <summary>
        /// Inserir cliente
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Insere um novo cliente passando um objeto no body da requisição no método POST</remarks>
        /// <param name="dto">Objeto de registro cliente</param>
        /// <returns></returns>
        // POST Api/Customer/Add
        [HttpPost]
        public override HttpResponseMessage Add([FromBody] CustomerDTO dto)
        {
            return base.Add(dto);
        }
        #endregion

        #region Update Cliente
        /// <summary>
        /// Atualizar cliente
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Atualiza o cliente passando o objeto no body da requisição pelo método PUT</remarks>
        /// <param name="dto">Objeto de registro do cliente</param>
        /// <returns></returns>
        // PUT Api/Customer/Update
        [HttpPut]
        public override HttpResponseMessage Update([FromBody] CustomerDTO dto)
        {
            return base.Update(dto);
        }
        #endregion

        #region Delete Customer
        /// <summary>
        /// Excluir cliente
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Exclui o cliente passando o objeto no body da requisição pelo método DELETE</remarks>
        /// <param name="Id">Id de registro do cliente</param>
        /// <returns></returns>
        // DELETE Api/Customer/Delete
        [HttpDelete]
        public override HttpResponseMessage Delete([FromUri] Guid Id)
        {
            return base.Delete(Id);
        }
        #endregion
    }
}
