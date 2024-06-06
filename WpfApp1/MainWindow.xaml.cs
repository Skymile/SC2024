using System.Windows;
using System.Windows.Controls;

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
        private Picture _input;
        private Picture _output;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _vm = new MainWindowVM();

            _input  = new Picture("C:/Samples/cat.png");
            _output = new Picture("C:/Samples/cat.png");

            var algo = new NiblackBinarization();
            algo.K = 3;
            algo.Apply(_input, _output);
            MainImg.Source = _output.ToSource();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_input is null) 
                return;
            var algo = new NiblackBinarization();
            algo.K = e.NewValue / 10;
            algo.Apply(_input, _output);
            MainImg.Source = _output.ToSource();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _vm.SampleText = "abc";
            _vm.SampleLength = 4;
        }

        private MainWindowVM _vm;
    }
}