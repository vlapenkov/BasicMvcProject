using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC5.Controllers;
using MVC5.Models;

namespace MVC5
{
    public class PartnerService:IPartnerService
    {
         IDbContext _dbcontext;
        public PartnerService(IDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public string GetCulture()
        {
            string default_culture = "RU-ru";
            return _dbcontext.Partners.Any() ? _dbcontext.Partners.First().Culture : default_culture;
        }
    }
}