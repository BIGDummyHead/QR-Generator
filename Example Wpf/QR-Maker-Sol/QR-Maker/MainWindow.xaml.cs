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
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Reflection;

namespace QR_Maker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Custom Instance { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            CreateDefault();

            Instance = JsonConvert.DeserializeObject<Custom>(File.ReadAllText("color.json"));

            AddAllColors(qrWhiteColor);
            AddAllColors(qrBlackColor);

            colorGrid.Background = new SolidColorBrush(Instance.Background);
            Start();
        }

        public string Sep(string words)
        {
            StringBuilder builder = new();
            bool inc = false;
            foreach (char c in words)
            {
                if (char.IsUpper(c))
                {
                    if (!inc)
                        inc = true;
                    else
                        builder.Append(' ');
                }

                builder.Append(c);
            }

            return builder.ToString();
        }

        void AddAllColors(ComboBox box)
        {
            var colors = GetStaticPropertyBag(typeof(Colors));

            foreach (KeyValuePair<string, object> colorPair in colors)
            {
                Color color = (Color)colorPair.Value;
                string name = System.Drawing.ColorTranslator.FromHtml(color.ToString()).Name;
                box.Items.Add(Sep(name));
            }
        }

        const BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        public static Dictionary<string, object> GetStaticPropertyBag(Type t)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();

            foreach (var prop in t.GetProperties(flags))
            {
                map[prop.Name] = prop.GetValue(null, null);
            }

            return map;
        }

        private string jsonFile = "color.json";
        private void CreateDefault()
        {
            if (File.Exists(jsonFile))
                return;

            File.Create(jsonFile).Close();

            Custom custom = new Custom
            {
                Background = Colors.SlateGray,
            };

            string json = JsonConvert.SerializeObject(custom, Formatting.Indented);

            File.WriteAllText(jsonFile, json);
        }

        public string[] RandomContent = new string[] { "Hello world!", "https://github.com/bigdummyhead/", "https://tinyurl.com/6hmrkypn", "You found a secret", "Something is wrong I can feel it" };

        private async void Start()
        {
            string con = RandomContent[new Random().Next(0, RandomContent.Length)];
            content.Text = con;
            await GenerateQR(con);
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

        public static WriteableBitmap StreamCopy { get; private set; }

        public async Task GenerateQR(string content)
        {
            Code = new QR(content);
            byte[] data = await Code.AsBytesAsync();

            StreamCopy = GetImage(data);

            qr.Source = StreamCopy;
        }

        WriteableBitmap GetImage(byte[] data)
        {
            BitmapImage img = new BitmapImage();
            WriteableBitmap bit = null;
            using (MemoryStream stream = new MemoryStream(data))
            {
                stream.Position = 0;

                img.BeginInit();
                img.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.UriSource = null;
                img.StreamSource = stream;
                img.EndInit();
                img.Freeze();

                bit = BitmapFactory.ConvertToPbgra32Format(img);

                using (bit.GetBitmapContext())
                {
                    for (int w = 0; w < img.PixelWidth; w++)
                    {
                        for (int h = 0; h < img.PixelHeight; h++)
                        {
                            Color pixel = bit.GetPixel(w, h);

                            if (Color.AreClose(pixel, Colors.Black))
                                bit.SetPixel(w, h, GetOptColor(qrBlackColor, Colors.Black));
                            else if (Color.AreClose(pixel, Colors.White))
                                bit.SetPixel(w, h, GetOptColor(qrWhiteColor, Colors.White));
                        }
                    }
                }
            }

            return bit;
        }

        private object GetSelected(ComboBox box, object defaultValue)
        {
            int index = box.SelectedIndex;

            if (index == -1)
                return defaultValue;

            return box.Items[index];
        }

        private Color GetOptColor(ComboBox box, Color def)
        {
            string name = GetSelected(box, "NONE") as string;

            if (name == "NONE")
                return def;

            var col = System.Drawing.ColorTranslator.FromHtml(name.Replace(" ", ""));

            return Color.FromArgb(col.A, col.R, col.G, col.B);
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

        private void EnterValue(object sender, System.Windows.Input.KeyEventArgs e)
        {
            bool enter = e.Key == System.Windows.Input.Key.Enter;
            if (enter)
                Button_Click(null, null);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DownloadCode();
        }
        //
        private void DownloadCode()
        {
            string output = Path.GetTempFileName();
            output = Path.GetFileNameWithoutExtension(output).Replace("tmp", "qr") + ".png";

            using (FileStream stream = File.Create(output))
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(StreamCopy));
                encoder.Save(stream);
            }
        }
    }

    public struct Custom
    {
        //public Color QRBlack { get; set; }
        //public Color QRWhite { get; set; }
        public Color Background { get; set; }
    }
}
