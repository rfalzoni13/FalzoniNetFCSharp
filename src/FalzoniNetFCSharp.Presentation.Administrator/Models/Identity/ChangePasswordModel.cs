namespace FalzoniNetFCSharp.Presentation.Administrator.Models.Identity
{
    public class ChangePasswordModel
    {
        public string UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}