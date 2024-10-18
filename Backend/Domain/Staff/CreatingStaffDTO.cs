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
        public List<AvailabilitySlot> AvailabilitySlots { get; set; }

        public CreatingStaffDTO(string licenseNumber, UserId userId, SpecializationId specializationId, List<AvailabilitySlot> availabilitySlots)
        {
            this.LicenseNumber = licenseNumber;
            this.UserId = userId;
            this.SpecializationId = specializationId;
            this.AvailabilitySlots = availabilitySlots ?? new List<AvailabilitySlot>();
        }

        public static StaffDTO CreateFromDomain(Staff staff)
        {
            return new StaffDTO
            {
                LicenseNumber = staff.Id,
                UserId = staff.UserId,
                SpecializationId = staff.SpecializationId,
                AvailabilitySlots = staff.AvailabilitySlots?.Slots?.ToList() ?? new List<AvailabilitySlot>()
            };
        }

        public static Staff CreateDomainFromDTO(CreatingStaffDTO dto)
        {
            var availabilitySlots = new AvailabilitySlots();
            foreach (var slot in dto.AvailabilitySlots)
            {
                availabilitySlots.AddSlot(slot.Start, slot.End);
            }

            return new Staff(new UserId(dto.UserId.AsGuid()), new LicenseNumber(dto.LicenseNumber), dto.SpecializationId, availabilitySlots);
        }
    }
}
