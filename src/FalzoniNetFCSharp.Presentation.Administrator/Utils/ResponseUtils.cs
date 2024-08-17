using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Common;

namespace FalzoniNetFCSharp.Presentation.Administrator.Utils
{
    public static class ResponseUtils<T> where T : class
    {
        public static string ReturnMessageString(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode) return response.Content.ReadAsStringAsync().Result;

            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    throw new ApplicationException("Caminho ou serviço não encontrado!");

                default:
                    StatusCodeModel statusCode = response.Content.ReadAsAsync<StatusCodeModel>().Result;

                    throw new ApplicationException(statusCode.Message);
            }
        }

        public static async Task<string> ReturnMessageStringAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    throw new ApplicationException("Caminho ou serviço não encontrado!");

                default:
                    StatusCodeModel statusCode = await response.Content.ReadAsAsync<StatusCodeModel>();

                    throw new ApplicationException(statusCode.Message);
            }
        }

        public static T ReturnObject(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode) return response.Content.ReadAsAsync<T>().Result;

            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    throw new ApplicationException("Caminho ou serviço não encontrado!");

                default:
                    StatusCodeModel statusCode = response.Content.ReadAsAsync<StatusCodeModel>().Result;

                    throw new ApplicationException(statusCode.Message);
            }
        }

        public static async Task<T> ReturnObjectAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode) return await response.Content.ReadAsAsync<T>();

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