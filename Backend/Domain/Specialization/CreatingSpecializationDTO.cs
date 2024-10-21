using Backend.Domain.Specialization.ValueObjects;

namespace DDDSample1.Domain.Specialization
{
    public class CreatingSpecializationDTO
    {
        public required string Description { get; set; }

        public CreatingSpecializationDTO(string description)
        {
            Description = description;
        }
    }
}

