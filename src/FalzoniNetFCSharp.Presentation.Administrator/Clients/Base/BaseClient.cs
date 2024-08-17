using FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Base;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Common;
using FalzoniNetFCSharp.Presentation.Administrator.Utils;
using FalzoniNetFCSharp.Utils.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FalzoniNetFCSharp.Presentation.Administrator.Clients.Base
{
    public class BaseClient<T> : IBaseClient<T>
        where T : class
    {
        protected string token;

        public BaseClient()
        {
            token = RequestHelper.GetAccessToken();
        }

        public virtual string Add(string url, T obj)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.PostAsJsonAsync(url, obj).Result;
                string message = ResponseUtils<T>.ReturnMessageString(response);
                return message;
            }
        }

        public virtual async Task<string> AddAsync(string url, T obj)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.PostAsJsonAsync(url, obj);
                string message = await ResponseUtils<T>.ReturnMessageStringAsync(response);
                return message;
            }
        }

        public virtual string Update(string url, T obj)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.PutAsJsonAsync(url, obj).Result;
                string message = ResponseUtils<T>.ReturnMessageString(response);
                return message;
            }
        }

        public virtual async Task<string> UpdateAsync(string url, T obj)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.PutAsJsonAsync(url, obj);
                string message = await ResponseUtils<T>.ReturnMessageStringAsync(response);
                return message;
            }
        }

        public virtual string Delete(string url, T obj)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(url),
                    Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
                };
                HttpResponseMessage response = client.SendAsync(request).Result;
                string message = ResponseUtils<T>.ReturnMessageString(response);
                return message;
            }
        }

        public virtual async Task<string> DeleteAsync(string url, T obj)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(url),
                    Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
                };
                HttpResponseMessage response = await client.SendAsync(request);
                string message = await ResponseUtils<T>.ReturnMessageStringAsync(response);
                return message;
            }
        }

        public virtual T Get(string url, string id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.GetAsync($"{url}?id={id}").Result;
                T obj = ResponseUtils<T>.ReturnObject(response);
                return obj;
            }
        }

        public virtual async Task<T> GetAsync(string url, string id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.GetAsync($"{url}?id={id}");
                T obj = await ResponseUtils<T>.ReturnObjectAsync(response);
                return obj;
            }

        }

        public virtual ICollection<T> GetAll(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.GetAsync(url).Result;
                ICollection<T> collection = ResponseUtils<ICollection<T>>.ReturnObject(response);
                return collection;
            }
        }

        public virtual async Task<ICollection<T>> GetAllAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.GetAsync(url);
                ICollection<T> collection = await ResponseUtils<ICollection<T>>.ReturnObjectAsync(response);
                return collection;
            }
        }
    }
}