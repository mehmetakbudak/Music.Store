using System;

namespace Music.Store.Domain.Models.User
{
    public class JwtTokenModel
    {
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
