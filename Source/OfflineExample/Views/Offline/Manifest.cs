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
        public static IList<CacheScript> CachedScripts;

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

        public class CacheScript : CacheFile
        {
            public CacheScript() { CachedScripts.Add(this); }
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
            CachedScripts = new List<CacheScript>();

            // styles
            Cache.Add(new CacheFile() { File = "~/favicon.ico" });
            Cache.Add(new CacheFile() { File = "~/Content/jquery.mobile-1.0.1.min.css" });
            Cache.Add(new CacheFile() { File = "~/Content/jquery.mobile.structure-1.0.1.min.css" });
            Cache.Add(new CacheFile() { File = "~/Content/images/ajax-loader.png" });
            Cache.Add(new CacheFile() { File = "~/Content/images/icons-18-black.png" });
            Cache.Add(new CacheFile() { File = "~/Content/images/icons-18-white.png" });
            Cache.Add(new CacheFile() { File = "~/Content/images/icons-36-black.png" });
            Cache.Add(new CacheFile() { File = "~/Content/images/icons-36-white.png" });

            // scripts
            Cache.Add(new CacheScript() { File = "~/Scripts/jquery-1.7.1.min.js" });
            Cache.Add(new CacheScript() { File = "~/Scripts/jquery.mobile.global.js" });
            Cache.Add(new CacheScript() { File = "~/Scripts/jquery.mobile-1.0.1.min.js" });
            Cache.Add(new CacheScript() { File = "~/Scripts/Util/DateUtil.js" });
            Cache.Add(new CacheScript() { File = "~/Scripts/Util/JsonUtil.js" });
            Cache.Add(new CacheScript() { File = "~/Scripts/Offline/Dto/Appointment.js" });
            Cache.Add(new CacheScript() { File = "~/Scripts/Offline/Dto/AppointmentMonth.js" });
            Cache.Add(new CacheScript() { File = "~/Scripts/Offline/Dto/CmdFetchFuture.js" });
            Cache.Add(new CacheScript() { File = "~/Scripts/Offline/OfflineGlobal.js" });
            Cache.Add(new CacheScript() { File = "~/Scripts/Offline/StorageService.js" });
            Cache.Add(new CacheScript() { File = "~/Scripts/Offline/Index.js" });
            Cache.Add(new CacheScript() { File = "~/Scripts/Offline/Months.js" });

            // pages
            Cache.Add(new CacheView<OfflineController>(c => c.Index()));
            Cache.Add(new CacheView<OfflineController>(c => c.Month()));
        }

        public long LastModified;
        public string RootUrl;
    }
}