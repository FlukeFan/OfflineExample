using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using OfflineExample.Controllers;
using OfflineExample.Util;

namespace OfflineExample.Views.Offline
{
    public class Manifest
    {
        public static IList<ICacheItem> Cache;

        public interface ICacheItem
        {
            string ToUrl(UrlHelper urlHelper);
            string Filename();
        }

        public class CacheFile : ICacheItem
        {
            public string File;
            public string ToUrl(UrlHelper urlHelper) { return urlHelper.FileContent(File) + "\n"; }
            public string Filename() { return HostUtil.MapPath(File); }
        }

        public class CacheView<TController> : ICacheItem
        {
            public CacheView(Expression<Func<TController, ActionResult>> action) { ViewAction = action; }
            public Expression<Func<TController, ActionResult>> ViewAction;
            public string ToUrl(UrlHelper urlHelper) { return urlHelper.Action(ViewAction) + "\n"; }
            public string Filename() { return RequestUtil.GetViewFile(ViewAction); }
        }

        static Manifest()
        {
            Cache = new List<ICacheItem>();

            // styles
            Cache.Add(new CacheFile() { File = "~/favicon.ico" });
            Cache.Add(new CacheFile() { File = "~/Content/jquery.mobile-1.0.min.css" });
            Cache.Add(new CacheFile() { File = "~/Content/jquery.mobile.structure-1.0.min.css" });
            Cache.Add(new CacheFile() { File = "~/Content/images/ajax-loader.png" });
            Cache.Add(new CacheFile() { File = "~/Content/images/icons-18-black.png" });
            Cache.Add(new CacheFile() { File = "~/Content/images/icons-18-white.png" });
            Cache.Add(new CacheFile() { File = "~/Content/images/icons-36-black.png" });
            Cache.Add(new CacheFile() { File = "~/Content/images/icons-36-white.png" });

            // scripts
            Cache.Add(new CacheFile() { File = "~/Scripts/jquery-1.7.1.min.js" });
            Cache.Add(new CacheFile() { File = "~/Scripts/jquery.mobile.global.js" });
            Cache.Add(new CacheFile() { File = "~/Scripts/jquery.mobile-1.0.min.js" });
            Cache.Add(new CacheFile() { File = "~/Scripts/Offline/OfflineGlobal.js" });
            Cache.Add(new CacheFile() { File = "~/Scripts/Offline/Dto/CmdFetchFuture.js" });
            Cache.Add(new CacheFile() { File = "~/Scripts/Offline/StorageService.js" });
            Cache.Add(new CacheFile() { File = "~/Scripts/Offline/Index.js" });
            Cache.Add(new CacheFile() { File = "~/Scripts/Offline/Months.js" });

            // pages
            Cache.Add(new CacheView<OfflineController>(c => c.Index()));
            Cache.Add(new CacheView<OfflineController>(c => c.Month()));
        }

        public long LastModified;
        public string RootUrl;
    }
}