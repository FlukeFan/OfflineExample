using System.Web.Mvc;
using RazorEngine.Templating;

namespace OfflineExample.Test
{
    public class TestTemplateBase<TModel> : TemplateBase<TModel>
    {
        public UrlHelper Url { get; set; }
    }
}
