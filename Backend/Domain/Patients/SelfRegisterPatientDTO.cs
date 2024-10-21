using Backend.Domain.Users.ValueObjects;

namespace DDDSample1.Domain.Patients
{
    public class SelfRegisterPatientDTO
    {
        public string IamEmail { get; set; }
        public string PersonalEmail { get; set; }
        public string PhoneNumber { get; set; } 
    }
}