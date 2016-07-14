using MVC5.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5.Filters
{
    /// <summary>
    /// this filter is used only in homecontroller accoring to 
    /// </summary>
    public class LogFilter : IActionFilter
    {
       /* [Inject]
        public IDbContext DbContext { get; set; } */
        private readonly string  _loglevel;
        private readonly IDbContext _dbContext;

        public LogFilter(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var partner=_dbContext.Partners.FirstOrDefault();
            Debug.WriteLine(partner); 
        //    throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
          //  throw new NotImplementedException();
        }
    }
}