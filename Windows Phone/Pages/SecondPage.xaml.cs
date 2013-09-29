using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SharedLibrary.Infrastructure;
using SharedLibrary.Services.Interfaces;
using System.Diagnostics;

namespace Pages
{
    public partial class SecondPage : PhoneApplicationPage
    {
        public SecondPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            foreach (var item in NavigationContext.QueryString)
            {
                Debug.WriteLine("Parameter '{0}' = '{1}'", item.Key, item.Value);
            }
        }

        private async void OnButtonClicked(object sender, RoutedEventArgs e)
        {
            var service = ServiceLocator.Resolve<IProtectionService>();
            var data = "Johan Lindfors";
            var key = "password";

            var encrypted = await service.ProtectAsync(data, key);

            var decrypted = await service.UnprotectAsync(encrypted, key);
        }

        void TestGC()
        {
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}