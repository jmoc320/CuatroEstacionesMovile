using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CuatroEstaciones.Core.Models {

    public class BaseModel : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual int Id { get; set; }

        // SetField (Name, value); // where there is a data member
        protected bool Set<T>(ref T field, T value, [CallerMemberName] string property = null) {
            if (EqualityComparer<T>.Default.Equals(field, value)) {
                return false;
            }

            field = value;
            RaisePropertyChanged(property);
            return true;
        }

        // SetField(()=> somewhere.Name = value; somewhere.Name, value) // Advanced case where you rely on another property
        protected bool Set<T>(T currentValue, T newValue, Action doSet, [CallerMemberName] string property = null) {
            if (EqualityComparer<T>.Default.Equals(currentValue, newValue)) {
                return false;
            }

            doSet.Invoke();
            RaisePropertyChanged(property);
            return true;
        }

        protected void RaisePropertyChanged(string property) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }

    public class NotificationBase<T> : BaseModel where T : class, new() {
        protected T This;

        public static implicit operator T(NotificationBase<T> thing) => thing.This;

        public NotificationBase(T thing = null) {
            This = thing ?? new T();
        }
    }
}