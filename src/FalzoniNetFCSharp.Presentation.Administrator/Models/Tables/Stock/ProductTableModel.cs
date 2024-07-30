using FalzoniNetFCSharp.Presentation.Administrator.Models.Base;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Tables.Base;
using System.Collections.Generic;

namespace FalzoniNetFCSharp.Presentation.Administrator.Models.Tables.Stock
{
    public class ProductTableModel : TableBase
    {
        public ProductTableModel()
        {
            data = new List<ProductListTableModel>();
        }

        public virtual List<ProductListTableModel> data { get; set; }
    }

    public class ProductListTableModel : BaseModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public float Code { get; set; }

    }
}