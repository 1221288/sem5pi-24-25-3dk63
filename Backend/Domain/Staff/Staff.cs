using DDDSample1.Domain.Shared;
using Backend.Domain.Staff.ValueObjects;
using DDDSample1.Domain.Users;

namespace DDDSample1.Domain.Staff
{
    public class Staff : Entity<LicenseNumber>, IAggregateRoot
    {
        public UserId UserId { get; private set; }
        public AvailabilitySlots? AvailabilitySlots { get; private set; }

        protected Staff() { }
        public Staff(UserId userId, LicenseNumber licenseNumber, AvailabilitySlots availabilitySlots)
        {
            this.UserId = userId;
            this.Id = licenseNumber;
            this.AvailabilitySlots = availabilitySlots;
        }

        public Staff(UserId userId, LicenseNumber licenseNumber)
        {
            this.UserId = userId;
            this.Id = licenseNumber;
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
