using FalzoniNetFCSharp.Presentation.Administrator.Clients.Base;
using FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Configuration;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Common;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Configuration;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Identity;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Tables.Configuration;
using FalzoniNetFCSharp.Presentation.Administrator.Utils;
using FalzoniNetFCSharp.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FalzoniNetFCSharp.Presentation.Administrator.Clients.Configuration
{
    public class UserClient : BaseClient<UserModel>, IUserClient
    {
        public UserClient() :base() 
        {
            url += "/User";
        }

        public override async Task<UserModel> GetAsync(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.GetAsync($"{url}/Get/{id}");
                UserModel model = await ResponseUtils<UserModel>.ReturnObjectAsync(response);
                model.LoadProfilePhoto();
                return model;
            }
        }

        public async Task<UserTableModel> GetTableAsync()
        {
            var table = new UserTableModel();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage response = await client.GetAsync($"{url}/GetAll");
                    ICollection<UserModel> users = await ResponseUtils<ICollection<UserModel>>.ReturnObjectAsync(response);

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
            }
            catch (Exception ex)
            {
                table.error = ExceptionHelper.CatchMessageFromException(ex);
            }

            return await Task.FromResult(table);
        }

        public override string Add(UserModel obj)
        {
            var model = new ApplicationUserModel(obj);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.PostAsJsonAsync($"{url}/Add", model).Result;
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

        public override string Update(UserModel obj)
        {
            var model = new ApplicationUserModel(obj);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.PutAsJsonAsync($"{url}/Update", model).Result;
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