using System.Drawing.Imaging;
using System.Linq;

namespace FalzoniNetFCSharp.Presentation.Administrator.Models.Common
{
    public class FileModel
    {
        public string FileName { get; set; }

        public string Base64String { get; set; }

        public ImageFormat Formato { get; set; }

        public void RemoveHeaderBase64()
        {
            var mimeType = Base64String.Split(',').FirstOrDefault();

            switch (mimeType)
            {
                case "data:image/jpeg;base64":
                    Formato = ImageFormat.Jpeg;
                    break;

                case "data:image/png;base64":
                    Formato = ImageFormat.Png;
                    break;
            }

            Base64String = System.Text.RegularExpressions.Regex.Replace(Base64String, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
        }

    }
}