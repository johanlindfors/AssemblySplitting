using System.Collections.Generic;
using System.Windows;
using Microsoft.Phone.Controls;
using SharedLibrary.Services.Interfaces;
using SharedLibrary.Infrastructure;
using SharedLibrary.ViewModels.Interfaces;
using System.Windows.Navigation;
using TransitionLibrary.Transitions.Transitions;

namespace Pages
{
    public partial class MainPage : PhoneApplicationPage
    {
        IMainViewModel ViewModel { get { return this.DataContext as IMainViewModel; } }

        public MainPage()
        {
            InitializeComponent();
            //this.DataContext = ServiceLocator.Resolve<IMainViewModel>();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await ViewModel.ViewNavigatedTo();
        }

        //private void OnButtonClicked(object sender, RoutedEventArgs e)
        //{
        //    var parameters = new Dictionary<string, object>();
        //    parameters.Add("firstname", "johan");
        //    parameters.Add("lastname", "lindfors");
        //    ServiceLocator.Resolve<INavigationService>().Navigate("SecondPage", parameters);
        //}
    }
}