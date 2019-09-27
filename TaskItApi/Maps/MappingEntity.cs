using AutoMapper;
using TaskItApi.Dtos;
using TaskItApi.Entities;

namespace TaskItApi.Maps
{
    public class MappingEntity: Profile
    {
       public MappingEntity()
        {
            CreateMap<UserInComingDto, User>();

            CreateMap<GroupDto, Group>();

            CreateMap<Group, GroupDto>();

            CreateMap<Subscription, SubscriptionDto>()
                .ForMember(dto => dto.GroupID, s => s.MapFrom(src => src.Group.ID))
                .ForMember(dto => dto.GroupName, s => s.MapFrom(src => src.Group.Name))
                .ForMember(dto => dto.UserID, s => s.MapFrom(src => src.User.ID))
                .ForMember(dto => dto.UserName, s => s.MapFrom(src => src.User.Name));
        }
    }
}
