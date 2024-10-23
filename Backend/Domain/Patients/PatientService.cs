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
        private readonly List<string> _sensitiveAttributes = new List<string> { "Email", "emergencyContact"};

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

                // Register the patient
                var patient = new Patient(user.Id, registerDto.dateOfBirth, registerDto.gender, registerDto.emergencyContact, registerDto.allergy, _patientRepository.GetNextSequentialNumberAsync().Result);
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
            return user;
        }

        public async Task<bool> UpdatePatientProfileAsync(PatientUpdateDTO updateDto)
        {
            var patient = await _patientRepository.FindByMedicalRecordNumberAsync(updateDto.Id);
            var userPatient = await _patientRepository.FindByEmailAsync(updateDto.personalEmail);

            if (patient == null)
            {
                return false;
            }

            // Update patient profile
            await UpdatePatientInfo(patient, userPatient, updateDto);

            // Log changes
            _logger.LogInformation($"Patient profile updated: {updateDto.Id}");

            return true;
        }

        private async Task<Patient> UpdatePatientInfo(Patient patient, User user, PatientUpdateDTO updateDto)
        {
            if (user == null) throw new ArgumentNullException(nameof(user), "User cannot be null");

            if (patient == null) throw new ArgumentNullException(nameof(patient), "Patient cannot be null");

            bool userSensitiveDataChanged = false;
            bool patientSensitiveDataChanged = false;

            PropertyInfo[] properties = typeof(PatientUpdateDTO).GetProperties();
            foreach (PropertyInfo property in properties)
            {   
                // Verify if it is the id, if so continue to the other atributes
                if(property.PropertyType == typeof(MedicalRecordNumber)) continue;

                var newValue = property.GetValue(updateDto, null);

                if (newValue != null)
                {
                    if (CheckIfExistsOnUser(property.Name))
                    {
                        var oldValue = typeof(User).GetProperty(property.Name)?.GetValue(user);

                        typeof(User).GetProperty(property.Name)?.SetValue(user, newValue);

                        if (_sensitiveAttributes.Contains(property.Name))
                        {
                            userSensitiveDataChanged = true;
                        }
                    }

                    if (CheckIfExistsOnPatient(property.Name))
                    {
                        var oldValue = typeof(Patient).GetProperty(property.Name)?.GetValue(patient);

                        typeof(Patient).GetProperty(property.Name)?.SetValue(patient, newValue);

                        if (_sensitiveAttributes.Contains(property.Name))
                        {
                            patientSensitiveDataChanged = true;
                        }
                    }
                }
            }

            await _userRepository.UpdateUserAsync(user);
            await _patientRepository.UpdatePatientAsync(patient);
            await _unitOfWork.CommitAsync();
            
            if (userSensitiveDataChanged || patientSensitiveDataChanged)
            {
                await _emailService.SendPatientNotificationEmailAsync(updateDto);
            }

            return patient;
        }

        public async Task<bool> UpdatePatientUserProfile(PatientUpdateDTO updateDto)
        {
            var patient = await _patientRepository.FindByMedicalRecordNumberAsync(updateDto.Id);
            var userPatient = await _patientRepository.FindByEmailAsync(updateDto.personalEmail);

            if (patient == null)
            {
                return false;
            }

            await UpdatePatientUserProfile(patient, userPatient, updateDto);

            _logger.LogInformation($"Patient profile updated: {updateDto.Id}");

            return true;
        }

        private async Task<Patient> UpdatePatientUserProfile(Patient patient, User user, PatientUpdateDTO updateDto)
        {
            if (user == null) throw new ArgumentNullException(nameof(user), "User cannot be null");
            if (patient == null) throw new ArgumentNullException(nameof(patient), "Patient cannot be null");

            bool userSensitiveDataChanged = false;
            bool patientSensitiveDataChanged = false;
            var oldEmail = user.Email;

            PropertyInfo[] properties = typeof(PatientUpdateDTO).GetProperties();
            foreach (PropertyInfo property in properties)
            {   
                if(property.PropertyType == typeof(MedicalRecordNumber)) continue;

                var newValue = property.GetValue(updateDto, null);

                if (newValue != null)
                {
                    if (CheckIfExistsOnUser(property.Name))
                    {
                        var oldValue = typeof(User).GetProperty(property.Name)?.GetValue(user);

                        typeof(User).GetProperty(property.Name)?.SetValue(user, newValue);

                        if (_sensitiveAttributes.Contains(property.Name))
                        {
                            userSensitiveDataChanged = true;
                        }
                    }

                    if (CheckIfExistsOnPatient(property.Name))
                    {
                        var oldValue = typeof(Patient).GetProperty(property.Name)?.GetValue(patient);

                        typeof(Patient).GetProperty(property.Name)?.SetValue(patient, newValue);

                        if (_sensitiveAttributes.Contains(property.Name))
                        {
                            patientSensitiveDataChanged = true;
                        }
                    }
                }
            }

            if (userSensitiveDataChanged || patientSensitiveDataChanged)
            {
                user.generateConfirmationToken();
                user.ChangeActiveFalse();
                patient.ChangeActiveFalse();
            }

            await _userRepository.UpdateUserAsync(user);
            await _patientRepository.UpdatePatientAsync(patient);
            await _unitOfWork.CommitAsync();
            
            if (userSensitiveDataChanged || patientSensitiveDataChanged)
            {
                await _emailService.SendUpdateEmail(oldEmail.ToString(), user.ConfirmationToken);
            }

            return patient;
        }

        public async Task ConfirmEmailAsync(string token)
        {
            var user = await _userRepository.GetUserByConfirmationTokenAsync(token);
            if (user == null)
            {
                throw new Exception("Invalid or expired confirmation token.");
            }

            var patient = await _patientRepository.FindByUserIdAsync(user.Id);
            if (patient == null)
            {
                throw new Exception("Patient not found.");
            }

            user.ChangeActiveTrue();
            patient.ChangeActiveTrue();

            await _userRepository.UpdateUserAsync(user);
            await _patientRepository.UpdatePatientAsync(patient);
            await _unitOfWork.CommitAsync();
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

        public async Task<PatientDTO> GetPatientByUsername(Username email)
        {
            var user = await _userRepository.GetUserByUsernameAsync(email);

            var patient = await _patientRepository.FindByUserIdAsync(user.Id);

            if (patient == null)
            {
                return null;
            }

            return _mapper.Map<PatientDTO>(patient);
        }

        public async Task<UserDTO> GetUserByUserIdAsync(UserId userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<UserDTO>(user);
        }
    }
}
