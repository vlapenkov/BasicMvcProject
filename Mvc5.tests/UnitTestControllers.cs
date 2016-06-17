using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVC5.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;
using MVC5;
using  System.Web;
using System.Web.Mvc;
using Moq;
using MVC5.Controllers;

namespace Mvc5.tests
{
  

    [TestClass]
    public class UnitTestControllers
    {
        Mock<HttpContextBase> context = new Mock<HttpContextBase>();
        private IDbContext dbcontext;
        private ProductService productService;
        private PartnerService partnerService;

        [TestInitialize]
        
        public   void TestInitialize()
        {
             dbcontext = new MemoryDbContext { Products = new MemDbSet<Product>() };
            dbcontext.Products.Add(new Product { Id = 1, Name = "123" });
            dbcontext.Products.Add(new Product { Id = 2, Name = "345" });
             productService = new ProductService(dbcontext);
             partnerService = new PartnerService(dbcontext);

            
         
            context.Setup(c => c.Session["CartId"]).Returns("volod");
        //    Assert.AreEqual(context.Object.Session["CartId"], "volod");
            
        }
        
        [TestMethod]
        public void TestMoqHttpContextBase()
        {
            Assert.AreEqual(context.Object.Session["CartId"], "volod");
        }


        [TestMethod]
        public void TestProductController_IndexHasProducts()
        {
            ProductController controller = new ProductController(dbcontext, productService, partnerService,null, context.Object,null);

            var result = (IList<Product>)(controller.Index() as ViewResult).Model;
            
          // var result = (IList<Product>)(controller.ViewData.Model);

             Assert.AreEqual(result.Count,2);

          
        }

        [TestMethod]
        public void TestProductController_IndexViewbag()
        {
            ProductController controller = new ProductController(dbcontext, productService, partnerService,null, context.Object,null);

            var result = controller.Index() as ViewResult;


            Assert.AreEqual(result.ViewBag.AddInfo as string, "info");


        }

      

    }
}
