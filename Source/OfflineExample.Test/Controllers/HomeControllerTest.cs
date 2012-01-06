using System;
using NUnit.Framework;
using OfflineExample.Controllers;
using OfflineExample.Views.Home;

namespace OfflineExample.Test.Controllers
{
    [TestFixture]
    public class HomeControllerTest : ViewTestBase
    {
        [Test]
        public void Index()
        {
            var controller = new HomeController();

            var result = controller.Index();
            var model = (Index)result.Model;

            Assert.That(result.View, Is.Null);
            Assert.That(model.MachineName, Is.EqualTo(Environment.MachineName));
        }
    }
}
