using MVC5.Models;
using MVC5.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC5.Controllers
{
    public class BaseController : Controller
    {
        private IDbContext _dbContext;
        private IPartnerService _partnerService;
        ISiteProvider _siteProvider;

     /*   public BaseController(IDbContext dbContext)
        {
       
            ISiteProvider _siteProvider;
        } */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="partnerService"></param>
        /// <param name="siteProvider"></param>
        public BaseController(IDbContext dbContext, IPartnerService partnerService,ISiteProvider siteProvider)
        {
            _partnerService = partnerService;
            _siteProvider = siteProvider;
            _dbContext = dbContext;
        }

        protected override void Initialize(RequestContext requestContext)
        {

            string[] host = requestContext.HttpContext.Request.Headers["Host"].Split(':');

            _siteProvider.Initialise(host[0]);

            string culture=_partnerService.GetCulture();

            var cultureInfo = new CultureInfo(culture);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            var product = _dbContext.Products.FirstOrDefault();
            Debug.WriteLine(product);
            base.Initialize(requestContext);
        }

        public IDbContext DbContext {
            get { return _dbContext ?? new AppDbContext(); }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewData["Site"] = Site;

            base.OnActionExecuting(filterContext);
        }
        public Site Site
        {
            get
            {
                return _siteProvider.GetCurrentSite();
            }
        }

	}
}