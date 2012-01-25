using System.Collections.Specialized;
using System.Web.Mvc;
using Domain.Core;
using NUnit.Framework;
using OfflineExample.Controllers;
using System;
using Domain.Util;
using Domain.Test.Core;

namespace OfflineExample.Test.Controllers
{
    [TestFixture]
    public class OnlineControllerTest : ViewTestBase
    {
        [Test]
        public void Execute()
        {
            var controller = new OnlineController();

            var cmd =
                new Appointment.Create()
                {
                    ClientId = 123,
                    VisitDate = new DateTime(2009, 08, 07),
                    Notes = "test notes",
                };

            var createdAppointment = new AppointmentBuilder().Build();

            RootStub.StubExecute(cmd, createdAppointment);

            var result = (Appointment)controller.Execute(cmd).Data;

            Assert.That(result, Is.SameAs(createdAppointment));
        }

        public class TempModelType
        {
            public string TestValue { get; set; }
        }

        [Test]
        public void ModelBinder()
        {
            var postData =
                new NameValueCollection()
                {
                    { "Class", "TempModelType" },
                    { "TestValue", "1234" },
                    { "Ignored", "IgnoredValue" },
                };

            var controllerContext = new ControllerContext();
            var valueProvider = new NameValueCollectionValueProvider(postData, null);
            var modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(object));

            var bindingContext =
                new ModelBindingContext()
                {
                    ValueProvider = valueProvider,
                    ModelMetadata = modelMetadata,
                };

            var result = (TempModelType)new OnlineController.Binder().BindModel(controllerContext, bindingContext);

            Assert.That(result, Is.Not.Null, "null model returned");
            Assert.That(result.TestValue, Is.EqualTo("1234"));
        }

        public class ModelWithDateTime
        {
            public DateTime DateTimeProp { get { return new DateTime(2009, 08, 07, 06, 05, 04); } }
        }

        [Test]
        public void CustomJsonResult()
        {
            var data = new ModelWithDateTime();
            var result = OnlineController.CustomJsonResult.Serialize(data);

            Assert.That(result, Is.EqualTo("{\"DateTimeProp\":\"2009-08-07T06:05\"}"));
        }
    }
}
