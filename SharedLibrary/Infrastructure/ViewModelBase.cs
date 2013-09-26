using SharedLibrary.Services.Interfaces;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace SharedLibrary.Infrastructure
{
    public interface IViewModelBase : INotifyPropertyChanged
    {
        void ViewNavigatedTo();
        void ViewNavigatingFrom();

        bool IsPremium { get; }
        bool UserInteractionEnabled { get; set; }
    }

    [DataContract]
    public abstract class ViewModelBase : ObservableObject
    {
        protected INavigationService navigationService;

        public ViewModelBase(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value, () => IsBusy); }
        }

        protected bool userInteractionEnabled;
        public bool UserInteractionEnabled
        {
            get { return userInteractionEnabled; }
            set { SetProperty(ref userInteractionEnabled, value, () => UserInteractionEnabled); }
        }

        public virtual void ViewNavigatedTo() { }
        public virtual void ViewNavigatingFrom() { }
    }
}
