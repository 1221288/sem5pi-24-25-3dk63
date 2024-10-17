using DDDSample1.Domain.Shared;
using Backend.Domain.Staff.ValueObjects;
using DDDSample1.Domain.Users;
using DDDSample1.Domain.Specialization;

namespace DDDSample1.Domain.Staff
{
    public class Staff : Entity<LicenseNumber>, IAggregateRoot
    {
        public UserId UserId { get; private set; }
        public SpecializationId SpecializationId { get; private set; }
        public AvailabilitySlots? AvailabilitySlots { get; private set; }

        protected Staff() { }

        public Staff(UserId userId, LicenseNumber licenseNumber, SpecializationId specializationId, AvailabilitySlots availabilitySlots)
        {
            this.UserId = userId;
            this.Id = licenseNumber;
            this.SpecializationId = specializationId;
            this.AvailabilitySlots = availabilitySlots;
        }

        public Staff(UserId userId, LicenseNumber licenseNumber, SpecializationId specializationId)
        {
            this.UserId = userId;
            this.Id = licenseNumber;
            this.SpecializationId = specializationId;
        }

        public void AddAvailabilitySlot(DateTime start, DateTime end)
        {
            if (AvailabilitySlots == null)
            {
                AvailabilitySlots = new AvailabilitySlots();
            }
            AvailabilitySlots.AddSlot(start, end);
        }

        public void UpdateAvailabilitySlots(AvailabilitySlots newAvailabilitySlots)
        {
            AvailabilitySlots = newAvailabilitySlots;
        }
    }
}
