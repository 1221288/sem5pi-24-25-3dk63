using DDDSample1.Domain.Users;
using DDDSample1.Domain.Shared;


namespace DDDSample1.Domain.Patients
{
    public interface IPatientRepository : IRepository<Patient, MedicalRecordNumber>
    {
        Task<bool> EmailExistsAsync(string email);
        Task<int> GetNextSequentialNumberAsync();
        Task<User> FindByEmailAsync(Email email);
        Task<bool> ExistsByMedicalRecordNumberAsync(MedicalRecordNumber medicalRecordNumber);
        Task<Patient> FindByMedicalRecordNumberAsync(MedicalRecordNumber medicalRecordNumber);
        Task UpdatePatientAsync(Patient patient);
        Task<Patient?> GetPatientByIamEmailAsync(string iamEmail);
    }
}
