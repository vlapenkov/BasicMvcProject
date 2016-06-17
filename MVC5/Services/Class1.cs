using MVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5.Services
{
    public interface ISiteProvider
    {
        void Initialise(string host);
        Site GetCurrentSite();
    }

    public class SiteProvider : ISiteProvider
    {
        IDbContext _db;
        Site _site;

        public SiteProvider(IDbContext db)
        {
            _db = db;
        }

        public void Initialise(string host)
        {
            _site = _db.Sites.SingleOrDefault(s => s.Name == host);
        }

        public Site GetCurrentSite()
        {
            return _site;
        }
    }
}