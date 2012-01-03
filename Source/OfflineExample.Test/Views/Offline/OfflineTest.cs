using NUnit.Framework;
using OfflineExample.Util;
using OfflineExample.Views.Offline;
using System.IO;
using System;

namespace OfflineExample.Test.Views.Offline
{
    [TestFixture]
    public class IndexTest : ViewTestBase
    {
        [Test]
        public void Index()
        {
            var controller = new OfflineController();

            var result = controller.Index();

            Assert.That(result.View, Is.Null);
        }

        [Test]
        public void IndexRender()
        {
            var result = Render<OfflineController, object>(c => c.Index(), null);

            Assert.That(result, Is.StringContaining("div id=\"pageAll\""));
        }

        [Test]
        public void Month()
        {
            var controller = new OfflineController();

            var result = controller.Month();

            Assert.That(result.View, Is.Null);
        }

        [Test]
        public void MonthRender()
        {
            var result = Render<OfflineController, object>(c => c.Month(), null);

            Assert.That(result, Is.StringContaining("div id=\"pageMonth\""));
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

        [Test]
        public void ManifestRender()
        {
            var result = Render<OfflineController, Manifest>(c => c.Manifest(), new Manifest() { LastModified = 12345L });

            Assert.That(result, Is.StringContaining("# version 12345"));
        }
    }
}
