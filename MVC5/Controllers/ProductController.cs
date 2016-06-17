using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5.Models;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Globalization;
using System.Web.Routing;
using System.Threading;
using MVC5.Services;
using MVCBoilerPlate.Services;

namespace MVC5.Controllers
{
    
    public class ProductController : BaseController
    {
      
        private readonly IProductService _productService;
        private readonly IPartnerService _partnerService;
        private readonly HttpContextBase _contextBase;
        private readonly ISiteProvider _siteProvider;
        ICacheService _cache;

        public ProductController(IDbContext dbContext, IProductService productService, IPartnerService partnerService, ISiteProvider siteProvider,HttpContextBase context,ICacheService cache)
            : base(dbContext,partnerService,siteProvider)
        {
            _productService = productService;
            _partnerService = partnerService;
            _contextBase = context;
            _cache = cache;
            
        }


      

        public  ActionResult Index()

        {

            var o1= DependencyResolver.Current.GetService<IDbContext>();
            var o2 = DependencyResolver.Current.GetService<IDbContext>();

            var ps1 = DependencyResolver.Current.GetService<IProductService>();
            var ps2 = DependencyResolver.Current.GetService<IProductService>();

            Debug.WriteLine(Object.ReferenceEquals(o1, o2) ? "IDbContext is the same" : "IDbContext are different");
            Debug.WriteLine(Object.ReferenceEquals(ps1, ps2) ? "IPartnerService is the same" : "IPartnerService are different");

            if (_contextBase.Session["first"]==null)_contextBase.Session["first"] = DateTime.Now;
            //_contextBase
            ViewBag.first = _contextBase.Session["first"];
            ViewBag.AddInfo = "info";
            //Debug.WriteLine(product.Name);
            Debug.WriteLine("DbContext in contr:" + DbContext.GetHashCode());

            var products=_cache.GetOrAdd("products", ()=>DbContext.Products.Include(p => p.Producer).AsEnumerable<Product>(), null, null);

            return View(products);
            //return View(DbContext.Products.Include(p => p.Producer).ToArray());
        }

        // GET: /Default1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
              Product product = _productService.Find((int)id);
            
          //  Product product = _db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: /Default1/Create
        public ActionResult Create()
        {

          


            ViewBag.ProducerId = new SelectList(DbContext.Producers, "Id", "Name");

            return View();
        }

        // POST: /Default1/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*[Bind(Include="Id,Name,ReleaseDate,Price")]*/ Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.Add(product);
              //  _db.Products.Add(product);
              //  _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: /Default1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product=_productService.Find((int)id);
          
            if (product == null)
            {
                return HttpNotFound();
            }

            //    ViewBag.SL = new SelectList(ViewBag.Producers, "Id", "Name"); 
           ViewBag.Producers = DbContext.Producers;
         
            return View(product);
        }

        // POST: /Default1/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,ReleaseDate,Price,ProducerId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.Update(product);
              //  _productService.
       
        //        _db.SaveChanges(); 
                return RedirectToAction("Index");
            } 
            return View(product);
        }

        // GET: /Default1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = _productService.Find((int)id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Default1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = _productService.Find((int)id);
            _productService.Remove(product);
            //_db.Products.Remove(product);
            //_db.SaveChanges();
            return RedirectToAction("Index");
        }

        
    }
}
