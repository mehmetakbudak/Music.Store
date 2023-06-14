using Music.Store.Domain.Enums;

namespace Music.Store.Domain.Models.User
{
    public class AppUserModel
    {
        public int UserId { get; set; }

        public UserType UserType { get; set; }
    }
}
