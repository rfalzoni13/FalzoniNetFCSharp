using FalzoniNetFCSharp.Presentation.Administrator.Clients.Base;
using FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Configuration;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Configuration;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Tables.Configuration;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Common;
using FalzoniNetFCSharp.Utils.Helpers;
using System.Collections.Generic;
using System.Linq;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Identity;

namespace FalzoniNetFCSharp.Presentation.Administrator.Clients.Configuration
{
    public class UserClient : BaseClient<UserModel, UserTableModel>, IUserClient
    {
        public UserClient() :base() { }

        public override async Task<UserModel> GetAsync(string url, string id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.GetAsync($"{url}?id={id}");
                if (response.IsSuccessStatusCode)
                {
                    var model = await response.Content.ReadAsAsync<UserModel>();
                    
                    //Carregar foto do perfil
                    model.LoadProfilePhoto();

                    return model;
                }
                else
                {
                    StatusCodeModel statusCode = await response.Content.ReadAsAsync<StatusCodeModel>();

                    throw new ApplicationException(statusCode.Message);
                }
            }
        }

        public override async Task<UserTableModel> GetTableAsync(string url)
        {
            var table = new UserTableModel();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var users = await response.Content.ReadAsAsync<ICollection<UserModel>>();

                        foreach (var user in users)
                        {
                            table.data.Add(new UserListTableModel()
                            {
                                Id = user.Id,
                                Name = user.Name,
                                Email = user.Email,
                                UserName = user.UserName,
                                Gender = user.Gender,
                                Created = user.Created,
                                Modified = user.Modified
                            });
                        }

                        table.recordsFiltered = table.data.Count();
                        table.recordsTotal = table.data.Count();
                    }
                    else
                    {
                        StatusCodeModel statusCode = response.Content.ReadAsAsync<StatusCodeModel>().Result;

                        table.error = statusCode.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                table.error = ExceptionHelper.CatchMessageFromException(ex);
            }

            return await Task.FromResult(table);
        }

        public override string Add(string url, UserModel obj)
        {
            var model = new ApplicationUserModel(obj);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.PostAsJsonAsync(url, model).Result;
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

        public override string Update(string url, UserModel obj)
        {
            var model = new ApplicationUserModel(obj);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.PutAsJsonAsync(url, model).Result;
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
    }
}