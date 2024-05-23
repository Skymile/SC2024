using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

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
    public class Algorithm
    {
        public Bitmap ApplyBinarization(Bitmap read, Bitmap write, int threshold)
        {
            const int channels = 3;

            var readData = read.LockBits(
                new Rectangle(Point.Empty, read.Size),
                ImageLockMode.ReadOnly, 
                read.PixelFormat);
            var writeData = write.LockBits(
                new Rectangle(Point.Empty, read.Size),
                ImageLockMode.WriteOnly,
                read.PixelFormat);

            int length = readData.Width * channels * readData.Height;
            byte[] r = new byte[length];
            byte[] w = new byte[length];

            Marshal.Copy(readData.Scan0, r, 0, r.Length);

            for (int i = 0; i < r.Length; i += channels)
            {
                int sum = 0;
                for (int j = 0; j < channels; ++j)
                    sum += r[i + j];
                sum /= channels;

                byte value = sum > threshold
                    ? byte.MaxValue
                    : byte.MinValue;

                for (int j = 0; j < channels; j++)
                    w[i + j] = value;
            }

            Marshal.Copy(w, 0, writeData.Scan0, r.Length);
            
            read.UnlockBits(readData);
            write.UnlockBits(writeData);
            
            return write;
        }
    }
}