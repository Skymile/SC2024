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
    public class Picture(Bitmap bmp, string filename = "Unnamed.png")
    {
        public string Filename => filename;
        public bool IsAutoUpdateOff { get; set; }

        public int LengthInBytes => _raw.Length;
        public int LengthInPixels => bmp.Width * bmp.Height;

        public int Width => bmp.Width;
        public int Height => bmp.Height;
        public int Channels => 3;

        public Picture(string filename) :
            this(new Bitmap(filename), filename) => Load();

        public Picture(Picture pic) : this(pic.bmp, pic.Filename) => Load();

        public byte this[int i]
        {
            get
            {
                Contract.Requires<IndexOutOfRangeException>(i >= 0 && i < _raw.Length);

                if (_isDirty && !IsAutoUpdateOff)
                    Update();
                return _raw[i];
            }
            set
            {
                Contract.Requires<IndexOutOfRangeException>(i >= 0 && i < _raw.Length);

                _raw[i] = value;
                _isDirty = true;
            }
        }
        
        public Color this[int x, int y]
        {
            get 
            {
                if (_isDirty && !IsAutoUpdateOff)
                    Update();
                int index = x * 3 + y * bmp.Width * 3;
                return Color.FromArgb(
                    _raw[index + 2],
                    _raw[index + 1], 
                    _raw[index + 0] 
                );
            }
            set 
            {
                _isDirty = true;
                int index = x * 3 + y * bmp.Width * 3;
                _raw[index + 2] = value.R;
                _raw[index + 1] = value.G;
                _raw[index + 0] = value.B;
            }
        }

        public System.Windows.Media.ImageSource ToSource()
        {
            Update();
            return bmp.ToBitmapSource();
        }

        private void Load()
        {
            var data = bmp.LockBits(
                new Rectangle(Point.Empty, bmp.Size),
                ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb
            );

            _raw = new byte[data.Width * data.Height * 3];
            Marshal.Copy(data.Scan0, _raw, 0, _raw.Length);

            bmp.UnlockBits(data);
        }

        public void Update()
        {
            var data = bmp.LockBits(
                new Rectangle(Point.Empty, bmp.Size),
                ImageLockMode.WriteOnly,
                PixelFormat.Format24bppRgb
            );

            Marshal.Copy(_raw, 0, data.Scan0, _raw.Length);

            bmp.UnlockBits(data);
        }

        private byte[] _raw = null!;
        private bool _isDirty;
        private readonly Bitmap bmp = bmp;
    }
}