using TaskItApi.Entities;

namespace TaskItApi.Handlers.Interfaces
{
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
    }
}
