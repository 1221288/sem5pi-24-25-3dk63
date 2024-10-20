using System;
using Backend.Domain.Staff.ValueObjects;

namespace DDDSample1.Domain.Staff
{
    public class StaffDTO
    {
        public Guid LicenseNumber { get; set; }
        public Guid UserId { get; set; }
        public Guid SpecializationId { get; set; }
        public List<AvailabilitySlot> AvailabilitySlots { get; set; }
        public bool Active { get; set; }
    }
}
