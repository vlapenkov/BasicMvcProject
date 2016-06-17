using System;
using MVC5.Models;
using System.Linq.Expressions;

namespace MVC5
{
   public interface IProductService
    {
        void Add(Product product);
        void Update(Product product);
        System.Collections.Generic.List<Product> GetProducts(Expression<Func<Product, bool>> predicate);
        void Remove(MVC5.Models.Product product);
       int Count();

       Product Find(int id);
       
       decimal Sum
       {
           get;
       }
    }
}
