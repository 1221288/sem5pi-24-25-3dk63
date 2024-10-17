using DDDSample1.Domain.Specialization;
using DDDSample1.Domain.Users;

namespace DDDSample1.Domain.Staff
{
    public class StaffDTO
    {
        public LicenseNumber LicenseNumber { get; set; }
        public UserId UserId { get; set; }
        public SpecializationId SpecializationId { get; set; }
        public List<AvailabilitySlotDTO> AvailabilitySlots { get; set; }

        public StaffDTO()
        {
            AvailabilitySlots = new List<AvailabilitySlotDTO>();
        }
    }

    public class AvailabilitySlotDTO
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
