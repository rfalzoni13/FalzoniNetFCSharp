using FalzoniNetFCSharp.Presentation.Administrator.Models.Base;

namespace FalzoniNetFCSharp.Presentation.Administrator.Models.Stock
{
    public class ProductCategoryModel : BaseModel
    {
        public string Name { get; set; }

        public virtual ProductModel Product { get; set; }
    }
}