using FalzoniNetFCSharp.Domain.DTO.Base;
using FalzoniNetFCSharp.Domain.DTO.Register;
using FalzoniNetFCSharp.Domain.Entities.Stock;
using System;

namespace FalzoniNetFCSharp.Domain.DTO.Stock
{
    public class ProductDTO : BaseDTO
    {
        public ProductDTO() 
        {
        }

        public ProductDTO(Product product) 
        {
            this.Id = product.Id;
            this.ProductCategoryId = product.ProductCategoryId;
            this.Name = product.Name;
            this.Code = product.Code;
            this.Description = product.Description;
            this.Price = product.Price;
            this.Created = product.Created;
            this.Modified = product.Modified;
        }

        public Guid ProductCategoryId { get; set; }

        public string Name { get; set; }

        public float Code { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }


        public virtual ProductCategoryDTO Category { get; set; }



        #region Methods
        public void ConfigureNewEntity()
        {
            Id = Guid.NewGuid();
        }

        public Product ConvertToEntity()
        {
            return new Product
            {
                Id = this.Id,
                ProductCategoryId = this.ProductCategoryId,
                Name = this.Name,
                Code = this.Code,
                Description = this.Description,
                Price = this.Price,
                Created = this.Created,
                Modified = this.Modified,
            };
        }
        #endregion

    }
}
