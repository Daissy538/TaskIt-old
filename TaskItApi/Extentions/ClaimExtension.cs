using System;
using System.Linq;
using System.Security.Claims;

namespace TaskItApi.Extentions
{
    public static class ClaimExtension
    {
        private readonly static string userIdClaim = ClaimTypes.NameIdentifier;
        private readonly static string userNameclaim = ClaimTypes.Name;

        /// <summary>
        /// Get the user id from the ClaimsPrincipal
        /// </summary>
        /// <param name="user">The claimPrincinpal that repecence the active user</param>
        /// <returns>THe user id</returns>
        public static int GetCurrentUserId(this ClaimsPrincipal user)
        {
            ClaimsIdentity claims = user.Identity as ClaimsIdentity;
            string userId = claims.Claims.Where(c => c.Type == userIdClaim).FirstOrDefault().Value;

            return Convert.ToInt32(userId);
        }

        public static Claim[] GenerateUserClaims(int userId, string userName)
        {
            Claim[] claims = new Claim[]
                {
                new Claim(userIdClaim, userId.ToString()),
                new Claim(userNameclaim, userName)
                };

            return claims;
        }
    }
}
