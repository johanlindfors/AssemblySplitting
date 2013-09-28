using SharedLibrary.Infrastructure;
using SharedLibrary.Services.Interfaces;
using System;
using System.Diagnostics;
using Windows.Security.Cryptography;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Pages
{
    public sealed partial class SecondPage : Page
    {
        public SecondPage()
        {
            this.InitializeComponent();
        }

       
        private async void OnButtonClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
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
