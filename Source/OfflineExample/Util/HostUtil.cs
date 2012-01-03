using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Hosting;

namespace OfflineExample.Util
{
    public class HostUtil : Controller
    {
        public static Func<string, string> MapPath = (string virtualPath) =>
            {
                return HostingEnvironment.MapPath(virtualPath);
            };
    }
}
