using SharedLibrary.Infrastructure.Ioc;
using SharedLibrary.Services.Interfaces;

namespace SharedLibrary.Infrastructure
{
    public class DeviceSpecificsLocator : Singleton<IContainer>, IDeviceSpecificsService
    {
        public DeviceSpecificsLocator(IContainer container)
        {
            sharedInstance = container;
        }

        public int MaxRows
        {
            get { return sharedInstance.Resolve<IDeviceSpecificsService>().MaxRows; }
        }
    }
}
