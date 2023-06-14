namespace Music.Store.Domain.Models.User
{
    public class ResetPasswordModel
    {
        public string Code { get; set; }

        public string NewPassword { get; set; }

        public string ReNewPassword { get; set; }
    }
}
