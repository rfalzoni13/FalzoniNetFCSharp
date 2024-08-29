using FalzoniNetFCSharp.Presentation.Administrator.Models.Identity;
using FalzoniNetFCSharp.Utils.Helpers;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Common;
using System.Net;
using FalzoniNetFCSharp.Presentation.Administrator.Utils;

namespace FalzoniNetFCSharp.Presentation.Administrator.Clients.Identity
{
    public class IdentityUtilityClient
    {
        #region TWO FACTORS
        public async Task<ICollection<string>> GetTwoFactorProviders()
        {
            var url = $"{PathUtils.GetApiPath()}/IdentityUtility/GetTwoFactorProviders";

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
            var url = $"{PathUtils.GetApiPath()}/IdentityUtility/SendTwoFactorProviderCode";

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
            var url = $"{PathUtils.GetApiPath()}/IdentityUtility/VerifyCodeTwoFactor";

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
            var url = $"{PathUtils.GetApiPath()}/IdentityUtility/SendEmailConfirmationCode";

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
            var url = $"{PathUtils.GetApiPath()}/IdentityUtility/SendPhoneConfirmationCode";

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
            var url = $"{PathUtils.GetApiPath()}/IdentityUtility/VerifyEmailConfirmationCode";

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
            var url = $"{PathUtils.GetApiPath()}/IdentityUtility/VerifyPhoneConfirmationCode";

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