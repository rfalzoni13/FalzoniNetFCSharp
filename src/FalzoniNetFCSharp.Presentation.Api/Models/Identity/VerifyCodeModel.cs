namespace FalzoniNetFCSharp.Presentation.Api.Models.Identity
{
    public class VerifyCodeModel
    {
        public string UserId { get; set; }
        public string Code { get; set; }
        public string Provider { get; set; }
        public string ReturnUrl { get; set; }

    }
}