using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Domain.Util;
using OfflineExample.Util;

namespace OfflineExample.Controllers
{
    public class OnlineController : Controller
    {
        public CustomJsonResult Execute([ModelBinder(typeof(OnlineController.Binder))] ICommand command)
        {
            var result = command.Execute();
            return new CustomJsonResult() { Data = result };
        }

        public class CustomJsonResult : JsonResult
        {
            public class DateTimeJavaScriptConverter : JavaScriptConverter
            {
                public override IEnumerable<Type> SupportedTypes
                {
                    get { return new [] { typeof(DateTime) }; }
                }

                public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
                {
                    throw new NotImplementedException();
                }

                public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
                {
                    var date = (DateTime)obj;
                    return new JsonString(date.ToString("yyyy-MM-ddTHH:mm"));
                }
            }

            public static string Serialize(object data)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                serializer.RegisterConverters(new JavaScriptConverter[] { new DateTimeJavaScriptConverter() });
                return serializer.Serialize(data);
            }

            public override void ExecuteResult(ControllerContext context)
            {
                HttpResponseBase response = context.HttpContext.Response;
                response.ContentType = "application/json";
                response.Write(Serialize(Data));
            }
        }

        public class Binder : DefaultModelBinder
        {
            protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, System.Type modelType)
            {
                var modelTypeName = bindingContext.ValueProvider.GetValue("Class").AttemptedValue;

                modelType =
                    AppDomain.CurrentDomain
                        .GetAssemblies()
                        .SelectMany(a => a.GetTypes())
                        .Where(t => t.Name == modelTypeName)
                        .Single();

                bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, modelType);
                return base.CreateModel(controllerContext, bindingContext, modelType);
            }
        }
    }
}