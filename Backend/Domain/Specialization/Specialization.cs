using Backend.Domain.Specialization.ValueObjects;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Staff;

namespace DDDSample1.Domain.Specialization
{
    public class Specialization : Entity<LicenseNumber>, IAggregateRoot
    {
        public Description Description { get; private set; }

        public Specialization(LicenseNumber licenseNumber, Description description)
        {
            Id = licenseNumber;
            Description = description;
        }

        public void UpdateDescription(Description newDescription)
        {
            Description = newDescription;
        }
    }
}
