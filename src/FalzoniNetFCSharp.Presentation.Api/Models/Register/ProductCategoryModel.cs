using FalzoniNetFCSharp.Domain.DTO.Register;
using FalzoniNetFCSharp.Domain.DTO.Stock;
using FalzoniNetFCSharp.Presentation.Api.Models.Base;
using FalzoniNetFCSharp.Presentation.Api.Models.Stock;

namespace FalzoniNetFCSharp.Presentation.Api.Models.Register
{
    public class ProductCategoryModel : BaseModel
    {
        public ProductCategoryModel(ProductCategoryDTO categoryDTO)
        {
            this.Id = categoryDTO.Id;
            this.Name = categoryDTO.Name;
            this.Created = categoryDTO.Created;
            this.Modified = categoryDTO.Modified;
            this.Product = categoryDTO.Product == null ? null :
            new ProductModel(categoryDTO.Product);
        }

        public string Name { get; set; }


        public virtual ProductModel Product { get; set; }



        #region Methods
        public ProductCategoryDTO ConvertToDTO()
        {
            return new ProductCategoryDTO
            {
                Id = this.Id,
                Name = this.Name,
                Created = this.Created,
                Modified = this.Modified,
                Product = this.Product == null ? null :
                new ProductDTO
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