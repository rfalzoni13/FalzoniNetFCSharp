using FalzoniNetFCSharp.Domain.DTO.Base;
using FalzoniNetFCSharp.Domain.DTO.Stock;
using FalzoniNetFCSharp.Domain.Entities.Register;
using FalzoniNetFCSharp.Domain.Entities.Stock;
using System;

namespace FalzoniNetFCSharp.Domain.DTO.Register
{
    public class ProductCategoryDTO : BaseDTO
    {
        public ProductCategoryDTO() 
        {
        }

        public ProductCategoryDTO(ProductCategory category) 
        {
            this.Id = category.Id;
            this.Name = category.Name;
            this.Created = category.Created;
            this.Modified = category.Modified;
            this.Product = category.Product == null ? null :
            new ProductDTO(category.Product);
        }

        public string Name { get; set; }


        public virtual ProductDTO Product { get; set; }



        #region Methods
        public void ConfigureNewEntity()
        {
            Id = Guid.NewGuid();
        }

        public ProductCategory ConvertToEntity()
        {
            return new ProductCategory
            {
                Id = this.Id,
                Name = this.Name,
                Created = this.Created,
                Modified = this.Modified,
                Product = this.Product == null ? null :
                new Product
                {
                    Id = this.Product.Id,
                    ProductCategoryId = this.Product.ProductCategoryId,
                    Name = this.Product.Name,
                    Code = this.Product.Code,
                    Description = this.Product.Description,
                    Price = this.Product.Price,
                    Created = this.Product.Created,
                    Modified = this.Product.Modified
                }
            };
        }
        #endregion
    }
}
