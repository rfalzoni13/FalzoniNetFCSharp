using System.Drawing.Imaging;

namespace FalzoniNetFCSharp.Domain.DTO.Common
{
    public class FileDTO
    {
        public string FileName { get; set; }

        public string Base64String { get; set; }

        public ImageFormat Format { get; set; }
    }
}
