using FalzoniNetFCSharp.Domain.Entities.Stock;
using FalzoniNetFCSharp.Test.Utils.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FalzoniNetFCSharp.Test.Utils.Data.Stock
{
    public class ProductData : BaseData<Product>
    {
        public override IQueryable<Product> GetData()
        {
            return GetList().AsQueryable();
        }

        public override Product GetItem(Guid id)
        {
            return GetList().FirstOrDefault(x => x.Id == id);
        }

        public override Product CreateObject()
        {
            return new Product
            {
                Id = Guid.NewGuid(),
                CategoryId = Guid.Parse("cc939b49-3d7d-48d9-aada-ea8d6538a295"),
                Name = "Notebook DELL Inspiron",
                Price = 8999.99M,
                Description = "Notebook c/ processador I5 8GB de RAM 256 SSD.",
                Code = 016,
                Created = DateTime.Now
            };
        }

        public override Product UpdateObject(Product obj)
        {
            obj.Name = "Vídeo Game Nintendo Wii";
            obj.Price = 4999.99M;
            obj.Description = "Vídeo Game clássico da Nintendo";
            obj.Code = 001;
            obj.Modified = DateTime.Now;
            return obj;
        }

        public override Guid GetGuidFromList()
        {
            List<Product> list = GetList();
            list = list.Where(x => x.Category != null).ToList();
            Random rnd = new Random();
            int i = rnd.Next(list.Count());
            return list[i].Id;
        }

        public override Guid GetGuidWithoutInclude()
        {
            List<Product> list = GetList();
            list = list.Where(x => x.Category == null).ToList();
            Random rnd = new Random();
            int i = rnd.Next(list.Count());
            return list[i].Id;
        }

        #region Self Methods
        private List<Product> GetList()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = Guid.Parse("429fe9ba-9d57-4b98-a704-826d7fba8e37"),
                    CategoryId = Guid.Parse("fb5099e8-d513-4167-ad61-de973e307b26"),
                    Name = "Vídeo Game Playstation 5",
                    Price = 4999.99M,
                    Description = "Vídeo Game PS5 de última geração",
                    Code = 001,
                    Created = DateTime.Now,
                    Category = new ProductCategory
                    {
                        Id = Guid.Parse("fb5099e8-d513-4167-ad61-de973e307b26"),
                        Name = "Games",
                        Created = DateTime.Now
                    }
                },
                new Product
                {
                    Id = Guid.Parse("c57052e1-83bb-4abd-a9cb-8fa8e8d5ecc8"),
                    CategoryId = Guid.Parse("cc939b49-3d7d-48d9-aada-ea8d6538a295"),
                    Name = "Notebook Gamer AMD",
                    Price = 8999.99M,
                    Description = "Notebook com as melhores configurações do mercados para jogos e etc.",
                    Code = 010,
                    Created = new DateTime(2025, 1, 2, 10, 5, 49),
                    Modified = DateTime.Now,
                    Category = new ProductCategory
                    {
                        Id = Guid.Parse("cc939b49-3d7d-48d9-aada-ea8d6538a295"),
                        Name = "Informática",
                        Created = new DateTime(2024, 10, 25, 18, 32, 44)
                    }
                },
                new Product
                {
                    Id = Guid.Parse("e00373f0-833d-4096-8d84-8023fe58d9dd"),
                    CategoryId = Guid.Parse("79826431-c6a7-4584-9d36-b517d7799628"),
                    Name = "Carrinho de Controle Remoto Estrela",
                    Price = 499.99M,
                    Description = "Carrinho excelente para você curtir as maiores aventuras",
                    Code = 921,
                    Created = new DateTime(2022, 3, 12, 00, 45, 12),
                    Modified = new DateTime(2024, 7, 9, 13, 25, 38),
                    Category = new ProductCategory
                    {
                        Id = Guid.Parse("79826431-c6a7-4584-9d36-b517d7799628"),
                        Name = "Brinquedos",
                        Created = new DateTime(2020, 8, 19, 15, 5, 57),
                        Modified = new DateTime(2024, 2, 21, 10, 10, 23)
                    }
                },
                new Product
                {
                    Id = Guid.Parse("106dbca3-5df0-4af4-82a6-a55b088bd27c"),
                    CategoryId = Guid.Parse("fb5099e8-d513-4167-ad61-de973e307b26"),
                    Name = "Vídeo Game Xbox Series S 1TB",
                    Price = 2999.99M,
                    Description = "Vídeo Game Xbox Series S escuro com 1TB de armazenamento",
                    Code = 012,
                    Created = new DateTime(2024, 1, 10, 12, 19, 5),
                    Modified = DateTime.Now
                }
            };
        }
        #endregion
    }
}
