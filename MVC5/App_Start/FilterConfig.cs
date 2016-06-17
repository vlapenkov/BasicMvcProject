using System.Web;
using System.Web.Mvc;

namespace MVC5
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        
        {
            // removed to Ninject Kernel.RegisterServices
        //   filters.Add(new MyActionFilterAttribute());
            
            filters.Add(new HandleErrorAttribute());
           
        }
    }
}
