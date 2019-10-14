using AutoMapper;

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskItApi.Dtos;
using TaskItApi.Entities;
using TaskItApi.Exceptions;
using TaskItApi.Models.Interfaces;
using TaskItApi.Services.Interfaces;

namespace TaskItApi.Services
{
    public class GroupService: IGroupService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public GroupService(IMapper mapper, IUnitOfWork unitOfWork, ILogger<IGroupService> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

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
        /// Get group based on the groupId. If the user is subscribed on it
        /// </summary>
        /// <param name="groupId">The group id</param>
        /// <param name="userId">The active user</param>
        /// <returns>the group, null if the group doesn't exist or the user is not a subscriber</returns>
        private Group RetrieveGroupById (int groupId, int userId)
        {
            if (!UserExist(userId))
            {
                _logger.LogError($"Couldn't retrieve groups of non-existing user {userId}");
                throw new NullReferenceException("Couldn't retrieve groups of non-existing user");
            }

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
        /// <param name="userId">the active user id</param>
        /// <returns>true if the user exist, false otherwise</returns>
        private bool UserExist(int userId)
        {
            return _unitOfWork.UserRepository.ContainceUser(userId);
        }
    }
}
