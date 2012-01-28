using System.IO;
using System.Linq;
using System.Web.Mvc;
using OfflineExample.Util;
using OfflineExample.Views.Offline;

namespace OfflineExample.Controllers
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

        public ViewResult Edit()
        {
            return View();
        }

        public PartialViewResult Manifest()
        {
            ResponseUtil.SetContentTypeCacheManifest();

            var files = OfflineExample.Views.Offline.Manifest.Cache.Select(f => f.Filename());

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