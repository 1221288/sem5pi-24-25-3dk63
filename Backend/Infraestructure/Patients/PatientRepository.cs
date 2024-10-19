using DDDSample1.Domain;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Users;
using System.Collections.Generic;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace DDDSample1.Infrastructure.Patients
{
    public class PatientRepository : BaseRepository<Patient, MedicalRecordNumber>, IPatientRepository
    {
        private readonly DDDSample1DbContext _context;

        public PatientRepository(DDDSample1DbContext context) : base(context.Patients)
        {
            _context = context;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            // Check for existence in the Users table
            return await _context.Users.AnyAsync(u => u.Email.Equals(email));
        }

        // public async Task RegisterPatientAsync(Patient patient)
        // {
        //     // Validate input DTOs
        //     if (registerDto == null) throw new ArgumentNullException(nameof(registerDto));

        //     // Convert PatientDTO to Patient using the parameterized constructor
        //     var patient = new Patient(
        //         registerDto.dateOfBirth,
        //         registerDto.gender,
        //         registerDto.emergencyContact
        //     );

        //     // Add allergies and appointments
        //     foreach (var allergy in registerDto.allergiesMedicalConditionalsList)
        //     {
        //         patient.AddAllergy(allergy);
        //     }

        //     foreach (var appointment in registerDto.appointmentHistoryList)
        //     {
        //         patient.AddAppointment(appointment);
        //     }

        //     //Add the patient
        //     await _context.Patients.AddAsync(patient);
            
        //     // Save changes to the database
        //     await _context.SaveChangesAsync();
        // }
        
        public async Task<int> GetNextSequentialNumberAsync()
        {
            var lastUser = await _context.Users
                .OrderByDescending(u => u.SequentialNumber)
                .FirstOrDefaultAsync();

            return lastUser != null ? lastUser.SequentialNumber + 1 : 1;
        }

        public async Task<User> FindByEmailAsync(Email email)
        {
            return await _context.Users .FirstOrDefaultAsync(u => u.Email.Equals(email));
        }

        public async Task<bool> ExistsByMedicalRecordNumberAsync(MedicalRecordNumber medicalRecordNumber)
        {
            return await _context.Patients
                .AnyAsync(p => p.Id == medicalRecordNumber);
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
        }

        public async Task<Patient?> GetPatientByIamEmailAsync(string iamEmail)
        {
            var result = await _context.Patients
                .Join(
                    _context.Users,
                    patient => patient.UserId,
                    user => user.Id,
                    (patient, user) => new { Patient = patient, User = user }
                )
                .Where(joined => joined.User.Username.Equals(iamEmail))
                .Select(joined => joined.Patient)
                .FirstOrDefaultAsync();

            return result ?? null;
        }



    }
}
