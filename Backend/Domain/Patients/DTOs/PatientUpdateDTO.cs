using Backend.Domain.Users.ValueObjects;
using DDDSample1.Domain.Users;

namespace DDDSample1.Domain.Patients
{
    public class PatientUpdateDTO
    {
        public MedicalRecordNumber Id { get; set; }
        public Email personalEmail { get; set; }
        public Name? Name { get; set; }
        public Email? Email { get; set; }
        public EmergencyContact? emergencyContact { get; set; }
        public PhoneNumber? PhoneNumber { get; set; }
        public MedicalHistory? medicalHistory { get; set; }      
    }
}