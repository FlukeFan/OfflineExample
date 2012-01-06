using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;

namespace OfflineExample.Util
{
    public class RequestUtil
    {
        private static readonly string BaseNamespace = typeof(OfflineExample.Global).Namespace;

        public static Func<string> GetRootUrl = () =>
            {
                return HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath;
            };

        public static string GetViewFile(LambdaExpression viewAction)
        {
            var actionMethod = (MethodCallExpression)viewAction.Body;
            var viewName = actionMethod.Method.Name;
            var controller = actionMethod.Object.Type;

            var filename = controller.Name; // e.g., HomeController

            if (filename.EndsWith("Controller"))
                filename = filename.Substring(0, filename.Length - "Controller".Length); // e.g., Home

            filename = ".Views." + filename + "." + actionMethod.Method.Name; // e.g., Views.Home.Index
            filename = HostUtil.MapPath("~" + filename.Replace(".", @"\")) + ".cshtml";
            return filename;
        }
    }
}
