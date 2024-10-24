using Backend.Domain.Users.ValueObjects;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Users;

namespace DDDSample1.Domain.PendingChange
{
    public class PendingChanges
    {
        public UserId UserId { get; set; }
        public Name? Name { get; set; }
        public Email? Email { get; set; }
        public EmergencyContact? EmergencyContact { get; set; }
        public PhoneNumber? PhoneNumber { get; set; }
        public MedicalHistory? MedicalHistory { get; set; }

        public PendingChanges(UserId userId)
        {
            UserId = userId;
        }

        public void UpdateEmail(Email newEmail)
        {
            Email = newEmail;
        }

        public void UpdateName(Name newName)
        {
            Name = newName;
        }

        public void UpdatePhoneNumber(PhoneNumber newPhoneNumber)
        {
            PhoneNumber = newPhoneNumber;
        }

        public void UpdateEmergencyContact(EmergencyContact newEmergencyContact)
        {
            EmergencyContact = newEmergencyContact;
        }

        public void UpdateMedicalHistory(MedicalHistory newMedicalHistory)
        {
            MedicalHistory = newMedicalHistory;
        }

        public void ResetChanges()
        {
            Email = null;
            Name = null;
            PhoneNumber = null;
            EmergencyContact = null;
            MedicalHistory = null;
        }
    }
}
