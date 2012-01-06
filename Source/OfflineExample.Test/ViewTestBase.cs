using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Domain.Util;
using NUnit.Framework;
using OfflineExample.Util;
using RazorEngine;
using RazorEngine.Templating;

namespace OfflineExample.Test
{
    public class ViewTestBase
    {
        private const string _rootFolder = @"..\..\..\OfflineExample";
        private RootStub _rootStub;

        protected RootStub RootStub { get { return _rootStub; } }

        [SetUp]
        public void SetUpTestBase()
        {
            _rootStub = new RootStub();
            Registry.Root = _rootStub;

            StubHostFunctions();
        }

        protected string Render<TController, TModel>(Expression<Func<TController,ActionResult>> action, TModel model)
        {
            var filename = RequestUtil.GetViewFile(action);
            Assert.That(File.Exists(filename), string.Format("View '{0}' does not exist", filename));

            var templateLines = File.ReadAllLines(filename);

            int startLine = 0;
            if (templateLines[0].StartsWith("@model"))
                startLine++;  // skip the @model

            var template = string.Join("\n", templateLines.Skip(startLine));

            string output = "";
            try
            {
                Razor.DefaultTemplateService.Namespaces.Add("OfflineExample.Controllers");
                Razor.DefaultTemplateService.Namespaces.Add("OfflineExample.Util");
                Razor.DefaultTemplateService.Namespaces.Add("OfflineExample.Views.Offline");
                Razor.SetTemplateBase(typeof(TestTemplateBase<>));
                return Razor.Parse<TModel>(template, model);
            }
            catch(TemplateCompilationException tce)
            {
                Assert.Fail(string.Join("\n", tce.Errors.Select(e => e.ErrorText)));
            }

            return output;
        }

        protected void StubHostFunctions()
        {
            HostUtil.MapPath = virtualPath => virtualPath.Replace("~", _rootFolder);

            UrlExtensions.FileContentFunc = (urlHelper, file) =>
                {
                    if (file == "~/")
                        return file; // special case

                    file = file.Replace("~", _rootFolder);
                    Assert.That(File.Exists(file), string.Format("File '{0}' does not exist", file));
                    return file;
                };

            UrlExtensions.ActionFunc = (urlHelper, actionName, controllerName) =>
                {
                    return string.Format("Call({1}Controller.{0})", actionName, controllerName);
                };
        }
    }
}
