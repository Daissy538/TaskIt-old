using TaskItApi.Entities;
using TaskItApi.Models.Interfaces;
using TaskItApi.Services.Interfaces;

namespace TaskItApi.Services
{
    public class GroupService: IGroupService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GroupService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<List<Group>> CreateAsync(Group newgroup, int userId)
        {
            var user = await this._unitOfWork.UserRepository.GetUserAsync(userId);

            if(user == default)
            {
                throw new NullReferenceException($"User with id {userId} doesn't exist.");
            }

            this._unitOfWork.GroupRepository.Create(newgroup);            
            
           await this._unitOfWork.SaveChangesAsync();

            return await this.GetGroupsAsync(userId);
        }

        public async Task<List<Group>> DeleteAsync(int groupId, int userId)
        {
            var group = await this._unitOfWork.GroupRepository.FindGroupOfUserAsync(groupId, userId);
            
            if(group == default)
            {
                throw new NullReferenceException($"Given group {groupId} not found for user {userId}");
            }

            this._unitOfWork.GroupRepository.Delete(group);
            await this._unitOfWork.SaveChangesAsync();
            return await this._unitOfWork.GroupRepository.FindAllGroupOfUserAsync(userId);
        }

        public async Task<Group?> GetGroup(int groupId, int userId)
        {
            return await this._unitOfWork.GroupRepository.FindGroupOfUserAsync(groupId, userId);
        }

        public async Task<List<Group>> GetGroupsAsync(int userId)
        {
            return await this._unitOfWork.GroupRepository.FindAllGroupOfUserAsync(userId);
        }

        public bool InviteUserToGroup(int sendingUserID, string emailAddress, int groupID)
        {
            throw new NotImplementedException();
        }

        public bool SubscribeToGroup(int userID, string token)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe(int userID, int groupID)
        {
            throw new NotImplementedException();
        }

        public async Task<Group> UpdateAsync(Group newGroupData, int userId)
        {
            var oldGroup = await this._unitOfWork.GroupRepository.FindGroupOfUserAsync(newGroupData.ID, userId);

            oldGroup.Description = newGroupData.Description;
            oldGroup.Name = newGroupData.Name;
            oldGroup.ColorID = newGroupData.ColorID;
            oldGroup.IconID = newGroupData.IconID;

            this._unitOfWork.GroupRepository.Update(oldGroup);
            await this._unitOfWork.SaveChangesAsync();

            return oldGroup;
        }
    }
}
