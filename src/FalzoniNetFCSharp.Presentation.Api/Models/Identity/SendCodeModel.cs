namespace FalzoniNetFCSharp.Presentation.Api.Models.Identity
{
    public class SendCodeModel
    {
        public string UserId { get; set; }
        public string SelectedProvider { get; set; }
    }
}