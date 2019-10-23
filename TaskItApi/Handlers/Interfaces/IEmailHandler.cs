using TaskItApi.Dtos;
using TaskItApi.Entities;

namespace TaskItApi.Handlers.Interfaces
{
    /// <summary>
    /// Handler for sending and creating emails
    /// </summary>
    public interface IEmailHandler
    {
        /// <summary>
        /// Send invite email to user
        /// </summary>
        /// <param name="email">the email data</param>
        void SendInviteEmail(EmailDTO email);

        /// <summary>
        /// Create invite email
        /// </summary>
        /// <param name="recievingUser">User that recieves the invite</param>
        /// <param name="sendingUser">User that send the invite</param>
        /// <param name="group">The group where the reciever is invited for</param>
        /// <returns>The email data</returns>
        EmailDTO CreateInviteEmail(User recievingUser, User sendingUser, Group group);
    }
}
