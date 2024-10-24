using Backend.Domain.Staff.ValueObjects;
using Backend.Domain.Users.ValueObjects;
using DDDSample1.Domain.Specialization;
using DDDSample1.Domain.Users;

namespace DDDSample1.Domain.Staff
{
    public class StaffUpdateDTO
    {
        public LicenseNumber? Id { get; set; }
        public string? SpecializationDescription { get; set; }
        public Email? Email { get; set; }
        public PhoneNumber? PhoneNumber { get; set; }
        public AvailabilitySlot? AvailabilitySlots { get; set; }      
    }
}