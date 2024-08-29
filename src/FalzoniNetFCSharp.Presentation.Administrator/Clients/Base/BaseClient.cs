using FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Base;
using FalzoniNetFCSharp.Presentation.Administrator.Utils;
using FalzoniNetFCSharp.Utils.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FalzoniNetFCSharp.Presentation.Administrator.Clients.Base
{
    public class BaseClient<T> : IBaseClient<T>
        where T : class
    {
        protected string token;
        protected string url;

        public BaseClient()
        {
            token = RequestHelper.GetAccessToken();
            url = PathUtils.GetApiPath();
        }

        public virtual string Add(T obj)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.PostAsJsonAsync($"{url}/Add", obj).Result;
                string message = ResponseUtils<T>.ReturnMessageString(response);
                return message;
            }
        }

        public virtual async Task<string> AddAsync(T obj)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.PostAsJsonAsync($"{url}/AddAsync", obj);
                string message = await ResponseUtils<T>.ReturnMessageStringAsync(response);
                return message;
            }
        }

        public virtual string Update(T obj)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.PutAsJsonAsync($"{url}/Update", obj).Result;
                string message = ResponseUtils<T>.ReturnMessageString(response);
                return message;
            }
        }

        public virtual async Task<string> UpdateAsync(T obj)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.PutAsJsonAsync($"{url}/UpdateAsync", obj);
                string message = await ResponseUtils<T>.ReturnMessageStringAsync(response);
                return message;
            }
        }

        public virtual string Delete(T obj)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri($"{url}/Delete"),
                    Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
                };
                HttpResponseMessage response = client.SendAsync(request).Result;
                string message = ResponseUtils<T>.ReturnMessageString(response);
                return message;
            }
        }

        public virtual async Task<string> DeleteAsync(T obj)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri($"{url}/DeleteAsync"),
                    Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
                };
                HttpResponseMessage response = await client.SendAsync(request);
                string message = await ResponseUtils<T>.ReturnMessageStringAsync(response);
                return message;
            }
        }

        public virtual T Get(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.GetAsync($"{url}/Get?id={id}").Result;
                T obj = ResponseUtils<T>.ReturnObject(response);
                return obj;
            }
        }

        public virtual async Task<T> GetAsync(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.GetAsync($"{url}/GetAsync?id={id}");
                T obj = await ResponseUtils<T>.ReturnObjectAsync(response);
                return obj;
            }

        }

        public virtual ICollection<T> GetAll()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.GetAsync($"{url}/GetAll").Result;
                ICollection<T> collection = ResponseUtils<ICollection<T>>.ReturnObject(response);
                return collection;
            }
        }

        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.GetAsync($"{url}/GetAllAsync");
                ICollection<T> collection = await ResponseUtils<ICollection<T>>.ReturnObjectAsync(response);
                return collection;
            }
        }
    }
}