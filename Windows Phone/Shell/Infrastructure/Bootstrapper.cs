﻿using Facades.Services;
using Microsoft.Practices.Unity;
using Pages;
using SharedLibrary.Infrastructure;
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

            // ViewModels
            container.RegisterType<IMainViewModel, MainViewModel>();

            new ServiceLocator(new UnityContainerWrapper(container));            

            Debug.WriteLine("Bootstrapping ends");        
        }
    }
}
