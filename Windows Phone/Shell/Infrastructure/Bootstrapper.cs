using Facades.Services;
using Microsoft.Practices.Unity;
using Pages;
using SharedLibrary.Infrastructure;
using SharedLibrary.Infrastructure.Ioc;
using SharedLibrary.Services;
using SharedLibrary.Services.Interfaces;
using SharedLibrary.ViewModels;
using SharedLibrary.ViewModels.Interfaces;
using System.Diagnostics;

namespace Shell.Infrastructure
{
    internal static class Bootstrapper
    {
        public static void BuildUp() 
        {                    
            Debug.WriteLine("Bootstrapping begins");

            var navigationService = new RootFrameNavigationService(App.RootFrame);
            navigationService.RegisterView("MainPage", "/Pages;component/MainPage.xaml");
            navigationService.RegisterView("SecondPage", "/Pages;component/SecondPage.xaml");
            navigationService.RegisterView("ThirdPage", "/Pages;component/ThirdPage.xaml");

            var x = typeof(MainPage); // Required to force JIT of Pages.dll

            var container = new UnityContainer();

            // Services
            container.RegisterInstance<INavigationService>(navigationService);
            container.RegisterType<IDialogService, DialogService>();
            container.RegisterType<IProtectionService, DPAPIProtectionService>();
            container.RegisterType<ISettingsService, SettingsService>();
            container.RegisterType<ISerializerService, XmlSerializerService>();
            container.RegisterType<IStorageService, IsolatedStorageService>();

            // ViewModels
            container.RegisterType<IMainViewModel, MainViewModel>();

            // Register container for ViewModelLocator
            var containerWrapper = new UnityContainerWrapper(container);
            container.RegisterInstance<IContainer>(containerWrapper);

            new ServiceLocator(containerWrapper);

            Debug.WriteLine("Bootstrapping ends");        
        }
    }
}
