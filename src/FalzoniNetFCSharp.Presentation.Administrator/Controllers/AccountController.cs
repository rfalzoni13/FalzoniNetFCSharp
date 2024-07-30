using FalzoniNetFCSharp.Presentation.Administrator.Clients.Identity;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Identity;
using FalzoniNetFCSharp.Utils.Helpers;
using NLog;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FalzoniNetFCSharp.Presentation.Administrator.Controllers
{
    public class AccountController : Controller
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly AccountClient _accountClient;
        private readonly IdentityUtilityClient _identityUtilityClient;

        public AccountController(AccountClient accountClient, IdentityUtilityClient identityUtilityClient)
        {
            _accountClient = accountClient;
            _identityUtilityClient = identityUtilityClient;
        }

        #region Login
        // GET: Account/Login
        [HttpGet]
        public ActionResult Login()
        {
            if (Request.GetOwinContext().Authentication.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                var model = new LoginModel();
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Ocorreu um erro: " + ex);
                throw;
            }
        }

        // POST: Account/Login
        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            try
            {
                await _accountClient.Login(model, Request);

                return RedirectToAction("Index", "Home");
            }
            catch (ApplicationException ex)
            {
                ModelState.Clear();

                ModelState.AddModelError(string.Empty, ExceptionHelper.CatchMessageFromException(ex));

                return View();
            }
            catch (TaskCanceledException ex)
            {
                _logger.Error("Ocorreu um erro interno do servidor: " + ex);

                ModelState.Clear();

                ModelState.AddModelError(string.Empty, ExceptionHelper.CatchMessageFromException(ex));

                return View();
            }
            catch (Exception ex)
            {
                _logger.Fatal("Ocorreu um erro: " + ex);
                throw;
            }
        }
        #endregion

        #region Logout
        //POST: Account/LogOut
        [HttpPost]
        public async Task<ActionResult> LogOut()
        {
            try
            {
                await _accountClient.Logout(Request);

                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Erro fatal!");
                throw;
            }
        }
        #endregion

        #region ExternalLogin
        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLogin(string provider, string returnUrl)
        {
            await _accountClient.ExternalLogin(provider);

            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region SendCode
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            try
            {
                var userFactors = await _identityUtilityClient.GetTwoFactorProviders();

                var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();

                return View(new SendCodeModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Erro fatal!");
                throw;
            }
        }

        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeModel model)
        {
            try
            {
                await _identityUtilityClient.SendTwoFactorProviderCode(model);

                return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, model.ReturnUrl, model.RememberMe });
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Erro fatal!");
                throw;
            }
        }
        #endregion

        #region VerifyCode
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public ActionResult VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            try
            {
                if (!Request.GetOwinContext().Authentication.User.Identity.IsAuthenticated)
                    throw new Exception("Não autorizado!");

                return View(new VerifyCodeModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Erro fatal!");
                throw;
            }
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeModel model)
        {
            var result = await _identityUtilityClient.VerifyCodeTwoFactor(model);

            return RedirectToLocal(result.ReturnUrl);
        }
        #endregion

        #region ForgotPassword
        //GET: /Account/ForgotPassword
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            var model = new ForgotPasswordModel();

            return View(model);
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            try
            {
                model.CallBackUrl = Url.Action("ResetPassword", "Account", new { userId = "{0}", code = "{1}" }, protocol: Request.Url.Scheme);

                await _accountClient.ForgotPassword(model);

                return View(model);
            }
            catch (ApplicationException ex)
            {
                _logger.Error(ex, ex.Message);
                return RedirectToAction("ConfirmResetPassword", "Account");
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Erro fatal!");
                throw;
            }
        }
        #endregion

        #region ResetPassword
        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordModel model)
        {
            try
            {
                var result = await _accountClient.ResetPassword(model);

                if (result.Succeeded)
                {
                    return View(model);
                }
                else
                {
                    return RedirectToAction("ConfirmResetPassword", "Account");
                }
            }
            catch (ApplicationException ex)
            {
                _logger.Error(ex, ex.Message);
                return RedirectToAction("ConfirmResetPassword", "Account");
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Erro fatal!");
                throw;
            }
        }
        #endregion

        #region ConfirmResetPassword
        //
        // GET: /Account/ConfirmResetPassword
        [AllowAnonymous]
        public ActionResult ConfirmResetPassword()
        {
            return View();
        }
        #endregion

        #region private METHODS
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}