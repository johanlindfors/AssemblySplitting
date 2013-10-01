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
using TransitionLibrary.Transitions.Transitions;

namespace Pages
{
    public partial class LoginPage : PhoneApplicationPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (e.Uri.OriginalString.Contains("fromLogin"))
            {
                Application.Current.Resources.Remove("mainPageTransition");
                Application.Current.Resources.Add("mainPageTransition",PageTransitionType.ZoomIn);
            }
            base.OnNavigatingFrom(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            
            if(!e.Uri.OriginalString.Contains("Facebook.Client"))
                ServiceLocator.Resolve<INavigationService>().RemoveBackEntry();
        }
    }
}