namespace FalzoniNetFCSharp.Presentation.Api.Models.Identity
{
    public class RemoveLoginBindingModel
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
    }
}