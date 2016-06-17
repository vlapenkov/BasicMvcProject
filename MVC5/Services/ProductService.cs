using MVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Diagnostics;

namespace MVC5
{
    public class ProductService : IProductService
    {
        private IDbContext _context;

        public ProductService(IDbContext context)
        {
            _context = context;
            Debug.WriteLine("Dbcontext in ProductService:" + context.GetHashCode());
        }

        public List<Product> GetProducts(Expression<Func<Product, bool>> predicate)
        {
            return _context.Products.Where(predicate).ToList();

        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();

        }

        public void Remove(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();

        }

        
        public void Update(Product product)
        {
            _context.Update(product);
            

        }



        public int Count()
        {
            return _context.Products.Count();
        }


        public decimal Sum
        {
            get { return _context.Products.Sum(x => x.Price); }
        }


        public Product Find(int id)
        {
            return _context.Products.Find(id);
        }
    }
    
}