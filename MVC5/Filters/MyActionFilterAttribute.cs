using MVC5.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
//using System.Web.Routing;

namespace MVC5
{
    public class MyActionFilterAttribute: FilterAttribute,IActionFilter
    {
    [Inject]
        public IDbContext DbContext { get; set; }
        [Inject]
        public IProductService ProductService { get; set; } 
        
    
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        //    filterContext.Controller.ViewBag.FirstProduct = DbContext.Products.FirstOrDefault();
            filterContext.Controller.ViewBag.HasProducts = DbContext.Products.Any();
    }
        public void OnActionExecuted(ActionExecutedContext filterContext)
        { }
    }
}

/*   if (String.Compare(filterContext.RouteData.Values["Controller"].ToString(), "product", true)==0)
            {
                if (DbContext.Products.Count() > 0)
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Movies",
                        action = "Index"
                    }));
            }*/