namespace FalzoniNetFCSharp.Presentation.Administrator.Models.Identity
{
    public class VerifyCodeModel
    {
        public string Provider { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
}