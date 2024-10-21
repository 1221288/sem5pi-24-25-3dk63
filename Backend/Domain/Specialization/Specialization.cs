using Backend.Domain.Specialization.ValueObjects;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Specialization
{
    public class Specialization : Entity<SpecializationId>, IAggregateRoot
    {
        public Description Description { get; private set; }
        public int SequentialNumber { get; private set; }

        protected Specialization() {}

        public Specialization(SpecializationId specializationId, Description description, int sequentialNumber)
        {
            Id = specializationId ?? throw new ArgumentNullException(nameof(specializationId));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            SequentialNumber = sequentialNumber;
        }

        public void UpdateDescription(Description newDescription)
        {
            Description = newDescription ?? throw new ArgumentNullException(nameof(newDescription));
        }

        public void UpdateSequentialNumber(int newSequentialNumber)
        {
            SequentialNumber = newSequentialNumber;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Specialization other)
            {
                return this.Description.Equals(other.Description);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Description.GetHashCode();
        }
    }
}
