using System;
using EasyNetQ;
using Microsoft.Practices.Unity;

namespace Drifter.Worker.DI
{
    class UnityAdapter : IContainer, IDisposable
    {
        private readonly IUnityContainer _container;

        public UnityAdapter(IUnityContainer unityContainer)
        {
            _container = unityContainer;
        }

        public TService Resolve<TService>() where TService : class
        {
            return _container.Resolve<TService>();
        }

        public IServiceRegister Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _container.RegisterType<TService, TImplementation>(new ContainerControlledLifetimeManager());
            return this;
        }

        public IServiceRegister Register<TService>(Func<EasyNetQ.IServiceProvider, TService> serviceCreator) where TService : class
        {
            _container.RegisterType<TService>(new ContainerControlledLifetimeManager(), new InjectionFactory(i => serviceCreator(this)));
            return this;
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
