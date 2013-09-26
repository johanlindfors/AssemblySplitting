using System.Collections.Generic;
using System.Windows;
using Microsoft.Phone.Controls;
using SharedLibrary.Services.Interfaces;
using SharedLibrary.Infrastructure;

namespace Pages
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnButtonClicked(object sender, RoutedEventArgs e)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("firstname", "johan");
            parameters.Add("lastname", "lindfors");
            ServiceLocator.Resolve<INavigationService>().Navigate("SecondPage", parameters);
        }
    }
}