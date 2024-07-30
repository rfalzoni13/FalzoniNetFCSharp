using System.Configuration;
using System;

namespace FalzoniNetFCSharp.Utils.Helpers
{
    public static class ConfigurationHelper
    {
        public static string ProviderName { get; set; }

        public static bool IsBundleled
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["IsBundleled"]);
            }
        }
    }
}
