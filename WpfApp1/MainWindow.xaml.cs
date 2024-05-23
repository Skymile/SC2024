using System.Drawing;
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
        private Bitmap _input;
        private Bitmap _output;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _vm = new MainWindowVM();

            _algo = new Algorithm();

            _input = new Bitmap("C:/Samples/apple.png");
            _output = new Bitmap("C:/Samples/apple.png");

            var bmp = _algo.ApplyBinarization(_input, _output, 128);
            MainImg.Source = bmp.ToBitmapSource();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_algo is null) 
                return;

            var bmp = _algo.ApplyBinarization(_input, _output, (int)e.NewValue);
            MainImg.Source = bmp.ToBitmapSource();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _vm.SampleText = "abc";
            _vm.SampleLength = 4;
        }

        private MainWindowVM _vm;
        private Algorithm _algo;
    }
}