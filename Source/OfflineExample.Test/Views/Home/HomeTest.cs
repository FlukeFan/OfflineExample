using NUnit.Framework;
using OfflineExample.Controllers;
using OfflineExample.Views.Home;

namespace OfflineExample.Test.Views.Home
{
    [TestFixture]
    public class HomeTest : ViewTestBase
    {
        [Test]
        public void IndexRender()
        {
            var result = Render<HomeController, Index>(c => c.Index(), new Index() { MachineName = "abcd/" });

            Assert.That(result, Is.StringContaining("http://abcd/Call(OfflineController.Index)"));
        }
    }
}
