using FalzoniNetFCSharp.Presentation.Administrator.Models.Identity;
using FalzoniNetFCSharp.Utils.Helpers;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Common;
using System.Net;

namespace FalzoniNetFCSharp.Presentation.Administrator.Clients.Identity
{
    public class IdentityUtilityClient
    {
        #region TWO FACTORS
        public async Task<ICollection<string>> GetTwoFactorProviders()
        {
            var url = UrlConfigurationHelper.IdentityUtilityGetTwoFactorProviders;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<ICollection<string>>();
                }
                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        throw new ApplicationException("Caminho ou serviço não encontrado!");

                    default:
                        StatusCodeModel statusCode = await response.Content.ReadAsAsync<StatusCodeModel>();

                        throw new ApplicationException(statusCode.Message);
                }
            }
        }

        public async Task SendTwoFactorProviderCode(SendCodeModel model)
        {
            var url = UrlConfigurationHelper.IdentityUtilitySendTwoFactorProviderCode;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(url, model);
                if (!response.IsSuccessStatusCode)
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            throw new ApplicationException("Caminho ou serviço não encontrado!");

                        default:
                            StatusCodeModel statusCode = await response.Content.ReadAsAsync<StatusCodeModel>();

                            throw new ApplicationException(statusCode.Message);
                    }
                }
            }
        }

        public async Task<ReturnVerifyCodeModel> VerifyCodeTwoFactor(VerifyCodeModel model)
        {
            string url = UrlConfigurationHelper.IdentityUtilityVerifyCodeTwoFactor;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(url, model);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<ReturnVerifyCodeModel>();
                }
                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        throw new ApplicationException("Caminho ou serviço não encontrado!");

                    default:
                        StatusCodeModel statusCode = await response.Content.ReadAsAsync<StatusCodeModel>();

                        throw new ApplicationException(statusCode.Message);
                }
            }
        }
        #endregion

        #region PHONE AND E-MAIL CONFIRMATION
        public async Task<string> SendEmailConfirmationCode(GenerateTokenEmailModel model)
        {
            string url = UrlConfigurationHelper.IdentityUtilitySendEmailConfirmationCode;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(url, model);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        throw new ApplicationException("Caminho ou serviço não encontrado!");

                    default:
                        StatusCodeModel statusCode = await response.Content.ReadAsAsync<StatusCodeModel>();

                        throw new ApplicationException(statusCode.Message);
                }
            }
        }

        public async Task<string> SendPhoneConfirmationCode(GenerateTokenPhoneModel model)
        {
            string url = UrlConfigurationHelper.IdentityUtilitySendPhoneConfirmationCode;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(url, model);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        throw new ApplicationException("Caminho ou serviço não encontrado!");

                    default:
                        StatusCodeModel statusCode = await response.Content.ReadAsAsync<StatusCodeModel>();

                        throw new ApplicationException(statusCode.Message);
                }
            }
        }

        public async Task<string> VerifyEmailConfirmationCode(ConfirmEmailCodeModel model)
        {
            string url = UrlConfigurationHelper.IdentityUtilityVerifyEmailConfirmationCode;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(url, model);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        throw new ApplicationException("Caminho ou serviço não encontrado!");

                    default:
                        StatusCodeModel statusCode = await response.Content.ReadAsAsync<StatusCodeModel>();

                        throw new ApplicationException(statusCode.Message);
                }
            }
        }

        public async Task<string> VerifyPhoneConfirmationCode(ConfirmPhoneCodeModel model)
        {
            string url = UrlConfigurationHelper.IdentityUtilityVerifyPhoneConfirmationCode;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(url, model);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        throw new ApplicationException("Caminho ou serviço não encontrado!");

                    default:
                        StatusCodeModel statusCode = await response.Content.ReadAsAsync<StatusCodeModel>();

                        throw new ApplicationException(statusCode.Message);
                }
            }
        }
        #endregion
    }
}