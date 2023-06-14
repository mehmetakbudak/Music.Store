﻿using Microsoft.IdentityModel.Tokens;
using Music.Store.Domain.Entities;
using Music.Store.Domain.Models.User;
using Music.Store.Infrastructure.Exceptions;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Music.Store.Infrastructure.Helpers
{
    public interface IJwtHelper
    {
        JwtTokenModel GenerateJwtToken(User user);
        JwtSecurityToken ValidateToken(string token);
    }

    public class JwtHelper : IJwtHelper
    {
        public JwtTokenModel GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Global.Secret);
            var expireDate = DateTime.Now.AddHours(2);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("UserType", user.UserType.ToString()),
                    new Claim("FirstName", user.FirstName),
                    new Claim("LastName", user.LastName),
                    new Claim("EmailAddress", user.EmailAddress)
                }),
                Expires = expireDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            var model = new JwtTokenModel
            {
                Token = token,
                ExpireDate = expireDate
            };
            return model;
        }

        public JwtSecurityToken ValidateToken(string token)
        {
            try
            {
                JwtSecurityToken jwtToken = null;
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(Global.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                if (validatedToken != null)
                {
                    jwtToken = (JwtSecurityToken)validatedToken;
                }
                else
                {
                    throw new BadRequestException("Token doğrulanamadı.");
                }
                return jwtToken;
            }
            catch (Exception)
            {
                throw new UnAuthorizedException("Token doğrulanamadı.");
            }
        }
    }
}
