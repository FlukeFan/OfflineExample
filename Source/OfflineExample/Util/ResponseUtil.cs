using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfflineExample.Util
{
    public static class ResponseUtil
    {
        public static Action SetContentTypeCacheManifest = () =>
            {
                HttpContext.Current.Response.Cache.SetExpires(DateTime.Now - TimeSpan.FromDays(1));
                HttpContext.Current.Response.ContentType = "text/cache-manifest";
            };
    }
}