using MVC5.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVC5.Filters
{
    public class LoggedExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;
        public LoggedExceptionFilter(ILogger logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext filterContext)
        {


            if (filterContext.IsChildAction) return;
            var result = new StringBuilder();



            string innerError = String.Empty;
            try
            {
                var context = filterContext.HttpContext;

                if (!context.Request.IsAuthenticated)
                {
                    _logger.Error("test error");
                    return;
                }

                if (!context.Request.IsAuthenticated || context.Request.IsAjaxRequest()) return;

                result.AppendLine("Path:" + context.Request.Url.AbsolutePath).AppendLine("Method:" + context.Request.HttpMethod);


                string action = filterContext.RouteData.Values["action"].ToString();
                string controller = filterContext.RouteData.Values["controller"].ToString();

                if (controller != null) result.AppendLine("controller:" + controller);
                if (action != null) result.AppendLine("action:" + action);

                var refferer = context.Request.UrlReferrer;

                if (refferer != null) result.AppendLine("urlrefferer:" + refferer.AbsolutePath);

                if (context != null && context.User != null && context.Request.IsAuthenticated)

                    result.AppendLine("User:" + context.User.Identity.Name);

                // record info
                result.Append(filterContext.Exception.ToString());
                _logger.Error(result.ToString());

            }
            finally { }
        }
    }
}