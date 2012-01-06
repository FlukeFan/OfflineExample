using System;
using System.Web.Mvc;
using OfflineExample.Views.Home;

namespace OfflineExample.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            var model = new Index() { MachineName = Environment.MachineName };
            return View(model);
        }

    }
}
