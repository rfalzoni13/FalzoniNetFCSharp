using FalzoniNetFCSharp.Presentation.Administrator.Clients.Base;
using FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Configuration;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Common;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Configuration;
using FalzoniNetFCSharp.Presentation.Administrator.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace FalzoniNetFCSharp.Presentation.Administrator.Clients.Configuration
{
    public class RoleClient : BaseClient<RoleModel>, IRoleClient
    {
        public RoleClient()
            :base() 
        {
            url += "Role";
        }

        public List<string> GetAllNames()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.GetAsync($"{url}/GetAllNames").Result;
                if (response.IsSuccessStatusCode)
                {
                    var roles = response.Content.ReadAsAsync<List<string>>().Result;

                    return roles;
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