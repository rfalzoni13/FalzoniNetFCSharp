using System.Collections.Generic;
using System.Web.Mvc;

namespace FalzoniNetFCSharp.Presentation.Administrator.Models.Identity
{
    public class SendCodeModel
    {
        public List<SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
        public string SelectedProvider { get; set; }
    }
}