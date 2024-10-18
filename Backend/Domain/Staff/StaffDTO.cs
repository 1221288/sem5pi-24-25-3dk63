using Backend.Domain.Staff.ValueObjects;
using DDDSample1.Domain.Specialization;
using DDDSample1.Domain.Users;

namespace DDDSample1.Domain.Staff
{
    public class StaffDTO
    {
        public LicenseNumber LicenseNumber { get; set; }
        public UserId UserId { get; set; }
        public SpecializationId SpecializationId { get; set; }
        public List<AvailabilitySlot> AvailabilitySlots { get; set; }

        public StaffDTO()
        {
            AvailabilitySlots = new List<AvailabilitySlot>();
        }
    }
}
