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

namespace Pages
{
    public partial class LoginPage : PhoneApplicationPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            
            if(!e.Uri.OriginalString.Contains("Facebook.Client"))
                ServiceLocator.Resolve<INavigationService>().RemoveBackEntry();
        }
    }
}