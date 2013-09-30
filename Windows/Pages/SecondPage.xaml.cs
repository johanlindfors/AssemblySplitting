using SharedLibrary.Infrastructure;
using SharedLibrary.Services.Interfaces;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Pages
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public sealed partial class SecondPage : Page
    {
        public SecondPage()
        {
            this.InitializeComponent();
        }


        private async void OnButtonClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
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

            settings.Set<string>("username", "johanlindfors");
        }

        async Task TestProtectionService()
        {
            var protectionService = ServiceLocator.Resolve<IProtectionService>();

            var data = "Johan Lindfors";
            var key = "LOCAL=user";

            var protectedData = await protectionService.ProtectAsync(data, key);

            //var protectedDataAsString = CryptographicBuffer.ConvertBinaryToString(BinaryStringEncoding.Utf8,CryptographicBuffer.CreateFromByteArray(protectedData));

            //Debug.WriteLine(protectedDataAsString);

            var decodedData = await protectionService.UnprotectAsync(protectedData, key);
        }
    }
}
