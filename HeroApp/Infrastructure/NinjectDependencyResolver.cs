using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.Mvc;
using CodeFirst;
using CodeFirst.Repositories;
using Ninject;
using Ninject.Web.Common;

namespace HeroApp.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<HeroContext>().ToSelf().InSingletonScope();
            kernel.Bind(typeof(IGenericRepository<>)).To(typeof(EFGenericRepository<>));
        }
    }
}