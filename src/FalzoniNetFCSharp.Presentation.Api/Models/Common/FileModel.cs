using System.Drawing.Imaging;

namespace FalzoniNetFCSharp.Presentation.Api.Models.Common
{
    public class FileModel
    {
        public string FileName { get; set; }

        public string Base64String { get; set; }

        public ImageFormat Format { get; set; }

    }
}