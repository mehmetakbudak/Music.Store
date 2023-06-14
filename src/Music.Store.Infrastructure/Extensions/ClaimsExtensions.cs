using Music.Store.Domain.Consts;
using Music.Store.Domain.Models.User;
using Music.Store.Infrastructure.Exceptions;
using System;
using System.Linq;
using System.Security.Claims;

namespace Music.Store.Infrastructure.Extensions
{
    public static class ClaimsExtensions
    {
        public static int UserId(this System.Security.Claims.ClaimsPrincipal principal)
        {
            var userIdClaim = principal.Claims.FirstOrDefault(x => x.Type == JwtTokenPayload.UserId);

            if (userIdClaim == null || !Int32.TryParse(userIdClaim.Value, out int userId))
            {
                throw new NotFoundException("Kullanıcı bulunamadı.");
            }
            return userId;
        }

        public static UserClaimsModel Parse(this ClaimsPrincipal claims)
        {
            var model = new UserClaimsModel();

            var _userId = claims.FindFirst(x => x.Type == "UserId");
            if (_userId != null)
            {
                Int32.TryParse(_userId.Value, out int userId);
                model.UserId = userId;
            }
            var _fullFullName = claims.FindFirst(x => x.Type == "FullName");
            if (_fullFullName != null)
            {
                model.FullName = _fullFullName.Value;
            }
            var _name = claims.FindFirst(x => x.Type == "Name");
            if (_name != null)
            {
                model.Name = _name.Value;
            }
            var _surname = claims.FindFirst(x => x.Type == "Surname");
            if (_surname != null)
            {
                model.Surname = _surname.Value;
            }
            var _userType = claims.FindFirst(x => x.Type == "UserType");
            if (_userType != null)
            {
                Int32.TryParse(_userType.Value, out int userType);
                model.UserType = userType;
            }
            return model;
        }
    }
}
