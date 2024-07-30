namespace FalzoniNetFCSharp.Domain.DTO.Identity
{
    public class VerifyCodeDTO
    {
        public string UserId { get; set; }
        public string Provider { get; set; }
        public string ReturnUrl { get; set; }
        public string Code { get; set; }
    }
}
