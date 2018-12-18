using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Interconnected.Code
{
    public class MyEndCode
    {
        public static string mahoa(string pass)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pass.Trim(), "SHA1");
        }
    }
}