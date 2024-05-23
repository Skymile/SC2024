using System.Windows.Media;

namespace WpfApp1;

public class MainWindowVM : ViewModel
{
    public int     SampleLength { get => Get<int>(); set => Set(value); }
    public string? SampleText   { get => Get<string>(); set => Set(value); }

    public ImageSource ImgPath { get; set; }
}