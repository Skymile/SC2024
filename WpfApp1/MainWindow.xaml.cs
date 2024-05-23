using System.ComponentModel;
using System.Windows;

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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string SampleText 
        { 
            get => sampleText;
            set 
            {
                sampleText = value;
                PropertyChanged?.Invoke(this, new(nameof(SampleText)));
            } 
        }
        private string sampleText;

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}