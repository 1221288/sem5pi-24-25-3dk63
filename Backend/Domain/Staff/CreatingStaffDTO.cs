using Backend.Domain.Staff.ValueObjects;
using DDDSample1.Domain.Users;
using DDDSample1.Domain.Specialization;

namespace DDDSample1.Domain.Staff
{
    public class CreatingStaffDTO
    {
        public string LicenseNumber { get; set; }
        public string SpecializationId { get; set; }
        public List<AvailabilitySlot>? AvailabilitySlots { get; set; }
        public string Email { get; set; } 
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public CreatingStaffDTO(
            string licenseNumber, 
            string specializationId, 
            List<AvailabilitySlot> availabilitySlots,
            string email, 
            string role, 
            string firstName,
            string lastName, 
            string phoneNumber)
        {
            // Staff-specific attributes
            this.LicenseNumber = licenseNumber;
            this.SpecializationId = specializationId;
            this.AvailabilitySlots = availabilitySlots;

            // User-specific attributes
            this.Email = email;
            this.Role = role;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumber = phoneNumber;

        }
    }
}
