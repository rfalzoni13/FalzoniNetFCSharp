using FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Register;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Common;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Register;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Tables.Register;
using FalzoniNetFCSharp.Utils.Helpers;
using NLog;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FalzoniNetFCSharp.Presentation.Administrator.Areas.Register.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerClient _customerClient;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public CustomerController(ICustomerClient customerClient)
        {
            _customerClient = customerClient;
        }

        // GET: Register/Customer
        public ActionResult Index()
        {
            return View();
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

        //GET: Register/Customer/Create
        [HttpGet]
        public ActionResult Create()
        {
            var model = new CustomerModel();

            try
            {
                return View(model);
            }
            catch (ApplicationException ex)
            {
                _logger.Error("Ocorreu um erro: " + ex);

                TempData["Return"] = new ReturnModel
                {
                    Type = "Error",
                    Message = ex.Message
                };

                return View("Index");
            }
            catch (Exception ex)
            {
                _logger.Fatal("Ocorreu um erro: " + ex);
                throw;
            }
        }

        // POST: Register/Customer/Create
        [HttpPost]
        public ActionResult Create(CustomerModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                string result = _customerClient.Add(UrlConfigurationHelper.CustomerCreate, model);

                TempData["Return"] = new ReturnModel
                {
                    Type = "Success",
                    Message = result
                };

                return View("Index");
            }
            catch (ApplicationException ex)
            {
                _logger.Error("Ocorreu um erro: " + ex);

                ModelState.AddModelError(string.Empty, ex.Message);

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Ocorreu um erro: " + ex);
                throw;
            }
        }

        // GET: Register/Customer/Edit
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            try
            {
                var model = await _customerClient.GetAsync(UrlConfigurationHelper.CustomerGet, id);

                return View(model);
            }
            catch (ApplicationException ex)
            {
                _logger.Error("Ocorreu um erro: " + ex);

                TempData["Return"] = new ReturnModel
                {
                    Type = "Error",
                    Message = ex.Message
                };

                return View("Index");
            }
            catch (Exception ex)
            {
                _logger.Fatal("Ocorreu um erro: " + ex);
                throw;
            }

        }

        // POST: Register/Customer/Edit
        [HttpPost]
        public ActionResult Edit(CustomerModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                string result = _customerClient.Update(UrlConfigurationHelper.CustomerEdit, model);

                TempData["Return"] = new ReturnModel
                {
                    Type = "Success",
                    Message = result
                };

                return View("Index");
            }
            catch (ApplicationException ex)
            {
                _logger.Error("Ocorreu um erro: " + ex);

                ModelState.AddModelError(string.Empty, ex.Message);

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Ocorreu um erro: " + ex);
                throw;
            }
        }

        //POST: Register/Customer/Delete
        [HttpPost]
        public async Task<ActionResult> Delete(CustomerModel model)
        {
            //List<string> errorsList = new List<string>();

            try
            {
                string result = await _customerClient.DeleteAsync(UrlConfigurationHelper.CustomerDelete, model);

                return Json(new { success = true, message = result });
            }
            catch (ApplicationException ex)
            {
                _logger.Error("Ocorreu um erro: " + ex);
                Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);

                //errorsList.Add(ex.Message);

                //return Json(new { success = false, errors = errorsList });
                return Json(new { success = false, error = ex.Message });
            }

            catch (Exception ex)
            {
                _logger.Fatal("Ocorreu um erro: " + ex);

                //errorsList.Add(ex.Message);

                if (Debugger.IsAttached)
                {
                    //errorsList.Add("Ocorreu um erro, verifique o arquivo de log e tente novamente!");
                    return Json(new { success = false, error = "Ocorreu um erro, verifique o arquivo de log e tente novamente!" });
                }

                //return Json(new { success = false, errors = errorsList });
                return Json(new { success = false, error = ex.Message });
            }
        }
    }
}