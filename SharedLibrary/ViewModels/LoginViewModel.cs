using SharedLibrary.Infrastructure;
using SharedLibrary.Services.Interfaces;
using SharedLibrary.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SharedLibrary.ViewModels
{
    public class LoginViewModel : ViewModelBase, ILoginViewModel
    {
        public ICommand LoginCommand { get; private set; }
        public ICommand RegisterCommand { get; private set; }
        public ICommand LoginWithFacebookCommand { get; private set; }

        private string username = string.Empty;
        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }

        private string password = string.Empty;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        public LoginViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            LoginCommand = new RelayCommand(LoginAction) { IsEnabled = false };
            RegisterCommand = new RelayCommand(RegisterAction) { IsEnabled = false };
            LoginWithFacebookCommand = new RelayCommand(LoginWithFacebookAction) { IsEnabled = true };
        }

        void LoginAction()
        {

        }

        void RegisterAction()
        {

        }

        async void LoginWithFacebookAction()
        {
            IsBusy = true;
            try
            {
                ((RelayCommand)LoginWithFacebookCommand).IsEnabled = false;
                var service = ServiceLocator.Resolve<ISocialService>();
                var result = await service.SignIn();
                if (result.Result)
                {
                    var parameters = new Dictionary<string, object>();
                    parameters.Add("fromLogin", true);
                    navigationService.Navigate("MainPage", parameters);
                }
            }
            catch { }
            finally {
                ((RelayCommand)LoginWithFacebookCommand).IsEnabled = true; 
                IsBusy = false;
            }            
        }
    }
}
