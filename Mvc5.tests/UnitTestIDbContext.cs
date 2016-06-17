using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVC5.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;
using MVC5;

namespace Mvc5.tests
{
  

    [TestClass]
    public class UnitTestIDbContext
    {
        

        [TestMethod]
        public   void TestProductsCountAfterAdd()
        {
            var dbcontext = new MemoryDbContext();

            var product= new Product { Id = 1, Name = "123" };
            dbcontext.Products.Add(product);
            dbcontext.Products.Add(new Product { Id = 2, Name = "345" });

            dbcontext.Products.Remove(product);

            Assert.AreEqual(dbcontext.Products.Count(),1);
            
            
        }

        [TestMethod]
        public void TestProductsSetAfterUpdated()
        {
            var dbcontext = new MemoryDbContext {Products = new MemDbSet<Product>()};

            dbcontext.Products.Add(new Product {Id = 1, Name = "123"});
            dbcontext.Products.Add(new Product { Id = 2, Name = "345" });

           // product.Name = "555";

            var newProduct = new Product { Id = 1, Name = "999" };
           dbcontext.Update(newProduct);
            dbcontext.SaveChanges();

            Assert.AreEqual(dbcontext.Products.ToArray()[0].Name, "999");


        }

        [TestMethod]
        public void TestProductServiceAfterUpdated()
        {
            var dbcontext = new MemoryDbContext { Products = new MemDbSet<Product>() };


            var productService = new ProductService(dbcontext);

            var productToCheck = new Product {Id = 1, Name = "123", Price = 100};
            productService.Add(productToCheck);
            productService.Add(new Product { Id = 2, Name = "123", Price = 350 });

            var productToCheck2 = new Product { Id = 1, Name = "333", Price = 0 };

            productService.Update(productToCheck2);

             productToCheck2 = new Product { Id = 2, Name = "333", Price = 10 };

             productService.Update(productToCheck2);

            Assert.AreEqual(productService.Sum, 10);


        }

        [TestMethod]
        public void TestMemberwiseClone()
        {
            var product1 = new Product { Id = 1, Name = "123" };
            var product2 = new Product { Id = 1, Name = "678" };

         //   product2=product1.Clone();
           
            

            Assert.AreEqual(product1.Name, product2.Name);
        }


        [TestMethod]
        public void TestProductServiceCountAfterAdd()
        {
            var dbcontext = new MemoryDbContext { Products = new MemDbSet<Product>() };

           
            var productService = new ProductService(dbcontext);

            productService.Add(new Product { Id = 1, Name = "123" });
            productService.Add(new Product { Id = 2, Name = "123" });


            Assert.AreEqual(productService.Count(), 2);


        }


        [TestMethod]
        public void TestProductServiceSumAfterAdd()
        {
            var dbcontext = new MemoryDbContext { Products = new MemDbSet<Product>() };

       
            var productService = new ProductService(dbcontext);

            productService.Add(new Product { Id = 1, Name = "123",Price = 100});
            productService.Add(new Product { Id = 2, Name = "123" ,Price = 350});


            Assert.AreEqual(productService.Sum, 450);


        }

    }
}
