﻿using FalzoniNetFCSharp.Presentation.Administrator.Clients.Base;
using FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Register;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Register;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Tables.Register;
using FalzoniNetFCSharp.Utils.Helpers;
using System.Collections.Generic;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Linq;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Common;

namespace FalzoniNetFCSharp.Presentation.Administrator.Clients.Register
{
    public class CustomerClient : BaseClient<CustomerModel>, ICustomerClient
    {
        public CustomerClient() :base() { }

        public async Task<CustomerTableModel> GetTableAsync(string url)
        {
            var table = new CustomerTableModel();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var customers = await response.Content.ReadAsAsync<ICollection<CustomerModel>>();

                        foreach (var customer in customers)
                        {
                            table.data.Add(new CustomerListTableModel()
                            {
                                Id = customer.Id,
                                Name = customer.Name,
                                Document = customer.Document,
                                Email = customer.Email,
                                Gender = customer.Gender,
                                Created = customer.Created,
                                Modified = customer.Modified
                            });
                        }

                        table.recordsFiltered = table.data.Count();
                        table.recordsTotal = table.data.Count();
                    }
                    else
                    {
                        if(response.StatusCode != System.Net.HttpStatusCode.NotFound)
                        {
                            StatusCodeModel statusCode = response.Content.ReadAsAsync<StatusCodeModel>().Result;

                            table.error = statusCode.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                table.error = ExceptionHelper.CatchMessageFromException(ex);
            }

            return await Task.FromResult(table);
        }
    }
}