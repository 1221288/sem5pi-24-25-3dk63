using Backend.Domain.Users.ValueObjects;
using DDDSample1.Domain.Users;

namespace DDDSample1.Domain.Patients
{
    public class UserProfileUpdateDTO
    {
        public Name? Name { get; set; }
        public Email? Email { get; set; }
        public Email? personalEmail { get; set; }
        public EmergencyContact? emergencyContact { get; set; }
        public PhoneNumber? PhoneNumber { get; set; }
        public Allergy? allergy { get; set; }      
    }
}