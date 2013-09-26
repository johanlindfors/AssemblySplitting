using Facades.Services;
using Microsoft.Practices.Unity;
using Pages;
using SharedLibrary.Infrastructure;
using SharedLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Shell.Infrastructure
{
    internal static class Bootstrapper
    {
        public static void BuildUp(Frame frame)
        {
            Debug.WriteLine("Bootstrapping begins");

            var navigationService = new FrameNavigationService(frame);
            navigationService.RegisterView("MainPage", typeof(MainPage));
            navigationService.RegisterView("SecondPage", "/Pages;component/SecondPage.xaml");
            navigationService.RegisterView("ThirdPage", "/Pages;component/ThirdPage.xaml");

            var x = typeof(MainPage); // Required to force JIT of Pages.dll

            var container = new UnityContainer();
            container.RegisterInstance<INavigationService>(navigationService);
            container.RegisterType<IDialogService, DialogService>();

            new ServiceLocator(new UnityContainerWrapper(container));

            Debug.WriteLine("Bootstrapping ends");
        }
    }
}
