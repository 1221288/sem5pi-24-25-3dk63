using Backend.Domain.Staff.ValueObjects;
using System;
using System.Collections.Generic;

namespace DDDSample1.Domain.Staff
{
    public class StaffDTO
    {
        public LicenseNumber LicenseNumber { get; set; }
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
