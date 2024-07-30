using System.Web.Mvc;

namespace FalzoniNetFCSharp.Presentation.Administrator.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}