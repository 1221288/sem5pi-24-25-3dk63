using Backend.Domain.Users.ValueObjects;
using DDDSample1.Domain.Users;

namespace DDDSample1.Domain.Patients
{
    public class PatientUpdateDTO
    {
        public MedicalRecordNumber id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public EmergencyContact emergencyContact { get; set; }
        public Allergy allergy { get; set; }        
    }
}