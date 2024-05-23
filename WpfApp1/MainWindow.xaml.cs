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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = _vm = new MainWindowVM();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _vm.SampleText = "abc";
            _vm.SampleLength = 4;
        }

        private MainWindowVM _vm;
    }
}