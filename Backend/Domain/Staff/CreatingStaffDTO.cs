using Backend.Domain.Staff.ValueObjects;
using DDDSample1.Domain.Users;

namespace DDDSample1.Domain.Staff
{
    public class CreatingStaffDTO
    {
        public string LicenseNumber { get; set; }
        public UserId UserId { get; set; }
        public List<AvailabilitySlotDTO> AvailabilitySlots { get; set; }

        public CreatingStaffDTO()
        {
            LicenseNumber = string.Empty;
            UserId = new UserId(Guid.NewGuid());
            AvailabilitySlots = new List<AvailabilitySlotDTO>();
        }

        public static StaffDTO CreateFromDomain(Staff staff)
        {
            return new StaffDTO
            {
                LicenseNumber = staff.Id,
                UserId = staff.UserId,
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

            return new Staff(new UserId(dto.UserId.Value), new LicenseNumber(dto.LicenseNumber), availabilitySlots);
        }
    }
}
