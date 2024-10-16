using Backend.Domain.Staff.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace DDDSample1.Domain.Staff
{
    public class CreatingStaffDTO
    {
        public string LicenseNumber { get; set; }
        public List<AvailabilitySlotDTO> AvailabilitySlots { get; set; }

        public CreatingStaffDTO()
        {
            LicenseNumber = string.Empty;
            AvailabilitySlots = new List<AvailabilitySlotDTO>();
        }

        public static StaffDTO CreateFromDomain(Staff staff)
        {
            return new StaffDTO
            {
                LicenseNumber = new LicenseNumber(staff.Id.AsString()),
                AvailabilitySlots = staff.AvailabilitySlots?.Slots
                    .Select(slot => new AvailabilitySlotDTO
            {   
                Start = slot.Start,
                End = slot.End
            }).ToList() ?? new List<AvailabilitySlotDTO>()
            };
        }

        public static Staff CreateDomainFromDTO(CreatingStaffDTO dto)
        {
            var availabilitySlots = new AvailabilitySlots();
            foreach (var slot in dto.AvailabilitySlots)
            {
                availabilitySlots.AddSlot(slot.Start, slot.End);
            }

            return new Staff(new LicenseNumber(dto.LicenseNumber), availabilitySlots);
        }
    }
}
