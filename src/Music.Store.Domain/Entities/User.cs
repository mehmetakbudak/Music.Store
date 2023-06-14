using Music.Store.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Music.Store.Domain.Entities
{
    public class User : EntityBase
    {
        [Required, MaxLength(20)]
        public string LastName { get; set; }

        [Required, MaxLength(20)]
        public string FirstName { get; set; }

        public UserType UserType { get; set; }

        public UserStatus UserStatus { get; set; }

        [MaxLength(60)]
        public string EmailAddress { get; set; }

        [MaxLength(20)]
        public string Password { get; set; }

        [MaxLength(24)]
        public string Phone { get; set; }       

        public bool IsActive { get; set; }

        public bool Deleted { get; set; }

        public string Token { get; set; }

        public string HashCode { get; set; }

        public DateTime? TokenExpireDate { get; set; }

        public DateTime? PasswordExpireDate { get; set; }

        public DateTime InsertedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

    }
}
