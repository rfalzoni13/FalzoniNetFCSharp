using FalzoniNetFCSharp.Presentation.Administrator.Attributes;
using FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Base;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Common;
using FalzoniNetFCSharp.Utils.Helpers;
using NLog;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FalzoniNetFCSharp.Presentation.Administrator.Controllers.Base
{
    public class BaseController<TModel> : Controller 
        where TModel : class, new()
    {
        private readonly IBaseClient<TModel> _baseClient;
        protected readonly ILogger _logger;

        public BaseController(IBaseClient<TModel> baseClient)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _baseClient = baseClient;
        }

        // GET: Base
        public ActionResult Index()
        {
            return View();
        }

        //GET: Create
        [UserConfiguration]
        [HttpGet]
        public ActionResult Create()
        {
            var model = new TModel();

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

        // POST: Create
        [UserConfiguration]
        [HttpPost]
        public ActionResult Create(TModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                string result = _baseClient.Add(model);

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

        // GET: Edit
        [UserConfiguration]
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            try
            {
                var model = await _baseClient.GetAsync(id);

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

        // POST: Edit
        [UserConfiguration]
        [HttpPost]
        public ActionResult Edit(TModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                string result = _baseClient.Update(model);

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
        public async Task<ActionResult> Delete(TModel model)
        {
            //List<string> errorsList = new List<string>();

            try
            {
                string result = await _baseClient.DeleteAsync(model);

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