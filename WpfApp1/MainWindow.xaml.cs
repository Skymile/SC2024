using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
    public static class BitmapExtensions
    {
        public static ImageSource ToBitmapSource(this Bitmap bmp)
        {
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            BitmapImage image = new BitmapImage();
            image.BeginInit();

            ms.Seek(0, SeekOrigin.Begin);

            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = _vm = new MainWindowVM();

            var bmp = new Bitmap("C:/Samples/apple.png");

            MainImg.Source = bmp.ToBitmapSource();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _vm.SampleText = "abc";
            _vm.SampleLength = 4;
        }

        private MainWindowVM _vm;
    }
}