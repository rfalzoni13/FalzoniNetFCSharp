namespace FalzoniNetFCSharp.Domain.DTO.Identity
{
    public class ConfirmPhoneCodeDTO
    {
        public string UserId { get; set; }
        public string Phone { get; set; }
        public string Code { get; set; }
    }
}
