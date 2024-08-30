using FalzoniNetFCSharp.Domain.DTO.Stock;
using FalzoniNetFCSharp.Presentation.Api.Attributes;
using FalzoniNetFCSharp.Presentation.Api.Controllers.Base;
using FalzoniNetFCSharp.Service.Stock;
using System;
using System.Net.Http;
using System.Web.Http;

namespace FalzoniNetFCSharp.Presentation.Api.Controllers.Admin.Stock
{
    [CustomAuthorize(Roles = "Administrator")]
    public class ProductController : BaseController<ProductDTO>
    {
        #region Constructor
        public ProductController(ProductService productService)
            :base(productService)
        {
        }
        #endregion

        #region Getters
        /// <summary>
        /// Listar todos os produtos
        /// </summary>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Listagem de todos os produtos</remarks>
        /// <returns></returns>
        // GET Api/Product/GetAll
        [HttpGet]
        public override HttpResponseMessage GetAll()
        {
            return base.GetAll();
        }

        /// <summary>
        /// Listar produto pelo Id
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Retorna o produto através do Id do mesmo</remarks>
        /// <param name="Id">Id do produto</param>
        /// <returns></returns>
        // GET Api/Product/Get/{Id}
        [HttpGet]
        public override HttpResponseMessage Get(Guid Id)
        {
            return base.Get(Id);
        }
        #endregion

        #region Add Product
        /// <summary>
        /// Inserir produto
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Insere um novo produto passando um objeto no body da requisição no método POST</remarks>
        /// <param name="dto">Objeto de registro produto</param>
        /// <returns></returns>
        // POST Api/Product/Add
        [HttpPost]
        public override HttpResponseMessage Add([FromBody] ProductDTO dto)
        {
            return base.Add(dto);
        }
        #endregion

        #region Update Product
        /// <summary>
        /// Atualizar produto
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Atualiza o produto passando o objeto no body da requisição pelo método PUT</remarks>
        /// <param name="dto">Objeto de registro do produto</param>
        /// <returns></returns>
        // PUT Api/Product/Update
        [HttpPut]
        public override HttpResponseMessage Update([FromBody] ProductDTO dto)
        {
            return base.Update(dto);
        }
        #endregion

        #region Delete Product
        /// <summary>
        /// Excluir produto
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        /// <remarks>Exclui o produto passando o objeto no body da requisição pelo método DELETE</remarks>
        /// <param name="Id">Id de identificação do produto</param>
        /// <returns></returns>
        // DELETE Api/Product/Delete
        [HttpDelete]
        public override HttpResponseMessage Delete([FromUri] Guid Id)
        {
            return base.Delete(Id);
        }
        #endregion
    }
}
