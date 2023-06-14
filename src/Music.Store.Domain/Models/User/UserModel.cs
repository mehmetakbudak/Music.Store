using System;

namespace Music.Store.Domain.Models.User
{
    public class UserModel
    {
        public int Id { get; set; }
        public int UserType { get; set; }
        public string UserTypeName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public int UserStatus { get; set; }
        public string UserStatusName { get; set; }
        public DateTime InsertedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
