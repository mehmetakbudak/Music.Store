namespace Music.Store.Domain.Models.User
{
    public class ChangePasswordModel
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ReNewPassword { get; set; }
    }
}
