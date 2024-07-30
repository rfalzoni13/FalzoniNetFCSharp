using FalzoniNetFCSharp.Presentation.Administrator.Attributes;
using FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Configuration;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Common;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Configuration;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Tables.Configuration;
using FalzoniNetFCSharp.Utils.Helpers;
using NLog;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FalzoniNetFCSharp.Presentation.Administrator.Areas.Configuration.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserClient _userClient;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public UserController(IUserClient userClient)
        {
            _userClient = userClient;
        }

        // GET: Configuration/User
        public ActionResult Index()
        {
            return View();
        }

        //GET: Configuration/User/LoadTable
        [HttpGet]
        public async Task<JsonResult> LoadTable()
        {
            var tabela = new UserTableModel();

            try
            {
                tabela = await _userClient.GetTableAsync(UrlConfigurationHelper.UserGetAll);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Ocorreu um erro: " + ex);
            }

            return Json(tabela, JsonRequestBehavior.AllowGet);
        }

        //GET: Configuration/User/Create
        [UserConfiguration]
        [HttpGet]
        public ActionResult Create()
        {
            var model = new UserModel();

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

        // POST: Configuration/User/Create
        [UserConfiguration]
        [HttpPost]
        public ActionResult Create(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                string result = _userClient.Add(UrlConfigurationHelper.UserCreate, model);

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

        // GET: Configuration/User/Edit
        [UserConfiguration]
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            try
            {
                var model = await _userClient.GetAsync(UrlConfigurationHelper.UserGet, id);

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

        // POST: Configuration/User/Edit
        [UserConfiguration]
        [HttpPost]
        public ActionResult Edit(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                string result = _userClient.Update(UrlConfigurationHelper.UserEdit, model);

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

        //POST: Configuration/User/Delete
        [HttpPost]
        public async Task<ActionResult> Delete(UserModel model)
        {
            //List<string> errorsList = new List<string>();

            try
            {
                string result = await _userClient.DeleteAsync(UrlConfigurationHelper.UserDelete, model);

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