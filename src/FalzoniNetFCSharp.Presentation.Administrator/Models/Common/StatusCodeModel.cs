using System.Net;

namespace FalzoniNetFCSharp.Presentation.Administrator.Models.Common
{
    public class StatusCodeModel
    {
        public virtual HttpStatusCode Status { get; set; }

        public string Message { get; set; }
    }
}