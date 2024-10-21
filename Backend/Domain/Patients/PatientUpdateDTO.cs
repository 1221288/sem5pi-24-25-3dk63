using Backend.Domain.Users.ValueObjects;
using DDDSample1.Domain.Users;

namespace DDDSample1.Domain.Patients
{
    public class PatientUpdateDTO
    {
        public MedicalRecordNumber Id { get; set; }
        public Email personalEmail { get; set; }
        public Name? name { get; set; }
        public Email? emailToChange { get; set; }
        public EmergencyContact? emergencyContact { get; set; }
        public Allergy? allergy { get; set; }        
    }
}