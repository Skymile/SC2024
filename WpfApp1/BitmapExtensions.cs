using System.Drawing;
using System.IO;
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
}