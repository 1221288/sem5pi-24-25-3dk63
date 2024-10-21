using DDDSample1.Domain;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Users;
using DDDSample1.Domain.Shared;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using Backend.Domain.Users.ValueObjects;
using Backend.Domain.Shared;

namespace DDDSample1.Patients
{
    public class PatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IUserRepository _userRepository;
        private readonly EmailService _emailService;
        private readonly AuditService _auditService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PatientService> _logger;
        private readonly IMapper _mapper;


        public PatientService(IPatientRepository patientRepository, IUserRepository userRepository, EmailService emailService,
                                IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<PatientService> logger,
                                IUnitOfWork unitOfWork, IMapper mapper, AuditService auditService)
        {
            _patientRepository = patientRepository;
            _userRepository = userRepository;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
            _auditService = auditService;
        }

        public async Task<PatientDTO> GetByIdAsync(MedicalRecordNumber id)
        {
            var patient = await this._patientRepository.GetByIdAsync(id);
            return patient == null ? null : _mapper.Map<PatientDTO>(patient);
        }

        public async Task<PatientDTO> RegisterPatientAsync(RegisterPatientDTO registerDto)
        {
            // Check if the patient medical record number already exists
            if (await _patientRepository.FindByEmailAsync(registerDto.personalEmail) == null)
            {
                //Register the initial user
                var user = await createUser(registerDto);

                var patient = new Patient(user.Id, registerDto.dateOfBirth, registerDto.gender, registerDto.emergencyContact, registerDto.allergy, _patientRepository.GetNextSequentialNumberAsync().Result);
                // Register the patient
                patient = await _patientRepository.AddAsync(patient);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<PatientDTO>(patient);
            }
            else
            {
                throw new BusinessRuleValidationException("Patient already exists.");
            }
        }

        private async Task<User> createUser(RegisterPatientDTO dto)
        {
            int sequentialNumber = await this._userRepository.GetNextSequentialNumberAsync();
            string domain = _configuration["DNS_DOMAIN"];
            if (string.IsNullOrEmpty(domain))
            {
                throw new BusinessRuleValidationException("O domínio DNS não está configurado corretamente.");
            }
            int recruitmentYear = DateTime.Now.Year;
            var role = new Role(RoleType.Patient);
            var phoneNumber = new PhoneNumber(dto.phoneNumber.Number);
            var user = new User(role, dto.personalEmail, dto.name, recruitmentYear, domain, sequentialNumber,phoneNumber);
            user.ChangeActiveFalse();
            user = await _userRepository.AddAsync(user);
            await _unitOfWork.CommitAsync();
            return user;
        }

        public async Task<bool> UpdatePatientProfileAsync(PatientUpdateDTO updateDto)
        {
            var patient = await _patientRepository.FindByMedicalRecordNumberAsync(updateDto.id);
            var userPatient = await _patientRepository.FindByEmailAsync(new Email(updateDto.Email));

            if (patient == null)
            {
                return false;
            }

            var oldEmail = userPatient.Email.ToString();

            // Update patient profile
            await UpdatePatientInfo(patient, userPatient, updateDto);

            // Log changes
            _logger.LogInformation($"Patient profile updated: {updateDto.id}");

            // Send notification if email changed
            if (oldEmail != updateDto.Email)
            {
                await _emailService.SendNotificationEmailAsync(updateDto);
            }

            return true;
        }

        private async Task<Patient> UpdatePatientInfo(Patient patient, User user, PatientUpdateDTO updateDto)
        {
            bool userAttributesUpdated = false;
            bool patientAttributesUpdated = false;

            PropertyInfo[] properties = typeof(PatientUpdateDTO).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var newValue = property.GetValue(updateDto, null);

                if (newValue != null)
                {

                    if(CheckIfExistsOnUser(property.Name)){
                        typeof(User).GetProperty(property.Name)?.SetValue(user, newValue);
                        userAttributesUpdated = true;
                    }

                    if(CheckIfExistsOnPatient(property.Name)){
                        typeof(Patient).GetProperty(property.Name)?.SetValue(patient, newValue);
                        patientAttributesUpdated = true;
                    }
                }
            }

            if(userAttributesUpdated) await _userRepository.UpdateUserAsync(user);
            if(patientAttributesUpdated) await _patientRepository.UpdatePatientAsync(patient);

            return patient;
        }

        private bool CheckIfExistsOnUser(string propertyName)
        {
            PropertyInfo userProperty = typeof(User).GetProperty(propertyName);
            return userProperty != null && userProperty.CanWrite;
        }

        private bool CheckIfExistsOnPatient(string propertyName)
        {
            PropertyInfo patientProperty = typeof(Patient).GetProperty(propertyName);
            return patientProperty != null && patientProperty.CanWrite;
        }



        public async Task<PatientDTO> DeletePatientAsync(MedicalRecordNumber id, string adminEmail)
        {
            _logger.LogInformation($"Tentando deletar o paciente com medical record number ID No patient service: {id}");

            var patientToRemove = await _patientRepository.FindByMedicalRecordNumberAsync(id);

            if (patientToRemove == null) return null;

            _auditService.LogDeletionPatient(patientToRemove, adminEmail);

            this._patientRepository.Remove(patientToRemove);
            await this._unitOfWork.CommitAsync();

            return _mapper.Map<PatientDTO>(patientToRemove); 
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
