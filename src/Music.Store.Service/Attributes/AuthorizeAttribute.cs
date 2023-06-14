using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Music.Store.Domain.Consts;
using Music.Store.Domain.Enums;
using Music.Store.Infrastructure;
using Music.Store.Infrastructure.Exceptions;
using Music.Store.Service.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Store.Service.Attributes
{
    public class AuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public bool CheckAccessRight { get; set; }

        public AuthorizeAttribute()
        {
            CheckAccessRight = true;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userService = (IUserService)context.HttpContext.RequestServices.GetService(typeof(IUserService));
            var userAccessRightService = (IUserAccessRightService)context.HttpContext.RequestServices.GetService(typeof(IUserAccessRightService));
            var httpContextAccessor = (IHttpContextAccessor)context.HttpContext.RequestServices.GetService(typeof(IHttpContextAccessor));

            #region authentication
            var request = context.HttpContext.Request;
            var token = request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(token))
            {
                throw new NotFoundException("Token bulunamadı.");
            }

            string tokenString = token.Split(' ')[1];

            if (string.IsNullOrEmpty(tokenString))
            {
                throw new BadRequestException("Token formatı uygun değil.");
            }

            var key = Encoding.ASCII.GetBytes(Global.Secret);

            var handler = new JwtSecurityTokenHandler();

            try
            {
                handler.ValidateToken(tokenString, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken securityToken);
            }
            catch
            {
                throw new UnAuthorizedException("Kimlik doğrulanamadı.");
            }

            var tokenDescrypt = handler.ReadJwtToken(tokenString);

            var strUserId = tokenDescrypt.Claims.FirstOrDefault(x => x.Type == JwtTokenPayload.UserId);

            if (strUserId == null || !Int32.TryParse(strUserId.Value, out int userId))
            {
                throw new BadRequestException("Token hatalı.");
            }

            var user = await userService.GetDmo(userId);

            if (user == null)
            {
                throw new NotFoundException("Kullanıcı bulunamadı.");
            }

            if (!string.IsNullOrEmpty(user.Token))
            {
                if (user.Token.ToLower() != tokenString.ToLower())
                {
                    throw new UnAuthorizedException("Token süresi dolmuş. Tekrar giriş yapınız.");
                }
            }
            else
            {
                throw new UnAuthorizedException("Lütfen giriş yapınız.");
            }

            if (!user.TokenExpireDate.HasValue)
            {
                throw new UnAuthorizedException("Token süresi dolmuş. Tekrar giriş yapınız.");
            }

            var tokenStartDate = user.TokenExpireDate.Value.AddHours(-2);
            var tokenEndDate = user.TokenExpireDate.Value;

            if (!((tokenStartDate <= DateTime.Now) && (tokenEndDate >= DateTime.Now)))
            {
                throw new UnAuthorizedException("Token süresi dolmuş. Tekrar giriş yapınız.");
            }
            #endregion

            #region authorization
            if (CheckAccessRight)
            {
                if (user.UserType == UserType.Member)
                {
                    throw new UnAuthorizedException("Yetkisiz İşlem.");
                }
                if (user.UserType != UserType.SuperAdmin)
                {
                    var pathes = context.HttpContext.Request.Path.Value.Split("/");
                    var method = context.HttpContext.Request.Method; 
                    var check = await userAccessRightService.CheckAccessRight(userId, pathes, method);
                    if(!check)
                    {
                        throw new UnAuthorizedException("Yetkisiz İşlem.");
                    }
                }
            }
            #endregion
        }
    }

}
