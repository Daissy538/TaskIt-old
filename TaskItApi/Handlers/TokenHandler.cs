using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
            SigningCredentials credentials = CreateSigningCredentials();

            IdentityModelEventSource.ShowPII = true;

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims, "jwt"),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials,
                Issuer = user.ID.ToString(),
                Audience = user.ID.ToString()
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Validate subscribe token
        /// Method checks if the user is authorized to subscribe on the given group.
        /// </summary>
        /// <param name="token">the subscribe token</param>
        /// <param name="user">the active user</param>
        /// <returns>true if the user is authorized to subscribe, false otherwise</returns>
        public bool ValidateSubscribeToken(string token, User user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SigningCredentials credentials = CreateSigningCredentials();

            TokenValidationParameters validationParameters =
            new TokenValidationParameters
            {
                ValidIssuer = user.ID.ToString(),
                ValidAudience = user.ID.ToString(),
                IssuerSigningKey = credentials.Key,                
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true
            };

            SecurityToken tokenValidated;

            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out tokenValidated);
            }
            catch (Exception exception)
            {                
                return false;
            }          
            
            return true;
        }

        /// <summary>
        /// Get group id out of token
        /// </summary>
        /// <param name="token">The token</param>
        /// <returns>The group ID</returns>
        public int GetGroupID(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtSecurityToken = tokenHandler.ReadJwtToken(token);

            int groupID = jwtSecurityToken.Claims.GetUserDataClaim();

            return groupID;
        }

        private SigningCredentials CreateSigningCredentials()
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:AppSecret"]));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            return credentials;
        }
    }
}
