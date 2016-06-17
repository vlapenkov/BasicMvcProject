using System.Web.Mvc;
using MVC5.Controllers;
using Ninject.Extensions.Wcf;
using Ninject.Web.Mvc.FilterBindingSyntax;
using Ninject.Extensions.Conventions;
using MVCBoilerPlate.Services;



[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MVC5.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(MVC5.App_Start.NinjectWebCommon), "Stop")]

namespace MVC5.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using MVC5.Models;
    using MVC5.Filters;
    using MVC5.Services;
    

    public static class NinjectWebCommon 
    {
        public static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            //var kernel = new StandardKernel();
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            
            kernel.Bind(x =>
            {
                x.FromThisAssembly() // Scans currently assembly
                 .SelectAllClasses() // Retrieve all non-abstract classes
                 
                 .BindDefaultInterface(); // Binds the default interface to them;
            }); 
            kernel.Bind<IDbContext>().To<AppDbContext>().InRequestScope();
        //    kernel.Bind<IDbContext>().To<AppDbContext>();
         /*   kernel.Bind<IProductService>().To<ProductService>();
            kernel.Bind<IPartnerService>().To<PartnerService>();
            kernel.Bind<ILogger>().To<Logger>();  */

            kernel.BindFilter<MyActionFilterAttribute>(FilterScope.Global, 1).InRequestScope();
            kernel.BindFilter<LoggedExceptionFilter>(FilterScope.Global, 2).InRequestScope();

            kernel.Rebind<ICacheService>().To<CacheService>().InSingletonScope();
           // kernel.Bind<ICacheService>().To<CacheService>().InSingletonScope();

            kernel.Bind<HttpContextBase>().ToMethod(c => new HttpContextWrapper(HttpContext.Current));

            kernel.BindFilter<LogFilter>(FilterScope.Controller, 0).WhenControllerType<HomeController>().WithConstructorArgument("IDbContext",(p)=>p.Kernel.Get<IDbContext>());
 //.WithConstructorArgument("logLevel", ("Info"));
        }

    
    }
}
