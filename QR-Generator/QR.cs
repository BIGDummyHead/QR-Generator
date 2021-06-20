using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace QRGenerator
{
    /// <summary>
    /// A QR Code
    /// </summary>
    public sealed class QR
    {
        private string request;

        /// <summary>
        /// Basic QR generation
        /// </summary>
        /// <param name="content">The content to display of the QR code</param>
        /// <param name="width">The width of the QR code image</param>
        /// <param name="height">The height of the QR code image</param>
        public QR(string content, int width = 500, int height = 500)
        {
            Width = width;
            Height = height;
            Content = content ?? throw new ArgumentNullException(nameof(content));

            this.request = $"https://api.qrserver.com/v1/create-qr-code/?size={width}x{height}&data={content}";
        }

        /// <summary>
        /// The width of the QR code image
        /// </summary>
        public int Width { get; init; }
        /// <summary>
        /// The height of the QR code image
        /// </summary>
        public int Height { get; init; }
        /// <summary>
        /// The content to display of the QR code image
        /// </summary>
        public string Content { get; init; }

        /// <summary>
        /// After <see cref="AsBytes"/> or <seealso cref="AsBytesAsync"/> is called, this is returned.
        /// </summary>
        public byte[] ReserverdBytes { get; private set; } = null;

        /// <summary>
        /// Turns the <see cref="HttpResponseMessage"/> into a <seealso cref="byte"/>[] | Async
        /// </summary>
        /// <returns></returns>
        public async Task<byte[]> AsBytesAsync()
        {
            if (ReserverdBytes != null)
                return ReserverdBytes;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage msg = await client.GetAsync(request);

                ReserverdBytes = await msg.Content.ReadAsByteArrayAsync();

                return ReserverdBytes;
            }
        }

        /// <summary>
        /// Turns the <see cref="HttpResponseMessage"/> into a <seealso cref="byte"/>[] 
        /// </summary>
        /// <returns>QR code image as <see cref="byte"/>[]</returns>
        public byte[] AsBytes() => AsBytesAsync().GetAwaiter().GetResult();

        /// <summary>
        /// Download the QR code image, as a file | Async
        /// </summary>
        /// <param name="output">Where to write the file</param>
        /// <param name="overwrite">Overwrite existing file - Set to false by default</param>
        public async Task DownloadAsync(string output, bool overwrite = false)
        {
            if (File.Exists(output))
            {
                if (overwrite)
                {
                    File.Delete(output);
                }
                else
                    return;
            }

            File.Create(output).Close();

            byte[] data = await AsBytesAsync();

            await File.WriteAllBytesAsync(output, data);
        }

        /// <summary>
        /// Download the QR code image, as a file
        /// </summary>
        /// <param name="output">Where to write the file</param>
        /// <param name="overwrite">Overwrite existing file - Set to false by default</param>
        public void Download(string output, bool overwrite = false) => DownloadAsync(output, overwrite).GetAwaiter().GetResult();

        /// <summary>
        /// Create a QR code via implicit content
        /// </summary>
        /// <param name="content"></param>
        public static implicit operator QR(string content)
        {
            return new QR(content);
        }
    }
}
