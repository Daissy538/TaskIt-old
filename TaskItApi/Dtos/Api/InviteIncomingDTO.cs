
namespace TaskItApi.Dtos.Api
{
    /// <summary>
    /// Incoming data to invite a user to  a group
    /// </summary>
    public class InviteIncomingDTO
    {
        /// <summary>
        /// The email of the recieving user
        /// </summary>
        public string RecievingMail { get; set; }
    }
}
