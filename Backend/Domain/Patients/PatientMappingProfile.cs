using AutoMapper;
using DDDSample1.Domain;
using DDDSample1.Domain.Appointments;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Users;

public class PatientMappingProfile : Profile
{
    public PatientMappingProfile()
    {
        CreateMap<Patient, PatientDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.dateOfBirth, opt => opt.MapFrom(src => src.dateOfBirth))
            .ForMember(dest => dest.gender, opt => opt.MapFrom(src => src.gender))
            .ForMember(dest => dest.allergy, opt => opt.MapFrom(src => src.allergy))
            .ForMember(dest => dest.emergencyContact, opt => opt.MapFrom(src => src.emergencyContact))
            .ForMember(dest => dest.appointmentHistoryList, opt => opt.MapFrom(src => src.appointmentHistoryList));

        CreateMap<PatientDTO, Patient>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new MedicalRecordNumber(src.Id.ToString())))
            .ForMember(dest => dest.dateOfBirth, opt => opt.MapFrom(src => src.dateOfBirth))
            .ForMember(dest => dest.gender, opt => opt.MapFrom(src => src.gender))
            .ForMember(dest => dest.allergy, opt => opt.MapFrom(src => src.allergy))
            .ForMember(dest => dest.emergencyContact, opt => opt.MapFrom(src => src.emergencyContact))
            .ForMember(dest => dest.appointmentHistoryList, opt => opt.MapFrom(src => src.appointmentHistoryList))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
    }
}
