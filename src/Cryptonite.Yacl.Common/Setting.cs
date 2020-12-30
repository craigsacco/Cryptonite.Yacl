using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Cryptonite.Yacl.Common
{
    public abstract class Setting<T> : ISetting, INotifyPropertyChanged
    {
        public abstract String Name { get; }

        public abstract String AbbreviatedName { get; }

        public abstract T Value { get; set; }

        public abstract String DisplayValue { get; }

        public abstract Type UnderlyingType { get; }

        public abstract void Parse(String value);

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] String name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
