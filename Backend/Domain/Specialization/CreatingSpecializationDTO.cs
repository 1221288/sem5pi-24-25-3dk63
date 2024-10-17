using Backend.Domain.Specialization.ValueObjects;

namespace DDDSample1.Domain.Specialization
{
    public class CreatingSpecializationDTO
    {
        public required string Description { get; set; }

        public Specialization ToDomain(SpecializationId specializationId, int sequentialNumber)
        {
            return new Specialization(specializationId, new Description(Description), sequentialNumber);
        }

        public static SpecializationDTO FromDomain(Specialization specialization)
        {
            return new SpecializationDTO
            {
                Id = specialization.Id,
                Description = specialization.Description.Value,
                SequentialNumber = specialization.SequentialNumber
            };
        }
    }
}
