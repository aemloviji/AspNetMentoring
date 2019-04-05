using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Module3.ViewComponents
{
    public class Breadcrumbs : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var breadcrumb = new StringBuilder();
            breadcrumb.Append(@"<a href='/'>Home</a> > ");

            var controller = GetControllerName();
            if (!string.IsNullOrEmpty(controller))
            {
                breadcrumb.Append($@"<a href='/{controller}'>{controller}</a>");
            }

            var action = GetActionName();
            if (!string.IsNullOrEmpty(action))
            {
                if (!IsListAction(action))
                {
                    var pageTitle = ViewContext.ViewData["Title"].ToString();
                    breadcrumb.Append($" > {pageTitle}");
                }
            }

            TempData["breadcrumbs"] = breadcrumb.ToString();
            return View();
        }

        private static bool IsListAction(string action)
        {
            return action.ToLower().Contains("index");
        }

        private string GetControllerName()
        {
            var routeValues = ViewContext.RouteData.Values;
            if (routeValues != null)
            {
                if (routeValues.ContainsKey("controller"))
                {
                    return routeValues["controller"].ToString();
                }
            }

            return string.Empty;
        }

        private string GetActionName()
        {
            var routeValues = ViewContext.RouteData.Values;
            if (routeValues != null)
            {
                if (routeValues.ContainsKey("action"))
                {
                    return routeValues["action"].ToString();
                }
            }

            return string.Empty;
        }
    }
}
