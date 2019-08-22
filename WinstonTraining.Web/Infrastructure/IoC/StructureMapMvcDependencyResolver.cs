using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WinstonTraining.Web.Infrastructure.IoC
{
    public class StructureMapMvcDependencyResolver : IDependencyResolver
    {
        private readonly IContainer _container;

        public StructureMapMvcDependencyResolver(IContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType.IsInterface || serviceType.IsAbstract)
                return GetInterfaceService(serviceType);

            return GetConcreteService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }

        private object GetConcreteService(Type serviceType)
        {
            return _container.GetInstance(serviceType);
        }

        private object GetInterfaceService(Type serviceType)
        {
            return _container.TryGetInstance(serviceType);
        }
    }
}