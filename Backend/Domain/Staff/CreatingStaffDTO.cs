using Backend.Domain.Staff.ValueObjects;
using DDDSample1.Domain.Specialization;
using DDDSample1.Domain.Users;

namespace DDDSample1.Domain.Staff
{
    public class CreatingStaffDTO
    {
        public LicenseNumber LicenseNumber { get; set; }
        public UserId UserId { get; set; }
        public SpecializationId SpecializationId { get; set; }
        public List<AvailabilitySlot> AvailabilitySlots { get; set; }

        public CreatingStaffDTO(string licenseNumber, string userId, string specializationId, List<AvailabilitySlot> availabilitySlots)
        {
            LicenseNumber = new LicenseNumber(licenseNumber);
            UserId = new UserId(userId);
            SpecializationId = new SpecializationId(specializationId);
            AvailabilitySlots = availabilitySlots ?? new List<AvailabilitySlot>();

        }

        public Staff CreateDomainFromDTO()
        {
            var availabilitySlots = new AvailabilitySlots();
            foreach (var slot in AvailabilitySlots)
            {
                availabilitySlots.AddSlot(slot.Start, slot.End);
            }

            return new Staff(
                UserId,
                LicenseNumber,
                SpecializationId,
                availabilitySlots
            );
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
    }
}
