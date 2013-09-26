using Microsoft.Practices.Unity;
using SharedLibrary.Infrastructure.Ioc;

namespace Shell.Infrastructure
{
    internal class UnityContainerWrapper : IContainer
    {
        readonly IUnityContainer container;

        public UnityContainerWrapper(IUnityContainer container)
        {
            this.container = container;
        }

        public T Resolve<T>(string key = "")
        {
            return container.Resolve<T>(key);
        }
    }
}
