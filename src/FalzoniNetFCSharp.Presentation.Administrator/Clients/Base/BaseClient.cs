using FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Base;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Common;
using FalzoniNetFCSharp.Utils.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FalzoniNetFCSharp.Presentation.Administrator.Clients.Base
{
    public class BaseClient<T, TTable> : IBaseClient<T, TTable>
        where T : class
        where TTable : class
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
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    StatusCodeModel statusCode = response.Content.ReadAsAsync<StatusCodeModel>().Result;

                    throw new ApplicationException(statusCode.Message);
                }
            }
        }

        public virtual async Task<string> AddAsync(string url, T obj)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.PostAsJsonAsync(url, obj);
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

        public virtual string Update(string url, T obj)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.PutAsJsonAsync(url, obj).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    StatusCodeModel statusCode = response.Content.ReadAsAsync<StatusCodeModel>().Result;

                    throw new ApplicationException(statusCode.Message);
                }
            }
        }

        public virtual async Task<string> UpdateAsync(string url, T obj)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.PutAsJsonAsync(url, obj);
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
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    StatusCodeModel statusCode = response.Content.ReadAsAsync<StatusCodeModel>().Result;

                    throw new ApplicationException(statusCode.Message);
                }
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

        public virtual T Get(string url, string id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.GetAsync($"{url}?id={id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<T>().Result;
                }
                else
                {
                    StatusCodeModel statusCode = response.Content.ReadAsAsync<StatusCodeModel>().Result;

                    throw new ApplicationException(statusCode.Message);
                }
            }
        }

        public virtual async Task<T> GetAsync(string url, string id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.GetAsync($"{url}?id={id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<T>();
                }
                else
                {
                    StatusCodeModel statusCode = await response.Content.ReadAsAsync<StatusCodeModel>();

                    throw new ApplicationException(statusCode.Message);
                }
            }

        }

        public virtual ICollection<T> GetAll(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<ICollection<T>>().Result;
                }
                else
                {
                    StatusCodeModel statusCode = response.Content.ReadAsAsync<StatusCodeModel>().Result;

                    throw new ApplicationException(statusCode.Message);
                }
            }
        }

        public virtual async Task<ICollection<T>> GetAllAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<ICollection<T>>();
                }
                else
                {
                    StatusCodeModel statusCode = await response.Content.ReadAsAsync<StatusCodeModel>();

                    throw new ApplicationException(statusCode.Message);
                }
            }
        }

        public virtual TTable GetTable(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<TTable>().Result;
                }
                else
                {
                    StatusCodeModel statusCode = response.Content.ReadAsAsync<StatusCodeModel>().Result;

                    throw new ApplicationException(statusCode.Message);
                }
            }
        }

        public virtual async Task<TTable> GetTableAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<TTable>();
                }
                else
                {
                    StatusCodeModel statusCode = await response.Content.ReadAsAsync<StatusCodeModel>();

                    throw new ApplicationException(statusCode.Message);
                }
            }
        }

    }
}