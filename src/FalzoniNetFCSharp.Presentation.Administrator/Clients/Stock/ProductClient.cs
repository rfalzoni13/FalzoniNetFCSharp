using FalzoniNetFCSharp.Presentation.Administrator.Clients.Base;
using FalzoniNetFCSharp.Presentation.Administrator.Clients.Interfaces.Stock;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Stock;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Tables.Stock;
using FalzoniNetFCSharp.Utils.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Common;

namespace FalzoniNetFCSharp.Presentation.Administrator.Clients.Stock
{
    public class ProductClient : BaseClient<ProductModel, ProductTableModel>, IProductClient
    {
        public override async Task<ProductTableModel> GetTableAsync(string url)
        {
            var table = new ProductTableModel();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var products = await response.Content.ReadAsAsync<ICollection<ProductModel>>();

                        foreach (var product in products)
                        {
                            table.data.Add(new ProductListTableModel()
                            {
                                Id = product.Id,
                                Name = product.Name,
                                Category = product.Category.Name,
                                Price = product.Price,
                                Code = product.Code,
                                Created = product.Created,
                                Modified = product.Modified
                            });
                        }

                        table.recordsFiltered = table.data.Count();
                        table.recordsTotal = table.data.Count();
                    }
                    else
                    {
                        if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
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