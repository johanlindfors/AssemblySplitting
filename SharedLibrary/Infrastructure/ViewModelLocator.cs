using SharedLibrary.Infrastructure.Ioc;
using SharedLibrary.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Infrastructure
{
    public class ViewModelLocator: Singleton<IContainer>
    {
        public ViewModelLocator(IContainer container)
        {
            sharedInstance = container;
        }

        public IMainViewModel Main
        {
            get { return sharedInstance.Resolve<IMainViewModel>(); }
        }

        public ILoginViewModel Login
        {
            get { return sharedInstance.Resolve<ILoginViewModel>(); }
        }
    }
}
