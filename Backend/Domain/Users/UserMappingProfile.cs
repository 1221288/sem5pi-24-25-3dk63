using AutoMapper;
using DDDSample1.Domain.Users;
using DDDSample1.Domain;
using Backend.Domain.Users.ValueObjects;

public class UserMappingProfile : Profile
{
  public UserMappingProfile()
  {
    CreateMap<User, UserDTO>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.AsGuid()))
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.FullName))
        .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
        .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
        .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

    CreateMap<UserDTO, User>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new UserId(src.Id)))
        .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
        .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
        .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new Name(src.Name.FirstName, src.Name.LastName)));
  }
}
