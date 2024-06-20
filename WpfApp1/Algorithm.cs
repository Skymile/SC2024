using System.Drawing;
using System.Windows.Automation.Peers;
using System.Windows.Controls.Primitives;

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
    public class PhansalkarBinarization : NiblackBinarization 
    {
        public override double Formulae(double std, double mean) =>
            std;
    }

    public class SauvolaBinarization    : NiblackBinarization 
    {
        public override double Formulae(double std, double mean) =>
            std;
    }

    public class NiblackBinarization : AlgorithmBase
    {
        public double K { get; set; } = 0.2;
        public int WindowSize { get; set; } = 3;

        public override void Apply(Picture read, Picture write)
        {
            int wndHalf = WindowSize / 2;

            for (int x = wndHalf; x < read.Width - wndHalf; x++)
            {
                for (int y = wndHalf; y < read.Height - wndHalf; y++)
                {
                    double mean = 0;

                    for (int mx = -wndHalf; mx < wndHalf; ++mx)
                        for (int my = -wndHalf; my < wndHalf; my++)
                        {
                            var c = read[x + mx, y + my];
                            mean += (c.R + c.G + c.B) / 3;
                        }

                    mean /= WindowSize * WindowSize;

                    double std = 0;

                    for (int mx = -wndHalf; mx < wndHalf; ++mx)
                        for (int my = -wndHalf; my < wndHalf; my++)
                        {
                            var c = read[x + mx, y + my];
                            int rgb = (c.R + c.G + c.B) / 3;
                            std += Math.Sqrt(rgb * rgb - mean * mean);
                        }

                    std /= WindowSize * WindowSize;

                    var value = Formulae(std, mean) < read[x, y].R ? 255 : 0;

                    write[x, y] = Color.FromArgb(value, value, value);
                }
            }
        }

        public virtual double Formulae(double std, double mean) =>
            K * std + mean;
    }

    public class ThresholdBinarization : AlgorithmBase
    {
        public required int Threshold { get; set; }

        public override void Apply(Picture pic)
        {
            const int channels = 3;

            for (int i = 0; i < pic.LengthInBytes; i += channels)
            {
                int sum = 0;
                for (int j = 0; j < channels; ++j)
                    sum += pic[i + j];
                sum /= channels;

                byte value = sum > Threshold
                    ? byte.MaxValue
                    : byte.MinValue;

                for (int j = 0; j < channels; j++)
                    pic[i + j] = value;
            }
        }
    }
}