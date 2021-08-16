namespace IdentityServerApi.Models
{
    public class ResetPassword
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
