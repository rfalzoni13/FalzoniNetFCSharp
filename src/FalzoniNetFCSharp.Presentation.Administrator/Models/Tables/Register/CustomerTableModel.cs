using FalzoniNetFCSharp.Presentation.Administrator.Models.Base;
using FalzoniNetFCSharp.Presentation.Administrator.Models.Tables.Base;
using System.Collections.Generic;

namespace FalzoniNetFCSharp.Presentation.Administrator.Models.Tables.Register
{
    public class CustomerTableModel : TableBase
    {
        public CustomerTableModel()
        {
            data = new List<CustomerListTableModel>();
        }

        public virtual List<CustomerListTableModel> data { get; set; }
    }

    public class CustomerListTableModel : BaseModel
    {
        public string Name { get; set; }

        public string Gender { get; set; }

        public string Email { get; set; }

        public string Document { get; set; }

    }
}