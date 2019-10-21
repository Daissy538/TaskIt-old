using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskItApi.Entities;
using TaskItApi.Extentions;
using TaskItApi.Handlers.Interfaces;

namespace TaskItApi.Handlers
{
    public class TokenHandler: ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration config)
        {
            _configuration = config;
        }

        /// <summary>
        /// Create authentication token voor given user
        /// </summary>
        /// <param name="user">The user that want to login</param>
        /// <returns>The created token</returns>
        public string CreateAuthenticationToken(User user)
        {
            Claim[] claims = ClaimExtension.GenerateUserClaims(user.ID, user.Name);
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:AppSecret"]));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            IdentityModelEventSource.ShowPII = true;

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims, "jwt"),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Create invite token for a group invitation
        /// </summary>
        /// <param name="user">The user that is invited</param>
        /// <param name="group">The Group where the user is invited for</param>
        /// <returns>Return jwt token</returns>
        public string CreateInviteToken(User user, Group group)
        {
            Claim[] claims = ClaimExtension.GenerateInviteClaims(user.ID, group.ID);
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:AppSecret"]));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            IdentityModelEventSource.ShowPII = true;

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims, "jwt"),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
