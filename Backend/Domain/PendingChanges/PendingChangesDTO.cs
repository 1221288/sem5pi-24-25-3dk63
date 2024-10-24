using Backend.Domain.Users.ValueObjects;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Users;

namespace DDDSample1.Domain.PendingChange
{
    public class PendingChangesDTO
    {
        public Name? Name { get; set; }
        public Email? Email { get; set; }
        public EmergencyContact? EmergencyContact { get; set; }
        public PhoneNumber? PhoneNumber { get; set; }
        public MedicalHistory? MedicalHistory { get; set; }
    }
}
