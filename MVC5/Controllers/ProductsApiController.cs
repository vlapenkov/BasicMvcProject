using MVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ninject.Infrastructure.Language;

namespace MVC5.Controllers
{
    public class ProductsApiController : ApiController
    {
        private readonly IDbContext _dbContext;
       /* public ProductsApiController()
           {
               _dbContext = new AppDbContext();
           }  */
        public ProductsApiController(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/<controller>
        public IEnumerable<ProductDTO> Get()
        {
            //return _dbContext.Products.ToArray();

         //   return _dbContext.Producers.ToArray();

            return _dbContext.Products.Select(p=>new ProductDTO{Id = p.Id,Name = p.Name,Price = p.Price,ReleaseDate = p.ReleaseDate}).ToArray();

          //  return new[] {new Producer {Id = 1, Name = "first"}, new Producer {Id = 2, Name = "second"}};

        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}