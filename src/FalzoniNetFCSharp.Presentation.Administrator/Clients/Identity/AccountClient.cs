using FalzoniNetFCSharp.Presentation.Administrator.Models.Identity;
using FalzoniNetFCSharp.Utils.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System;
using Microsoft.Owin.Security;
using System.Linq;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Common;

namespace FalzoniNetFCSharp.Presentation.Administrator.Clients.Identity
{
    public class AccountClient
    {
        #region LOGIN
        public async Task Login(LoginModel model, HttpRequestBase request)
        {
            var url = UrlConfigurationHelper.AccountLogin;

            using (HttpClient httpClient = new HttpClient())
            {
                //Setar Timeout de conexão
                httpClient.Timeout = TimeSpan.FromSeconds(30);

                HttpContent content = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("username", model.Login),
                        new KeyValuePair<string, string>("password", model.Password)
                });

                HttpResponseMessage response = await httpClient.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    string resultContent = response.Content.ReadAsStringAsync().Result;

                    var token = JsonConvert.DeserializeObject<TokenModel>(resultContent);

                    AuthenticationProperties options = new AuthenticationProperties();

                    options.AllowRefresh = true;
                    options.IsPersistent = true;
                    options.ExpiresUtc = token.Expire;

                    Claim[] claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, token.UserId),
                        new Claim(ClaimTypes.Role, token.RoleId),
                        new Claim(ClaimTypes.Expiration, token.Expire.ToString()),
                        new Claim("AccessToken", token.AccessToken),
                        new Claim("RefreshToken",token.RefreshToken),
                    };

                    var identity = new ClaimsIdentity(claims, "ApplicationCookie");

                    request.GetOwinContext().Authentication.SignIn(options, identity);
                }
                else
                {
                    var errorResponse = response.Content.ReadAsAsync<ResponseErrorLogin>().Result;

                    throw new ApplicationException(!string.IsNullOrEmpty(errorResponse.error_description) ? errorResponse.error_description : errorResponse.error);
                }
            }
        }

        public async Task RefreshToken()
        {
            var url = UrlConfigurationHelper.AccountLogin;

            string refreshToken = HttpContext.Current.GetOwinContext().Authentication.User.Claims
                .FirstOrDefault(x => x.Type.Contains("RefreshToken")).Value
                ?? throw new Exception("Token expirado ou inválido");

            using (HttpClient httpClient = new HttpClient())
            {
                HttpContent content = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("grant_type", "refresh_token"),
                        new KeyValuePair<string, string>("refresh_token", refreshToken)
                });

                HttpResponseMessage response = await httpClient.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    string resultContent = await response.Content.ReadAsStringAsync();

                    HttpContext.Current.GetOwinContext().Authentication.SignOut("ApplicationCookie");

                    var token = JsonConvert.DeserializeObject<TokenModel>(resultContent);

                    AuthenticationProperties options = new AuthenticationProperties();

                    options.AllowRefresh = true;
                    options.IsPersistent = true;
                    options.ExpiresUtc = token.Expire;

                    var claims = new[]
                    {
                            new Claim(ClaimTypes.NameIdentifier, token.UserId),
                            new Claim(ClaimTypes.Role, token.RoleId),
                            new Claim(ClaimTypes.Expiration, token.Expire.ToString()),
                            new Claim("AccessToken", token.AccessToken),
                            new Claim("RefreshToken",token.RefreshToken)
                        };

                    var identity = new ClaimsIdentity(claims, "ApplicationCookie");

                    HttpContext.Current.GetOwinContext().Authentication.SignIn(options, identity);
                }
                else
                {
                    throw new ApplicationException("Login e/ou Senha incorretos!");
                }
            }
        }

        public async Task Logout(HttpRequestBase request)
        {
            var url = UrlConfigurationHelper.AccountLogout;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync(url, null);
                if (response.IsSuccessStatusCode)
                {
                    HttpContext.Current.Session.Clear();
                    request.GetOwinContext().Authentication.SignOut("ApplicationCookie");
                }
                else
                {
                    StatusCodeModel statusCode = await response.Content.ReadAsAsync<StatusCodeModel>();

                    throw new Exception(statusCode.Message);
                }
            }
        }
        #endregion

        #region EXTERNAL LOGIN
        public async Task ExternalLogin(string provider)
        {
            var url = $"{UrlConfigurationHelper.AccountExternalLogin}?provider={provider}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    StatusCodeModel statusCode = await response.Content.ReadAsAsync<StatusCodeModel>();

                    throw new ApplicationException(statusCode.Message);
                }
            }
        }

        public async Task<IList<ExternalLoginModel>> GetExternalLogins()
        {
            string url = UrlConfigurationHelper.AccountGetExternalLogins;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<IList<ExternalLoginModel>>();
                }
                else
                {
                    StatusCodeModel statusCode = await response.Content.ReadAsAsync<StatusCodeModel>();

                    throw new Exception(statusCode.Message);
                }
            }
        }

        public async Task<IList<IdentityResultCodeModel>> AddExternalLogin(AddExternalLoginBindingModel model)
        {
            string url = UrlConfigurationHelper.AccountAddExternalLogin;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(url, model);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<IList<IdentityResultCodeModel>>();
                }
                else
                {
                    StatusCodeModel statusCode = await response.Content.ReadAsAsync<StatusCodeModel>();

                    throw new Exception(statusCode.Message);
                }
            }
        }

        public async Task<IList<IdentityResultCodeModel>> AddUserExternalLogin(RegisterExternalBindingModel model)
        {
            string url = UrlConfigurationHelper.AccountAddUserExternalLogin;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(url, model);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<IList<IdentityResultCodeModel>>();
                }
                else
                {
                    StatusCodeModel statusCode = await response.Content.ReadAsAsync<StatusCodeModel>();

                    throw new Exception(statusCode.Message);
                }
            }
        }

        public async Task<IList<IdentityResultCodeModel>> RemoveExternalLogin(RegisterExternalBindingModel model)
        {
            string url = UrlConfigurationHelper.AccountRemoveExternalLogin;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(url, model);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<IList<IdentityResultCodeModel>>();
                }
                else
                {
                    StatusCodeModel statusCode = await response.Content.ReadAsAsync<StatusCodeModel>();

                    throw new Exception(statusCode.Message);
                }
            }
        }
        #endregion

        #region PASSWORD
        public async Task<IdentityResultCodeModel> ChangePassword(ChangePasswordModel model)
        {
            var url = UrlConfigurationHelper.AccountChangePassword;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(url, model);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<IdentityResultCodeModel>();
                }
                else
                {
                    StatusCodeModel statusCode = await response.Content.ReadAsAsync<StatusCodeModel>();

                    throw new ApplicationException(statusCode.Message);
                }
            }
        }

        public async Task<string> ForgotPassword(ForgotPasswordModel model)
        {
            var url = UrlConfigurationHelper.AccountForgotPassword;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(url, model);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    StatusCodeModel statusCode = await response.Content.ReadAsAsync<StatusCodeModel>();

                    throw new ApplicationException(statusCode.Message);
                }
            }
        }

        public async Task<IdentityResultCodeModel> ResetPassword(ResetPasswordModel model)
        {
            var url = UrlConfigurationHelper.AccountResetPassword;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(url, model);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<IdentityResultCodeModel>();
                }
                else
                {
                    StatusCodeModel statusCode = await response.Content.ReadAsAsync<StatusCodeModel>();

                    throw new ApplicationException(statusCode.Message);
                }
            }
        }

        #endregion

    }
}