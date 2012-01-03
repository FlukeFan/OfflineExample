using System;
using System.Linq;
using System.Web.Mvc;
using Domain.Util;

namespace OfflineExample.Controllers
{
    public class OnlineController : Controller
    {
        public JsonResult Execute([ModelBinder(typeof(OnlineController.Binder))] ICommand command)
        {
            var result = command.Execute();
            return Json(result, JsonRequestBehavior.AllowGet);
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