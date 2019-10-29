
namespace TaskItApi.Dtos.Api.Outgoing
{
    /// <summary>
    /// The holder of a task
    /// </summary>
    public class TaskHolderOutgoingDTO
    {
        /// <summary>
        /// Holder id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The user id
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// The username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// The email of the user
        /// </summary>
        public string Email { get; set; }
    }
}
