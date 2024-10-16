using Backend.Domain.Specialization.ValueObjects;
using DDDSample1.Domain.Staff;

namespace DDDSample1.Domain.Specialization
{
    public class CreatingSpecializationDTO
    {
        public LicenseNumber LicenseNumber { get; set; }
        public string Description { get; set; }

        public Specialization ToDomain()
        {
            return new Specialization(LicenseNumber, new Description(Description));
        }

        public static SpecializationDTO FromDomain(Specialization specialization)
        {
            return new SpecializationDTO
            {
                LicenseNumber = specialization.Id,
                Description = specialization.Description.Value
            };
        }
    }
}
