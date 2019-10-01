using TaskItApi.Dtos;

namespace TaskItApi.Services.Interfaces
{
    /// <summary>
    /// Interface for managing the users
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Delete user by userId
        /// </summary>
        /// <param name="Id">the user Id</param>
        /// <returns></returns>
        bool DeleteUser(int Id);
        /// <summary>
        /// Update the user with the given user Id
        /// </summary>
        /// <param name="Id">the user to be updated</param>
        /// <param name="changedUserInComingData">The changed data</param>
        void UpdateUser(int Id, UserInComingDto changedUserInComingData);
    }
}
