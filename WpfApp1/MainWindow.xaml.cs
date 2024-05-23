using System.ComponentModel;
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
    public class MainWindowVM : ViewModel
    {

    }

    public abstract class ViewModel
    {

    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string SampleText 
        { 
            get => sampleText;
            set 
            {
                sampleText = value;
                PropertyChanged?.Invoke(this, new(nameof(SampleText)));
            } 
        }
        private string sampleText;

        public int SampleLength
        {
            get => sampleLength;
            set
            {
                sampleLength = value;
                PropertyChanged?.Invoke(this, new(nameof(SampleLength)));
            }
        }
        private int sampleLength;

        public event PropertyChangedEventHandler? PropertyChanged;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SampleText = "abc";
            SampleLength = 4;
        }
    }
}