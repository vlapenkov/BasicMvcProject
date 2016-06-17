using MVC5.Models;
using MVC5.Services;
using MVCBoilerPlate.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5.Controllers
{
   [MyActionFilter]
    public class HomeController : BaseController
    {
       ICacheService _cache;
       IProductService _products;
       IPartnerService _partnerservice;
       ISiteProvider _siteProvider;


       public HomeController(ICacheService cache, IProductService products, IDbContext dbContext, IPartnerService partnerservice,ISiteProvider siteProvider)
           : base(dbContext, partnerservice, siteProvider)
       {
           _cache = cache;
           _products = products;
           
       }
        public ActionResult Index()
        {
            var products=_products.GetProducts(_=>true);
            
            //_cache.AddOrUpdate("products", products,TimeSpan.FromMinutes(1));
            _cache.AddOrUpdate("products", products,DateTimeOffset.UtcNow.AddSeconds(30));
            _cache.AddOrUpdate("str", "string value", DateTimeOffset.UtcNow.AddSeconds(10));
            
            return View();
            
	

        }
        public ActionResult Clear()
        {
            _cache.Remove("products");

            return Content("Removed");

        }
    /*   [NonAction]
       public ActionResult Throw()
       {
           //throw new NotImplementedException("something wrong");
       }
     * */
       
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}