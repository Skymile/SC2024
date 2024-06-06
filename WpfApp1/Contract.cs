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
    public static class Contract
    {
        public static void Requires<T>(bool condition,
                string? message = null,
                [CallerFilePath] string path = "",
                [CallerLineNumber] int line = 0,
                [CallerMemberName] string name = ""
            )
            where T : Exception
        {
            if (condition)
                return;
            throw new AggregateException(
                new Exception(message ?? $"Error occured at line {line} in {path} in method {name}"),
                Activator.CreateInstance<T>()
            );
        }
    }
}