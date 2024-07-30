using FalzoniNetFCSharp.Domain.DTO.Stock;
using FalzoniNetFCSharp.Presentation.Api.Models.Base;
using FalzoniNetFCSharp.Presentation.Api.Models.Register;
using System;

namespace FalzoniNetFCSharp.Presentation.Api.Models.Stock
{
    public class ProductModel : BaseModel
    {
        public ProductModel(ProductDTO productDTO)
        {
            this.Id = productDTO.Id;
            this.ProductCategoryId = productDTO.ProductCategoryId;
            this.Name = productDTO.Name;
            this.Code = productDTO.Code;
            this.Description = productDTO.Description;
            this.Price = productDTO.Price;
            this.Created = productDTO.Created;
            this.Modified = productDTO.Modified;
        }

        public Guid ProductCategoryId { get; set; }

        public string Name { get; set; }

        public float Code { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }


        public virtual ProductCategoryModel Category { get; set; }


        #region Methods
        public ProductDTO ConvertToDTO()
        {
            return new ProductDTO
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