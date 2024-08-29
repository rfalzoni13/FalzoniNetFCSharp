using FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Register;
using FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Stock;
using FalzoniNetFCSharp.Presentation.Administrator.Controllers.Base;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Stock;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Tables.Register;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Tables.Stock;
using FalzoniNetFCSharp.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FalzoniNetFCSharp.Presentation.Administrator.Areas.Stock.Controllers
{
    public class ProductController : BaseController<ProductModel>
    {
        private readonly IProductClient _productClient;

        public ProductController(IProductClient productClient)
            : base(productClient)
        {
            _productClient = productClient;
        }

        //GET: Register/Product/LoadTable
        [HttpGet]
        public async Task<JsonResult> LoadTable()
        {
            var table = new ProductTableModel();

            try
            {
                table = await _productClient.GetTableAsync(UrlConfigurationHelper.ProductGetAll);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Ocorreu um erro: " + ex);
            }

            return Json(table, JsonRequestBehavior.AllowGet);
        }
    }
}