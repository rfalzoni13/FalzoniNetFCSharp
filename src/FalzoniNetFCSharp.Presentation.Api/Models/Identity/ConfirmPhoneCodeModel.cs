namespace FalzoniNetFCSharp.Presentation.Api.Models.Identity
{
    public class ConfirmPhoneCodeModel
    {
        public string UserId { get; set; }
        public string Code { get; set; }
        public string Phone { get; set; }
    }
}