using System;

namespace Music.Store.Domain.Models.User
{
    public class TokenReponseModel
    {
        public int UserId { get; set; }
        public int UserType { get; set; }
        public string NameSurname { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpireDate { get; set; }
    }
}
