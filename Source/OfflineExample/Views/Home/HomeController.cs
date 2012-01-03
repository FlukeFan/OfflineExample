using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OfflineExample.Views.Home
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
