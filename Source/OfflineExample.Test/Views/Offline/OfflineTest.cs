using System;
using System.IO;
using NUnit.Framework;
using OfflineExample.Controllers;
using OfflineExample.Util;
using OfflineExample.Views.Offline;

namespace OfflineExample.Test.Views.Offline
{
    [TestFixture]
    public class IndexTest : ViewTestBase
    {
        [Test]
        public void IndexRender()
        {
            var result = Render<OfflineController, object>(c => c.Index(), null);

            Assert.That(result, Is.StringContaining("div id=\"pageAll\""));
        }

        [Test]
        public void MonthRender()
        {
            var result = Render<OfflineController, object>(c => c.Month(), null);

            Assert.That(result, Is.StringContaining("div id=\"pageMonth\""));
        }

        [Test]
        public void EditRender()
        {
            var result = Render<OfflineController, object>(c => c.Edit(), null);

            Assert.That(result, Is.StringContaining("div id=\"pageEdit\""));
        }

        [Test]
        public void ManifestRender()
        {
            var result = Render<OfflineController, Manifest>(c => c.Manifest(), new Manifest() { LastModified = 12345L });

            Assert.That(result, Is.StringContaining("# version 12345"));
        }
    }
}
