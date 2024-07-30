using FalzoniNetFCSharp.Presentation.Administrator.Attributes;
using System.Web.Mvc;

namespace FalzoniNetFCSharp.Presentation.Administrator
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ProfileActionAttribute());
            //filters.Add(new UserRegisterActionAttribute());
        }
    }
}