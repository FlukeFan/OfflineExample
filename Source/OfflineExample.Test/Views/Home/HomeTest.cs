using NUnit.Framework;
using OfflineExample.Views.Home;
using System;

namespace OfflineExample.Test.Views.Home
{
    [TestFixture]
    public class HomeTest : ViewTestBase
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

        [Test]
        public void IndexRender()
        {
            var result = Render<HomeController, Index>(c => c.Index(), new Index() { MachineName = "abcd/" });

            Assert.That(result, Is.StringContaining("http://abcd/Call(OfflineController.Index)"));
        }
    }
}
