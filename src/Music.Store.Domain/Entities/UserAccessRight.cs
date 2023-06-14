namespace Music.Store.Domain.Entities
{
    public class UserAccessRight : EntityBase
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int AccessRightId { get; set; }

        public AccessRight AccessRight { get; set; }
    }
}
