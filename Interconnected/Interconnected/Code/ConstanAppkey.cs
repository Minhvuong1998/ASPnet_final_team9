using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Interconnected.Code
{
    public class ConstanAppkey
    {
        public static int PAGESIZE()
        {
            return int.Parse(ConfigurationManager.AppSettings["PageSize"]);
        }
        public static int ID_ROLE_SUPER()
        {
            return int.Parse(ConfigurationManager.AppSettings["Idrolesuper"]);
        }
        public static string ADMIN()
        {
            return ConfigurationManager.AppSettings["Admin"];
        }
        public static string MOD()
        {
            return ConfigurationManager.AppSettings["Mod"];
        }
        public static string USER()
        {
            return ConfigurationManager.AppSettings["User"];
        }
    }
}