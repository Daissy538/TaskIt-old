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

            CreateMap<GroupIncomingDTO, Group>();

            CreateMap<Group, GroupOutgoingDTO>()
                .ForMember(dto => dto.ColorName, s => s.MapFrom(src => src.Color.Name))
                .ForMember(dto => dto.ColorValue, s => s.MapFrom(src => src.Color.Value))
                .ForMember(dto => dto.IconName, s => s.MapFrom(src => src.Icon.Name))
                .ForMember(dto => dto.IconValue, s => s.MapFrom(src => src.Icon.Value));

            CreateMap<Subscription, SubscriptionOutgoingDto>()
                .ForMember(dto => dto.GroupID, s => s.MapFrom(src => src.Group.ID))
                .ForMember(dto => dto.GroupName, s => s.MapFrom(src => src.Group.Name))
                .ForMember(dto => dto.UserID, s => s.MapFrom(src => src.User.ID))
                .ForMember(dto => dto.UserName, s => s.MapFrom(src => src.User.Name));

            CreateMap<Color, ColorDTO>();
            CreateMap<Icon, IconDTO>();

        }
    }
}
