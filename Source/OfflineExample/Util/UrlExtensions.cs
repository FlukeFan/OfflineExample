using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;

namespace OfflineExample.Util
{
    public static class UrlExtensions
    {
        public static string FileContent(this UrlHelper urlHelper, string file)
        {
            return FileContentFunc(urlHelper, file);
        }

        public static Func<UrlHelper, string, string> FileContentFunc = (urlHelper, file) =>
            {
                return urlHelper.Content(file);
            };

        public static string Action<TController>(this UrlHelper urlHelper, Expression<Func<TController, ActionResult>> action)
        {
            var methodCall = (MethodCallExpression)action.Body;
            var actionName = methodCall.Method.Name;
            var controllerName = methodCall.Method.ReflectedType.Name;

            controllerName = controllerName.Substring(0, controllerName.Length - "Controller".Length); // strip off 'Controller'

            return ActionFunc(urlHelper, actionName, controllerName);
        }

        public static Func<UrlHelper, string, string, string> ActionFunc = (urlHelper, actionName, controllerName) =>
            {
                return urlHelper.Action(actionName, controllerName);
            };
    }
}