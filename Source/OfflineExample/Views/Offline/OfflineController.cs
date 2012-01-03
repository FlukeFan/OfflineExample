using System.Collections.Generic;
using System.Web.Mvc;
using OfflineExample.Util;
using System.Linq;
using System.IO;

namespace OfflineExample.Views.Offline
{
    public class OfflineController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Month()
        {
            return View();
        }

        public PartialViewResult Manifest()
        {
            ResponseUtil.SetContentTypeCacheManifest();

            var files = Offline.Manifest.Cache.Select(f => f.Filename());

            var newest = AssemblyUtil.LastModifiedTicks;
            foreach (var file in files)
            {
                var fileTicks = new FileInfo(file).LastWriteTimeUtc.Ticks;

                if (fileTicks > newest)
                    newest = fileTicks;
            }

            var model =
                new Manifest()
                {
                    LastModified = newest,
                    RootUrl = RequestUtil.GetRootUrl(),
                };

            return PartialView(model);
        }
    }
}