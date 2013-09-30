using System;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using SharedLibrary.Infrastructure;
using SharedLibrary.Services.Interfaces;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Pages
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

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
            var johan = new Person { Name = "Johan", Age = 40 };
            var filename = "johan.xml";

            var storage = ServiceLocator.Resolve<IStorageService>();
            await storage.Write(johan, filename);

            var objRead = await storage.Read<Person>(filename);
        }

        void TestSettings()
        {
            var settings = ServiceLocator.Resolve<ISettingsService>();

            var username = settings.Get<string>("username");

            settings.Set("username", "johanlindfors");
        }

        async Task TestDPAPI()
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