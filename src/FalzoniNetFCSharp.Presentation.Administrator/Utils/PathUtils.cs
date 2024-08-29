using System.Configuration;
using System.Diagnostics;

namespace FalzoniNetFCSharp.Presentation.Administrator.Utils
{
    public static class PathUtils
    {
        private static string _pathUrlApi;

        public static void LoadPath()
        {
            _pathUrlApi = Debugger.IsAttached ?
                ConfigurationManager.AppSettings["PathLocal"] :
                ConfigurationManager.AppSettings["PathProduction"];
        }

        public static string GetApiPath()
        {
            return _pathUrlApi;
        }
    }
}