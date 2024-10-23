using AutoMapper;
using DDDSample1.Domain.PendingChange;

public class PendingChangesMappingProfile : Profile
{
    public PendingChangesMappingProfile()
    {
        CreateMap<PendingChangesDTO, PendingChanges>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.EmergencyContact, opt => opt.MapFrom(src => src.EmergencyContact))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Allergy, opt => opt.MapFrom(src => src.Allergy));
        
        CreateMap<PendingChanges, PendingChangesDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.EmergencyContact, opt => opt.MapFrom(src => src.EmergencyContact))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Allergy, opt => opt.MapFrom(src => src.Allergy));
    }
}
