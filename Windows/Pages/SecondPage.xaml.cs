using System;
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

       
        private void OnButtonClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
