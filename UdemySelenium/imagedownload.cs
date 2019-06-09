using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UdemySelenium
{
    internal class imagedownload
    {
        public class DownloadImage
        {
            private string imageUrl;
            private Bitmap bitmap;
            public DownloadImage(string imageUrl)
            {
                this.imageUrl = imageUrl;
            }
            public void Download()
            {
                try
                {
                    WebClient client = new WebClient();
                    Stream stream = client.OpenRead(imageUrl);
                    bitmap = new Bitmap(stream);
                    stream.Flush();
                    stream.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            public Bitmap GetImage()
            {
                return bitmap;
            }
            public void SaveImage(string filename, ImageFormat format)
            {
                if (bitmap != null)
                {
                    bitmap.Save(filename, format);
                }
            }
        }
    }
}
