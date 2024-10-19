using DDDSample1.Domain;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Users;
using DDDSample1.Domain.Shared;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace DDDSample1.Patients
{
    public class PatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly EmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public PatientService(IPatientRepository patientRepository, EmailService emailService, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<PatientDTO> GetByIdAsync(MedicalRecordNumber id)
        {
            var patient = await this._patientRepository.GetByIdAsync(id);
            return patient == null ? null : _mapper.Map<PatientDTO>(patient);
        }

        public async Task<ActionResult<PatientDTO>> RegisterPatientAsync(RegisterPatientDTO registerDto)
        {
            // Check if the patient medical record number already exists
            if (!await _patientRepository.ExistsByMedicalRecordNumberAsync(new MedicalRecordNumber(registerDto.MedicalRecordNumber)))
            {
                var patient = new Patient(registerDto.dateOfBirth, registerDto.gender, registerDto.emergencyContact);
                // Register the patient
                await _patientRepository.AddAsync(patient);
                return _mapper.Map<PatientDTO>(patient);
            }
            else
            {
                throw new BusinessRuleValidationException("Patient already exists.");
            }
        }
        
        // // Obtém todos os pacientes
        // public async Task<List<PatientDTO>> GetAllAsync()
        // {
        //     var list = await this._patientRepository.GetAllAsync();
        //     List<PatientDTO> listDto = list.ConvertAll(patient => new PatientDTO
        //     {
        //         Id = patient.Id.AsGuid(),
        //         DateOfBirth = patient.DateOfBirth.Value,
        //         Gender = patient.Gender.Value,
        //         MedicalRecordNumber = patient.MedicalRecordNumber,
        //         EmergencyContact = patient.Value,
        //         AppointmentHistory = patient.AppointmentHistory.ConvertAll(appointment => new AppointmentDTO
        //         {
        //             Date = appointment.Date,
        //             Doctor = appointment.Doctor,
        //             Description = appointment.Description
        //         }),
        //         Active = patient.Active
        //     });
        //     return listDto;
        // }

        // // Obtém um paciente pelo ID
        // public async Task<PatientDTO> GetByIdAsync(UserId id)
        // {
        //     var patient = await this._patientRepository.GetByIdAsync(id);
        //     if (patient == null) return null;

        //     return new PatientDTO
        //     {
        //         Id = patient.Id.AsGuid(),
        //         FirstName = patient.FirstName.Value,
        //         LastName = patient.LastName.Value,
        //         FullName = patient.FullName.Value,
        //         DateOfBirth = patient.DateOfBirth.Value,
        //         Gender = patient.Gender.Value,
        //         MedicalRecordNumber = patient.MedicalRecordNumber,
        //         Email = patient.Email.Value,
        //         Phone = patient.Phone.Value,
        //         EmergencyContact = new EmergencyContactDTO
        //         {
        //             Name = patient.EmergencyContact.Name,
        //             Relationship = patient.EmergencyContact.Relationship,
        //             Phone = patient.EmergencyContact.Phone,
        //             Email = patient.EmergencyContact.Email
        //         },
        //         AppointmentHistory = patient.AppointmentHistory.ConvertAll(appointment => new AppointmentDTO
        //         {
        //             Date = appointment.Date,
        //             Doctor = appointment.Doctor,
        //             Description = appointment.Description
        //         }),
        //         Active = patient.Active
        //     };
        // }
        
        // // Atualiza um paciente
        // public async Task<PatientDTO> UpdateAsync(PatientDTO dto)
        // {
        //     var patient = await this._patientRepository.GetByIdAsync(new PatientId(dto.Id));
        //     if (patient == null) return null;

        //     patient.ChangeEmergencyContact(new EmergencyContact(dto.EmergencyContact.Name, dto.EmergencyContact.Relationship, dto.EmergencyContact.Phone, dto.EmergencyContact.Email));

        //     await this._unitOfWork.CommitAsync();

        //     return new PatientDTO
        //     {
        //         Id = patient.Id.AsGuid(),
        //         FirstName = patient.FirstName.Value,
        //         LastName = patient.LastName.Value,
        //         FullName = patient.FullName.Value,
        //         DateOfBirth = patient.DateOfBirth.Value,
        //         Gender = patient.Gender.Value,
        //         MedicalRecordNumber = patient.MedicalRecordNumber,
        //         Email = patient.Email.Value,
        //         Phone = patient.Phone.Value,
        //         EmergencyContact = new EmergencyContactDTO
        //         {
        //             Name = patient.EmergencyContact.Name,
        //             Relationship = patient.EmergencyContact.Relationship,
        //             Phone = patient.EmergencyContact.Phone,
        //             Email = patient.EmergencyContact.Email
        //         },
        //         AppointmentHistory = patient.AppointmentHistory.ConvertAll(appointment => new AppointmentDTO
        //         {
        //             Date = appointment.Date,
        //             Doctor = appointment.Doctor,
        //             Description = appointment.Description
        //         }),
        //         Active = patient.Active
        //     };
        // }

        // // Exclui um paciente (lógica de exclusão)
        // public async Task<PatientDTO> DeleteAsync(UserId id)
        // {
        //     var patient = await this._patientRepository.GetByIdAsync(id);
        //     if (patient == null) return null;

        //     if (patient.Active)
        //     {
        //         throw new BusinessRuleValidationException("Não é possível excluir um paciente ativo.");
        //     }

        //     this._patientRepository.Remove(patient);
        //     await this._unitOfWork.CommitAsync();

        //     return new PatientDTO
        //     {
        //         Id = patient.Id.AsGuid(),
        //         FirstName = patient.FirstName.Value,
        //         LastName = patient.LastName.Value,
        //         FullName = patient.FullName.Value,
        //         DateOfBirth = patient.DateOfBirth.Value,
        //         Gender = patient.Gender.Value,
        //         MedicalRecordNumber = patient.MedicalRecordNumber,
        //         Email = patient.Email.Value,
        //         Phone = patient.Phone.Value,
        //         EmergencyContact = new EmergencyContactDTO
        //         {
        //             Name = patient.EmergencyContact.Name,
        //             Relationship = patient.EmergencyContact.Relationship,
        //             Phone = patient.EmergencyContact.Phone,
        //             Email = patient.EmergencyContact.Email
        //         },
        //         AppointmentHistory = patient.AppointmentHistory.ConvertAll(appointment => new AppointmentDTO
        //         {
        //             Date = appointment.Date,
        //             Doctor = appointment.Doctor,
        //             Description = appointment.Description
        //         }),
        //         Active = patient.Active
        //     };
        // }

        // // Busca um paciente pelo email
        // public async Task<PatientDTO> FindByEmailAsync(string email)
        // {
        //     var patient = await this._patientRepository.FindByEmailAsync(new Email(email));
        //     if (patient == null) return null;

        //     return new PatientDTO
        //     {
        //         Id = patient.Id.AsGuid(),
        //         FirstName = patient.FirstName.Value,
        //         LastName = patient.LastName.Value,
        //         FullName = patient.FullName.Value,
        //         DateOfBirth = patient.DateOfBirth.Value,
        //         Gender = patient.Gender.Value,
        //         MedicalRecordNumber = patient.MedicalRecordNumber,
        //         Email = patient.Email.Value,
        //         Phone = patient.Phone.Value,
        //         EmergencyContact = new EmergencyContactDTO
        //         {
        //             Name = patient.EmergencyContact.Name,
        //             Relationship = patient.EmergencyContact.Relationship,
        //             Phone = patient.EmergencyContact.Phone,
        //             Email = patient.EmergencyContact.Email
        //         },
        //         AppointmentHistory = patient.AppointmentHistory.ConvertAll(appointment => new AppointmentDTO
        //         {
        //             Date = appointment.Date,
        //             Doctor = appointment.Doctor,
        //             Description = appointment.Description
        //         }),
        //         Active = patient.Active
        //     };
        // }        
    }
}
