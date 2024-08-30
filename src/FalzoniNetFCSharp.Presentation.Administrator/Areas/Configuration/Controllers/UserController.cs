using FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Configuration;
using FalzoniNetFCSharp.Presentation.Administrator.Controllers.Base;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Configuration;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Tables.Configuration;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FalzoniNetFCSharp.Presentation.Administrator.Areas.Configuration.Controllers
{
    [Authorize]
    public class UserController : BaseController<UserModel>
    {
        private readonly IUserClient _userClient;
        public UserController(IUserClient userClient)
            :base(userClient)
        {
            _userClient = userClient;
        }

        //GET: Configuration/User/LoadTable
        [HttpGet]
        public async Task<JsonResult> LoadTable()
        {
            var tabela = new UserTableModel();

            try
            {
                tabela = await _userClient.GetTableAsync();
            }
            catch (Exception ex)
            {
                _logger.Fatal("Ocorreu um erro: " + ex);
            }

            return Json(tabela, JsonRequestBehavior.AllowGet);
        }
    }
}