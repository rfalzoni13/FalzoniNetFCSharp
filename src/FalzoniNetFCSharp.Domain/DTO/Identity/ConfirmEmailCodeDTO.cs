namespace FalzoniNetFCSharp.Domain.DTO.Identity
{
    public class ConfirmEmailCodeDTO
    {
        public string UserId { get; set; }
        public string Code { get; set; }
        public string CallBackUrl { get; set; }
    }
}
