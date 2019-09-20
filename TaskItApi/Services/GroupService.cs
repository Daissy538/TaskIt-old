using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TaskItApi.Dtos;
using TaskItApi.Entities;
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

        public IEnumerable<Group> Create(GroupDto groupDto, int userId)
        {                     
            User user = _unitOfWork.UserRepository.GetUser(userId);

            if(user.Equals(default(User)))
            {
                _logger.LogError($"Couldn't delete group for non-existing user {userId}");
                throw new NullReferenceException("Couldn't create group for non-existing user");
            }

            Group group = new Group() { Color = groupDto.Color,
                                        Name = groupDto.Name,
                                        Icon = groupDto.Icon,
                                        Description = groupDto.Description};            

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
            User user = _unitOfWork.UserRepository.GetUser(userId);
          
            if (user.Equals(default(User)))
            {
                _logger.LogError($"Couldn't delete group for non-existing user {userId}");
                throw new NullReferenceException("Couldn't delete group for non-existing user");
            }

            Group group = _unitOfWork.GroupRepository.FindGroupOfUser(groupId, userId);

            if (group.Equals(default(Group)))
            {
                _logger.LogError($"Try to delete group with id: {groupId}. But group doesn't exist for user: {userId}");
                throw new NullReferenceException("User is not subscribed on given groups");
            }

            List<Subscription> subscriptions = group.Members.ToList();
            subscriptions.ForEach(s => {
                _unitOfWork.SubscriptionRepository.Delete(s);
            });
            
            _unitOfWork.GroupRepository.Delete(group);
            _unitOfWork.SaveChanges();

            IEnumerable<Group> groupsOfUser = _unitOfWork.GroupRepository.FindAllGroupOfUser(userId);

            _logger.LogInformation($"Deleted group: {groupId} by user: {userId}");

            return groupsOfUser;
        }

        public IEnumerable<Group> GetGroups(int userId)
        {
            User user = _unitOfWork.UserRepository.GetUser(userId);

            if (user.Equals(default(User)))
            {
                _logger.LogError($"Couldn't retrieve groups of non-existing user {userId}");
                throw new NullReferenceException("Couldn't retrieve groups of non-existing user");
            }

            IEnumerable<Group> groups = _unitOfWork.GroupRepository.FindAllGroupOfUser(userId);
            return groups;
        }


    }
}
