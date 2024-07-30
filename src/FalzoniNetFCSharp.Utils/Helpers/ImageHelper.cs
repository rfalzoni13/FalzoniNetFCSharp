using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System;


namespace FalzoniNetFCSharp.Utils.Helpers
{
    public class ImageHelper
    {
        public static void SaveImageFile(string base64String, string path, string fileName, ImageFormat format)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            byte[] bytes = Convert.FromBase64String(base64String);

            using (var ms = new MemoryStream(bytes, 0, bytes.Length))
            {
                Image image = Image.FromStream(ms, true);

                image.Save($"{path}\\{fileName}", format);
            }
        }
    }
}
