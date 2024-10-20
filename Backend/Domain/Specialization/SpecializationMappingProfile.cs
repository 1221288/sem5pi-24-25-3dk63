using AutoMapper;
using Backend.Domain.Specialization.ValueObjects;
using DDDSample1.Domain.Specialization;

public class SpecializationMappingProfile : Profile
{
    public SpecializationMappingProfile()
    {
        CreateMap<Specialization, SpecializationDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.AsGuid()))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Value))
            .ForMember(dest => dest.SequentialNumber, opt => opt.MapFrom(src => src.SequentialNumber));

        CreateMap<SpecializationDTO, Specialization>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.AsGuid())) 
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => new Description(src.Description)))
            .ForMember(dest => dest.SequentialNumber, opt => opt.MapFrom(src => src.SequentialNumber));
    }
}
