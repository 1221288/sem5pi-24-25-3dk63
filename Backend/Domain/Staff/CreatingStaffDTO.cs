using Backend.Domain.Staff.ValueObjects;

namespace DDDSample1.Domain.Staff
{
    public class CreatingStaffDTO
    {
        public string LicenseNumber { get; set; }
        public string UserId { get; set; }
        public string SpecializationId { get; set; }
        public List<AvailabilitySlot>? AvailabilitySlots { get; set; }

        public CreatingStaffDTO(string licenseNumber, string userId, string specializationId, List<AvailabilitySlot> availabilitySlots)
        {
            this.LicenseNumber = licenseNumber;
            this.UserId = userId;
            this.SpecializationId = specializationId;
            this.AvailabilitySlots = availabilitySlots;
        }
    }
}
