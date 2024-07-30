using FalzoniNetFCSharp.Presentation.Administrator.Models.Base;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Stock;

namespace FalzoniNetFCSharp.Presentation.Administrator.Models.Register
{
    public class ProductCategoryModel : BaseModel
    {
        public string Name { get; set; }

        public virtual ProductModel Product { get; set; }
    }
}