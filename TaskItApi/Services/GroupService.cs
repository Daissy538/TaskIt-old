using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskItApi.Dtos;
using TaskItApi.Dtos.Api;
using TaskItApi.Entities;
using TaskItApi.Exceptions;
using TaskItApi.Handlers.Interfaces;
using TaskItApi.Models.Interfaces;
using TaskItApi.Services.Interfaces;

namespace TaskItApi.Services
{
    public class GroupService: IGroupService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IEmailHandler _emailHandler;
        private readonly ITokenHandler _tokenHandler;

        public GroupService(IMapper mapper, IUnitOfWork unitOfWork, ILogger<IGroupService> logger, IEmailHandler emailHandler, ITokenHandler tokenHandler)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _emailHandler = emailHandler;
            _tokenHandler = tokenHandler;
        }

        /// <summary>
        /// Create a group by an user
        /// </summary>
        /// <param name="groupDto">The group details</param>
        /// <param name="userId">The user that create the group</param>
        /// <returns>The current subscripted groups of the user</returns>
        public IEnumerable<Group> Create(GroupIncomingDTO groupDto, int userId)
        { 
            if(!UserExist(userId))
            {
                _logger.LogError($"Couldn't create group for non-existing user {userId}");
                throw new NullReferenceException("Couldn't create group for non-existing user");
            }

            Group group = _mapper.Map<Group>(groupDto);

            Subscription subscription = new Subscription();

            subscription.UserID = userId;
            subscription.Group = group;
            subscription.DateOfSubscription = DateTime.UtcNow;
            
            try
            {
                _unitOfWork.GroupRepository.Create(group);
                _unitOfWork.SubscriptionRepository.Create(subscription);
                _unitOfWork.SaveChanges();           
            } catch(Exception exception)
            {
                _logger.LogError($"Could not create group: {groupDto.Name} by user:{userId} error: {exception.Message}");
                throw exception;
            }


            IEnumerable<Group> groupsOfUser = _unitOfWork.GroupRepository.FindAllGroupOfUser(userId);

            _logger.LogInformation($"Created group: {groupDto.Name} by user: {userId}");
            return groupsOfUser;
        }

        /// <summary>
        /// Delete a group by group id
        /// </summary>
        /// <param name="groupId">The id of the group</param>
        /// <param name="userId">The user that delete the group</param>
        /// <returns>The current subscripted groups of the user</returns>
        public IEnumerable<Group> Delete(int groupId, int userId)
        {
            Group group;
            try
            {
                group = RetrieveGroupById(groupId, userId);
            }catch(Exception exception)
            {
                _logger.LogError($"Could not find group for  to be deleted", exception);
                throw exception;
            }

            List<Subscription> subscriptions = group.Members.ToList();
            subscriptions.ForEach(s => {
                _unitOfWork.SubscriptionRepository.Delete(s);
            });

            try
            {
                _unitOfWork.GroupRepository.Delete(group);
                _unitOfWork.SaveChanges();
            }catch(Exception exception)
            {
                _logger.LogError($"Could not delete group with id: {groupId} for user: {userId}", exception);
                throw exception;
            }


            IEnumerable<Group> groupsOfUser = _unitOfWork.GroupRepository.FindAllGroupOfUser(userId);

            _logger.LogInformation($"Deleted group: {groupId} by user: {userId}");

            return groupsOfUser;
        }

        /// <summary>
        /// Get the all the groups where the user is subscribed on
        /// </summary>
        /// <param name="userId">The active user</param>
        /// <returns>The subscribed groups of the user</returns>
        public IEnumerable<Group> GetGroups(int userId)
        {
            if (!UserExist(userId))
            {
                _logger.LogError($"Couldn't retrieve groups of non-existing user {userId}");
                throw new NullReferenceException("Couldn't retrieve groups of non-existing user");
            }

            IEnumerable<Group> groups = _unitOfWork.GroupRepository.FindAllGroupOfUser(userId);
            return groups;
        }

        /// <summary>
        /// Get group details based on the groupId. If the user is subscribed on
        /// </summary>
        /// <param name="groupId">The group id</param>
        /// <param name="userId">The active user</param>
        /// <returns>the group, null if the group doesn't exist or the user is not a subscriber</returns>
        public Group GetGroup(int groupId, int userId)
        {
            Group group;
            try
            {
                group = RetrieveGroupById(groupId, userId);
            }catch(Exception exception)
            {
                _logger.LogError($"Could not find group", exception);
                throw exception;
            }

            return group;
        }

        /// <summary>
        /// Update the selected groupd
        /// </summary>
        /// <param name="groupId">The group to be updated</param>
        /// <param name="newGroupData">the updated group data</param>
        /// <param name="userId">the user that requested the update</param>
        /// <returns>The updated group</returns>
        public Group Update(int groupId, GroupIncomingDTO newgroupData, int userId)
        {
            Group group;

            try
            {
                group = RetrieveGroupById(groupId, userId);
            }catch(Exception exception){
                _logger.LogError($"Could not update group", exception);
                throw exception;
            }            

            Color color = _unitOfWork.ColorRepository.GetColorByValue(newgroupData.ColorID);
            group.Color = color;

            Icon icon = _unitOfWork.IconRepository.GetIconByValue(newgroupData.IconID);
            group.Icon = icon;

            if (!string.IsNullOrEmpty(newgroupData.Description))
                group.Description = newgroupData.Description;

            if(!string.IsNullOrEmpty(newgroupData.Name))
                group.Name = newgroupData.Name;

            try
            {
                _unitOfWork.GroupRepository.Update(group);
                _unitOfWork.SaveChanges();
            }catch(Exception exception)
            {
                _logger.LogError($"Could not update group", exception);
                throw exception;
            }

            _logger.LogInformation($"Updated group: {group} by user: {userId}");
            return group;
        }

        /// <summary>
        /// Invite user to a group
        /// </summary>
        /// <param name="emailAddress">the email of the user to be invited</param>
        /// <param name="groupID">The group where the user is invited for</param>
        /// <param name="sendingUserID">The id of the user that is sending the invitation</param>
        /// <returns>true if the invitation is send, false otherwise</returns>
        public bool InviteUserToGroup(int sendingUserID, string emailAddress, int groupID)
        {
            User recievingUser = _unitOfWork.UserRepository.GetUser(emailAddress);
            User sendingUser = _unitOfWork.UserRepository.GetUser(sendingUserID);
            Group group = RetrieveGroupById(groupID, sendingUserID);

            if (recievingUser == default(User) || sendingUser == default(User) || group == default(Group) || Equals(recievingUser.ID, sendingUser.ID))
            {
                _logger.LogError($"Couldn't send invation to {emailAddress} from {sendingUserID} for group {groupID}: Incomplete or Incorrect data");
                throw new InvalidInputException("Couldn't send invation");
            }

            try
            {
                EmailDTO emailData = _emailHandler.CreateInviteEmail(recievingUser, sendingUser, group);
                _emailHandler.SendInviteEmail(emailData);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Could not send invite email to {emailAddress}");
                throw exception;
            }

            return true;
        }

        /// <summary>
        /// Subscribe to group
        /// </summary>
        /// <param name="userID">The active user id</param>
        /// <param name="token">The subscribe token</param>
        /// <returns>true if succesfull subscribed</returns>
        public bool SubscribeToGroup(int userID, string token)
        {
            User user = _unitOfWork.UserRepository.GetUser(userID);

            if(!_tokenHandler.ValidateSubscribeToken(token, user))
            {
                _logger.LogError($"Could not subscribe user {userID} to group: Invalid subscribtion token");
                throw new UnauthorizedAccessException("User has no persion to subscribe to group");
            }

            int groupID = _tokenHandler.GetGroupID(token);

            Subscription subscription = new Subscription()
            {
                DateOfSubscription = DateTime.Now,
                GroupID = groupID,
                UserID = user.ID
            };

            try
            {
                _unitOfWork.SubscriptionRepository.SubscribeUser(groupID, userID);
                _unitOfWork.SaveChanges();                
            }
            catch (Exception exception)
            {
                _logger.LogError($"Could not add subscription to database");
                throw exception;
            }

            return true;
        }

        /// <summary>
        /// Unsubcribe to group
        /// </summary>
        /// <param name="userID">The active user</param>
        /// <param name="groupID">The group to be unsubscribed/param>
        /// <returns>new group data</returns>
        public void Unsubscribe(int userID, int groupID)
        {
            Group group = _unitOfWork.GroupRepository.FindGroupOfUser(groupID, userID);

            if (group == (default(Group)))
            {
                _logger.LogError($"Try to unsubscribe user with id: {userID} from group: {groupID}. But group doesn't exist for user: {userID}");
                throw new InvalidInputException("User is not subscribed on given group");
            }

            if(group.Members.Count <= 1)
            {
                _logger.LogWarning($"Try to unsubscribe user: {userID} from group: {groupID}. But group can't have less than one subscriber.");
                throw new InvalidOperationException("Can't have less than one subscribe on a group");
            }

            _unitOfWork.SubscriptionRepository.UnSubscribeUser(groupID, userID);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Get group based on the groupId. If the user is subscribed on it
        /// </summary>
        /// <param name="groupId">The group id</param>
        /// <param name="userId">The active user</param>
        /// <returns>the group, null if the group doesn't exist or the user is not a subscriber</returns>
        private Group RetrieveGroupById (int groupId, int userId)
        {
            Group group = _unitOfWork.GroupRepository.FindGroupOfUser(groupId, userId);

            if (group == (default(Group)))
            {
                _logger.LogError($"Try to retrieve group with id: {groupId}. But group doesn't exist for user: {userId}");
                throw new InvalidInputException("User is not subscribed on given group");
            }

            return group;
        }

        /// <summary>
        /// Check if the user exist
        /// </summary>
        /// <param name="userEmail">the active user email</param>
        /// <returns>true if the user exist, false otherwise</returns>
        private bool UserExist(string userEmail)
        {
            return _unitOfWork.UserRepository.ContainceUser(userEmail);
        }

        /// <summary>
        /// Check if the user exist
        /// </summary>
        /// <param name="userId">the active user id</param>
        /// <returns>true if the user exist, false otherwise</returns>
        private bool UserExist(int userId)
        {
            return _unitOfWork.UserRepository.ContainceUser(userId);
        }
    }
}
