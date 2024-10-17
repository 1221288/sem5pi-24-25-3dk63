using Backend.Domain.Staff.ValueObjects;
using DDDSample1.Domain.Specialization;
using DDDSample1.Domain.Users;

namespace DDDSample1.Domain.Staff
{
    public class CreatingStaffDTO
    {
        public string LicenseNumber { get; set; }
        public UserId UserId { get; set; }
        public SpecializationId SpecializationId { get; set; }
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
                LicenseNumber = staff.Id,
                UserId = staff.UserId,
                SpecializationId = staff.SpecializationId,
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

            return new Staff(dto.UserId, new LicenseNumber(dto.LicenseNumber), dto.SpecializationId, availabilitySlots);
        }
    }
}
