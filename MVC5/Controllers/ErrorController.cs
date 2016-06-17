using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVC5.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            //  Response.StatusCode = (int)HttpStatusCode.OK;

            return View();
        }

        // не работает! Обрабатывается HandleErrorAttribute
        public ActionResult InternalError()
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return View();
        }

    }
}