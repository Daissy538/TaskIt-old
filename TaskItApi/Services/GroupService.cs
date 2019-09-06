using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
            Group group = this._mapper.Map<Group>(groupDto);          
            User user = _unitOfWork.UserRepository.GetUser(userId);
            Subscription subscription = new Subscription();

            subscription.User = user;
            subscription.Group = group;
            subscription.DateOfSubscription = DateTime.UtcNow;

            group.Members.Add(subscription);
            user.Subscriptions.Add(subscription);

            try
            {
                _unitOfWork.GroupRepository.Create(group);
                _unitOfWork.SubscriptionRepository.Create(subscription);
                _unitOfWork.UserRepository.Update(user);
                _unitOfWork.SaveChanges();
            } catch(Exception exception)
            {
                _logger.LogError($"Could not create group: {groupDto.Name} by user:{userId} error: {exception.Message}");
                throw exception;
            }

            
            IEnumerable<Group> groupsOfUser = _unitOfWork.GroupRepository.FindByCondition(
                                                                            g => g.Members.Where(
                                                                                m => m.User.Email.Equals(user.Email))
                                                                            .Any());

            _logger.LogInformation($"Created group: {groupDto.Name} by user: {userId}");
            return groupsOfUser;
        }

        public IEnumerable<Group> Delete(int groupId, string userName)
        {
            User user = _unitOfWork.UserRepository.GetUser(userName);
            Group group = _unitOfWork.GroupRepository.FindByCondition(g => g.ID.Equals(groupId) && 
                                                                        g.Members.Where(m => m.User.Email.Equals(userName)).Any())
                                                                        .FirstOrDefault();

            if (group.Equals(default(Group)))
            {
                _logger.LogError($"Try to delete group with id: {groupId}. But group doesn't exist for user: {userName}");
                throw new NullReferenceException($"Could not find group with id: {groupId} for user:{userName}");
            }

            group.Members.ToList().ForEach(m => {
                m.User.Subscriptions.Remove(m);
                _unitOfWork.UserRepository.Update(m.User);
                _unitOfWork.SubscriptionRepository.Delete(m);
            });

            _unitOfWork.GroupRepository.Delete(group);
            _unitOfWork.SaveChanges();

            IEnumerable<Group> groupsOfUser = _unitOfWork.GroupRepository.FindByCondition(
                                                                g => g.Members.Where(
                                                                    m => m.User.Email.Equals(userName))
                                                                .Any());

            _logger.LogInformation($"Deleted group: {groupId} by user: {userName}");

            return groupsOfUser;
        }
    }
}
