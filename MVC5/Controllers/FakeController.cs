using MVC5.Models;
using MVCBoilerPlate.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5.Controllers
{
    public class FakeController : Controller
    {
        // something like fake
        ICacheService _cache;
       
       public FakeController(ICacheService cache)
       {
           _cache = cache;
           
       }
        // GET: Fake
        public ActionResult Index()
        {
            
            var products = _cache.Get<IEnumerable<Product>>("products");
            if (products==null || !products.Any()) products=Enumerable.Empty<Product>();
            ViewBag.str = _cache.Get<string>("str");
            return View(products);
        }

        [HttpGet]
        public ActionResult MultiSelect(FakeModelView model)
        {

            
            return View(model);
        }

        /*
        [HttpPost]
        public ActionResult MultiSelect(FakeModelView model)
        {


            return View(model);
        }*/


    }
}