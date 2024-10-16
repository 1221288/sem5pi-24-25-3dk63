using DDDSample1.Domain.Shared;
using Backend.Domain.Staff.ValueObjects;

namespace DDDSample1.Domain.Staff
{
    public class Staff : Entity<LicenseNumber>, IAggregateRoot
    {
        public AvailabilitySlots? AvailabilitySlots { get; private set; }

        public Staff(LicenseNumber licenseNumber, AvailabilitySlots availabilitySlots)
        {
            this.Id = licenseNumber;
            this.AvailabilitySlots = availabilitySlots;
        }

        public Staff(LicenseNumber licenseNumber)
        {
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
