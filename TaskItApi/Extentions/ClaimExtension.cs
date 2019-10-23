using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace TaskItApi.Extentions
{
    public static class ClaimExtension
    {
        private readonly static string userIdClaim = ClaimTypes.NameIdentifier;
        private readonly static string userNameclaim = ClaimTypes.Name;
        private readonly static string userDataclaim = ClaimTypes.UserData;

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

        /// <summary>
        /// Get the userID from the claims
        /// </summary>
        /// <param name="claims">The list of claims</param>
        /// <returns>the user id</returns>
        public static int GetCurrentUserId(this IEnumerable<Claim> claims)
        {           
            string userId = claims.Where(c => c.Type == userIdClaim).FirstOrDefault().Value;

            return Convert.ToInt32(userId);
        }

        /// <summary>
        /// Get the user data from the claims
        /// </summary>
        /// <param name="claims">the list of claims</param>
        /// <returns>the user data</returns>
        public static int GetUserDataClaim(this IEnumerable<Claim> claims)
        {
            string userId = claims.Where(c => c.Type == userDataclaim).FirstOrDefault().Value;

            return Convert.ToInt32(userId);
        }

        /// <summary>
        /// Generate claims used for authentication
        /// </summary>
        /// <param name="userId">the active user id</param>
        /// <param name="userName">the active username</param>
        /// <returns>the list of claims</returns>
        public static Claim[] GenerateUserClaims(int userId, string userName)
        {
            Claim[] claims = new Claim[]
                {
                new Claim(userIdClaim, userId.ToString()),
                new Claim(userNameclaim, userName)
                };

            return claims;
        }

        /// <summary>
        /// Generate claims for invite token
        /// </summary>
        /// <param name="userID">the user to be invited</param>
        /// <param name="groupID">the group to be invited for</param>
        /// <returns>The list of claims</returns>
        public static Claim[] GenerateInviteClaims(int userID, int groupID)
        {
            Claim[] claims = new Claim[]
                {
                new Claim(userIdClaim, userID.ToString()),
                new Claim(userDataclaim, groupID.ToString())
                };

            return claims;
        }
    }
}
