using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FalzoniNetFCSharp.Presentation.Administrator.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            Response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);

            return View();
        }

        //GET: Error/NotFound
        public ActionResult NotFound()
        {
            Response.StatusCode = Convert.ToInt32(HttpStatusCode.NotFound);

            return View("NotFound");
        }
    }
}