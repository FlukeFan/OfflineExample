using System;
using System.IO;
using NUnit.Framework;
using OfflineExample.Controllers;
using OfflineExample.Util;
using OfflineExample.Views.Offline;

namespace OfflineExample.Test.Controllers
{
    [TestFixture]
    public class OfflineControllerTest : ViewTestBase
    {
        [Test]
        public void Index()
        {
            var controller = new OfflineController();

            var result = controller.Index();

            Assert.That(result.View, Is.Null);
        }

        [Test]
        public void Month()
        {
            var controller = new OfflineController();

            var result = controller.Month();

            Assert.That(result.View, Is.Null);
        }

        [Test]
        public void Edit()
        {
            var controller = new OfflineController();

            var result = controller.Edit();

            Assert.That(result.View, Is.Null);
        }

        [Test]
        public void Manifest()
        {
            var contentTypeSet = false;
            ResponseUtil.SetContentTypeCacheManifest = () => contentTypeSet = true;
            RequestUtil.GetRootUrl = () => "test/testing";

            var controller = new OfflineController();

            var result = controller.Manifest();
            var outModel = (Manifest)result.Model;

            Assert.That(contentTypeSet, Is.True);
            Assert.That(outModel.LastModified, Is.GreaterThanOrEqualTo(AssemblyUtil.LastModifiedTicks));
            Assert.That(outModel.RootUrl, Is.EqualTo("test/testing"));
        }

        [Test]
        public void Manifest_UpdatesWhenFileChanged()
        {
            ResponseUtil.SetContentTypeCacheManifest = () => {};
            RequestUtil.GetRootUrl = () => "test/testing";

            var controller = new OfflineController();

            var result = controller.Manifest();
            var firstModified = ((Manifest)result.Model).LastModified;

            var onlineFile = HostUtil.MapPath("~/favicon.ico");
            File.SetLastWriteTimeUtc(onlineFile, DateTime.UtcNow);

            result = controller.Manifest();
            var newlyModified = ((Manifest)result.Model).LastModified;

            Assert.That(newlyModified, Is.GreaterThanOrEqualTo(firstModified));
        }
    }
}
