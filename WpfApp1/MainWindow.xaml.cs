using System.Reflection;
using System.Windows;
using System.Windows.Controls;

using WpfApp1.Algorithms;

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
        private const string _defaultFilename = "C:/Samples/apple.png";
        private Picture _input;
        private Picture _output;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _vm = new MainWindowVM();

            _input  = new Picture(_defaultFilename);
            _output = new Picture(_defaultFilename);
            MainImg.Source = _output.ToSource();

            foreach (var type in Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(i => !i.IsAbstract && i.IsSubclassOf(typeof(AlgorithmBase))))
            {
                string name = type.Name
                    .Replace("Thinning", string.Empty)
                    .Replace("Binarization", string.Empty);

                var btn = new Button { Content = name };
                var algorithm = (AlgorithmBase)Activator.CreateInstance(type)!;

                btn.Click += (sender, e) =>
                {
                    algorithm.Apply(_input, _output);
                    MainImg.Source = _output.ToSource();
                };

                StkPanel.Children.Add(btn);

                var properties = type.GetProperties();

                foreach (var prop in properties)
                {
                    var slider = new Slider();
                    slider.Value = 2;
                    slider.Minimum = 0;
                    slider.Maximum = 255;
                    slider.ValueChanged += (sender, e) =>
                    {
                        StatusLbl.Content = $"{prop.Name}: {Math.Round(slider.Value)}";

                        prop.SetValue(algorithm,
                            Convert.ChangeType(slider.Value, prop.PropertyType)
                        );
                    };

                    StkPanel.Children.Add(new Label { Content = prop.Name });
                    StkPanel.Children.Add(slider);
                }
            }
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