using FalzoniNetFCSharp.Presentation.Administrator.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace FalzoniNetFCSharp.Presentation.Administrator.Models.Stock
{
    public class ProductModel : BaseModel
    {
        [Required(ErrorMessage = "A categoria do produto é obrigatória")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório")]
        public string Name { get; set; }

        public float Code { get; set; }

        [Required(ErrorMessage = "A descrição do produto é obrigatório")]
        public string Description { get; set; }

        [Required(ErrorMessage = "O preço do produto é obrigatório")]
        [Range(1, double.MaxValue, ErrorMessage = "O valor mínimo do preço deve ser acima de zero")]
        public decimal Price { get; set; }

        public virtual ProductCategoryModel Category { get; set; }
    }
}