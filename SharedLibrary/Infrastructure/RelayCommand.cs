using System;
using System.Windows.Input;

namespace SharedLibrary.Infrastructure
{
    public class RelayCommand : ObservableObject, ICommand
    {
        private readonly Action execute;
        private bool isEnabled;

        public RelayCommand(Action execute)
        {
            this.execute = execute;
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                SetProperty(ref isEnabled, value);
                if (CanExecuteChanged != null)
                    CanExecuteChanged(this, null);
            }
        }

        public bool CanExecute(object parameter)
        {
            return isEnabled;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
                this.execute();
        }
    }
}
