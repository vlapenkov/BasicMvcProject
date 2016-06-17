using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC5.App_Start;
using Ninject.Extensions.Wcf;

namespace MVC5
{
    public class NinjectFileLessServiceHostFactory : NinjectServiceHostFactory
    {
        public NinjectFileLessServiceHostFactory()
        {
            SetKernel(NinjectWebCommon.bootstrapper.Kernel);
        }
    }
}