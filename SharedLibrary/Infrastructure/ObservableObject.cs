using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace SharedLibrary.Infrastructure
{
    [DataContract]
    public class ObservableObject<T> : ObservableObject
    {
        protected T _item;
        public T Item
        {
            get { return _item; }
            protected set { _item = value; }
        }

        public ObservableObject()
        {
             
        }

        public ObservableObject(T item)
        {
            _item = item;
        }
    }

    [DataContract]
    public class ObservableObject : INotifyPropertyChanged
    {
        public void SetProperty<T>(ref T store, T value, [CallerMemberName] string propertyName = "")
        {
            if ((store == null && value != null) || !store.Equals(value))
            {
                store = value;
                RaisePropertyChanged(propertyName);
            }
        }

        public void SetProperty<T>(ref T store, T value, Expression<Func<T>> propertyExpression)
        {
            if ((store == null && value != null) || !store.Equals(value))
            {
                store = value;
                RaisePropertyChanged(propertyExpression);
            }
        }

        public virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression.Body.NodeType == ExpressionType.MemberAccess)
            {
                var memberExpression = propertyExpression.Body as MemberExpression;
                string propertyName = memberExpression.Member.Name;
                this.RaisePropertyChanged(propertyName);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
