using TaskItApi.Entities;

namespace TaskItApi.Handlers.Interfaces
{
    /// <summary>
    /// Handler for creating, validating and reading JWT tokens
    /// https://jwt.io/
    /// </summary>
    public interface ITokenHandler
    {
        /// <summary>
        /// Create authentication token voor given user
        /// </summary>
        /// <param name="user">The user that want to login</param>
        /// <returns>The created token</returns>
        string CreateAuthenticationToken(User user);
        
        /// <summary>
        /// Create invite token for a group invitation
        /// </summary>
        /// <param name="user">The user that is invited</param>
        /// <param name="group">The Group where the user is invited for</param>
        /// <returns>Return jwt token</returns>
        string CreateInviteToken(User user, Group group);

        /// <summary>
        /// Validate subscribe token
        /// Method checks if the user is authorized to subscribe on the given group.
        /// </summary>
        /// <param name="token">the subscribe token</param>
        /// <param name="user">the active user</param>
        /// <returns>true if the user is authorized to subscribe, false otherwise</returns>
        bool ValidateSubscribeToken(string token, User user);

        /// <summary>
        /// Get group id out of token
        /// </summary>
        /// <param name="token">The token</param>
        /// <returns>The group ID</returns>
        int GetGroupID(string token);
    }
}
