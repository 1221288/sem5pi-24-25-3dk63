using AutoMapper;
using Backend.Domain.Staff.ValueObjects;
using DDDSample1.Domain.Specialization;
using DDDSample1.Domain.Staff;
using DDDSample1.Domain.Users;

public class StaffMappingProfile : Profile
{
    public StaffMappingProfile()
    {
        CreateMap<Staff, StaffDTO>()
            .ForMember(dest => dest.LicenseNumber, opt => opt.MapFrom(src => src.Id.AsString()))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId.AsString())) 
            .ForMember(dest => dest.SpecializationId, opt => opt.MapFrom(src => src.SpecializationId.AsString()))
            .ForMember(dest => dest.AvailabilitySlots, opt => opt.MapFrom(src => src.AvailabilitySlots.Slots));

        CreateMap<StaffDTO, Staff>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new LicenseNumber(src.LicenseNumber)))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => new UserId(src.UserId)))
            .ForMember(dest => dest.SpecializationId, opt => opt.MapFrom(src => new SpecializationId(src.SpecializationId)))
            .ForMember(dest => dest.AvailabilitySlots, opt => opt.MapFrom(src => new AvailabilitySlots(src.AvailabilitySlots)));
    }
}
