using System.ComponentModel;
using System.Runtime.CompilerServices;

// MVVM
// Model-View-ViewModel
// Model
//   Odpowiada za dane
// View
//   XAML i bindingi (wiązania danych)
// ViewModel
//   Dane widoku + logika

namespace WpfApp1
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public T Get<T>([CallerMemberName] string propName = "") =>
            _props.TryGetValue(propName, out var obj)
                ? (T)obj
                : default!;

        public void Set<T>(T value, [CallerMemberName] string propName = "")
        {
            _props[propName] = value!;
            PropertyChanged?.Invoke(this, new(propName));
        }

        private readonly Dictionary<string, object> _props = [];

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}