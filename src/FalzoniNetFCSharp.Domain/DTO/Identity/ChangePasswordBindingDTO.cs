namespace FalzoniNetFCSharp.Domain.DTO.Identity
{
    public class ChangePasswordBindingDTO
    {
        public string UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
