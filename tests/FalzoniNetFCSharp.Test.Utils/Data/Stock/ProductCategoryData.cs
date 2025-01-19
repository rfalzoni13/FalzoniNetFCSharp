using FalzoniNetFCSharp.Domain.Entities.Stock;
using FalzoniNetFCSharp.Test.Utils.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FalzoniNetFCSharp.Test.Utils.Data.Stock
{
    public class ProductCategoryData : BaseData<ProductCategory>
    {
        public override IQueryable<ProductCategory> GetData()
        {
            return GetList().AsQueryable();
        }

        public override ProductCategory GetItem(Guid id)
        {
            return GetList().FirstOrDefault(x => x.Id == id);
        }

        public override Guid GetGuidFromList()
        {
            List<ProductCategory> list = GetList();
            Random rnd = new Random();
            int i = rnd.Next(list.Count());
            return list[i].Id;
        }

        public override ProductCategory CreateObject()
        {
            return new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Cama, Mesa e Banho",
                Created = DateTime.Now
            };
        }

        public override ProductCategory UpdateObject(ProductCategory obj)
        {
            obj.Name = "Vídeo Games";
            obj.Modified = DateTime.Now;
            return obj;
        }

        public override Guid GetGuidWithoutInclude()
        {
            return GetGuidFromList();
        }

        #region Self Methods
        private List<ProductCategory> GetList()
        {
            return new List<ProductCategory>
            {
                new ProductCategory
                {
                    Id = Guid.Parse("fb5099e8-d513-4167-ad61-de973e307b26"),
                    Name = "Games",
                    Created = DateTime.Now
                },
                new ProductCategory
                {
                    Id = Guid.Parse("cc939b49-3d7d-48d9-aada-ea8d6538a295"),
                    Name = "Informática",
                    Created = new DateTime(2024, 10, 25, 18, 32, 44)
                },
                new ProductCategory
                {
                    Id = Guid.Parse("79826431-c6a7-4584-9d36-b517d7799628"),
                    Name = "Brinquedos",
                    Created = new DateTime(2020, 8, 19, 15, 5, 57),
                    Modified = new DateTime(2024, 2, 21, 10, 10, 23)
                }
            };
        }
        #endregion
    }
}
