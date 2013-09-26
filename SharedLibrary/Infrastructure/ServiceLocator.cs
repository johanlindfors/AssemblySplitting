using SharedLibrary.Infrastructure.Ioc;

namespace SharedLibrary.Infrastructure
{
    public class ServiceLocator : Singleton<IContainer>
    {
        public ServiceLocator(IContainer container)
        {
            sharedInstance = container;
        }

        public static T Resolve<T>(string key = "")
        {
            return sharedInstance.Resolve<T>(key);
        }
    }
}
