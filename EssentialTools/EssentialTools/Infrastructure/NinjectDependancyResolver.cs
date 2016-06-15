
using EssentialTools.Models;
using EssentialTools.Models.Substract;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EssentialTools.Infrastructure
{
    public class NinjectDependancyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependancyResolver(IKernel kernel)
        {
            this.kernel = kernel;
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
            kernel.Bind<IValueCalculator>().To<LinqValueCalculator>();
            kernel.Bind<IDiscountHelper>().To<DiscountHelper>().WithPropertyValue("DiscountSize", 50m);
            kernel.Bind<IDiscountHelper>().To<FlexibleDiscountHelper>().WhenInjectedInto<LinqValueCalculator>();
        }
    }
}
