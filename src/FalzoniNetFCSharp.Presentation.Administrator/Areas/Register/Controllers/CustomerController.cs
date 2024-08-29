using FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Register;
using FalzoniNetFCSharp.Presentation.Administrator.Controllers.Base;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Register;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Tables.Register;
using FalzoniNetFCSharp.Utils.Helpers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FalzoniNetFCSharp.Presentation.Administrator.Areas.Register.Controllers
{
    [Authorize]
    public class CustomerController : BaseController<CustomerModel>
    {
        private readonly ICustomerClient _customerClient;

        public CustomerController(ICustomerClient customerClient)
            : base(customerClient)
        {
            _customerClient = customerClient;
        }

        //GET: Register/Customer/LoadTable
        [HttpGet]
        public async Task<JsonResult> LoadTable()
        {
            var table = new CustomerTableModel();

            try
            {
                table = await _customerClient.GetTableAsync(UrlConfigurationHelper.CustomerGetAll);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Ocorreu um erro: " + ex);
            }

            return Json(table, JsonRequestBehavior.AllowGet);
        }
    }
}