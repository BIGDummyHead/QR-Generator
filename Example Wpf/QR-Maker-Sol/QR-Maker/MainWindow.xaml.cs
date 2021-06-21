using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using QRGenerator;

namespace QR_Maker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Start();
        }


        private async void Start()
        {
            await GenerateQR("Hello world");
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(content.Text))
            {
                MessageBox.Show("You can not generate an empty QR Code!", "Empty QR Code", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            await GenerateQR(content.Text);
        }

        public QR Code { get; private set; }
        public async Task GenerateQR(string content)
        {
            Code = new QR(content);
            byte[] data = await Code.AsBytesAsync();
            qr.Source = GetImage(data);
        }

        BitmapImage GetImage(byte[] data)
        {
            BitmapImage img = new BitmapImage();

            using (MemoryStream stream = new MemoryStream(data))
            {
                stream.Position = 0;

                img.BeginInit();
                img.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.UriSource = null;
                img.StreamSource = stream;
                img.EndInit();
            }

            img.Freeze();
            return img;
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

        private void window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            content.Width = e.NewSize.Width - 116;

        }

        private async void EnterValue(object sender, System.Windows.Input.KeyEventArgs e)
        {
            bool enter = e.Key == System.Windows.Input.Key.Enter;

            if (enter)
                await GenerateQR(content.Text);
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string filePath = Path.GetTempFileName();
            filePath = Path.GetFileNameWithoutExtension(filePath);
            filePath = filePath.Replace("tmp", "qr");

            await Code.DownloadAsync(filePath + ".jpg", true);
        }

    }
}
