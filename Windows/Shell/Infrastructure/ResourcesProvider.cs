using SharedLibrary.Infrastructure;
using SharedLibrary.Infrastructure.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Shell.Infrastructure
{
    internal static class ResourcesProvider
    {
        public static void Update(ResourceDictionary resources)
        {
            resources.Add("LocalizedStrings", new LocalizedStrings());
            resources.Add("ViewModelLocator", new ViewModelLocator(ServiceLocator.Resolve<IContainer>()));
            resources.Add("DeviceSpecificsLocator", new DeviceSpecificsLocator(ServiceLocator.Resolve<IContainer>()));
        

        }
    }
}
