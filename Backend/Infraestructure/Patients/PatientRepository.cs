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
        
        public async Task<int> GetNextSequentialNumberAsync()
        {
            var lastPatient = await _context.Patients
                .OrderByDescending(p => p.sequentialNumber)
                .FirstOrDefaultAsync();

            return lastPatient != null ? lastPatient.sequentialNumber + 1 : 1;
        }

        public async Task<User> FindByEmailAsync(Email email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }

        public async Task<bool> ExistsByMedicalRecordNumberAsync(MedicalRecordNumber medicalRecordNumber)
        {
            return await _context.Patients
                .AnyAsync(p => p.Id == medicalRecordNumber);
        }

        public async Task<Patient> FindByMedicalRecordNumberAsync(MedicalRecordNumber medicalRecordNumber)
        {
            return await _context.Patients.FirstOrDefaultAsync(p => p.Id.Equals(medicalRecordNumber));
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
        }

        public async Task<Patient?> GetPatientByPersonalEmailAsync(Email personalEmail)
        {
            var result = await (
                from Patient in _context.Patients
                join User in _context.Users on Patient.UserId equals User.Id
                where User.Email.Equals(personalEmail)
                select Patient
                ).FirstOrDefaultAsync();

            return result ?? null;
        }



    }
}
