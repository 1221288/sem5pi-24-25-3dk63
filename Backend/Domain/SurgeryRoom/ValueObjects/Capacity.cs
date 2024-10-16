using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.SurgeryRooms.ValueObjects
{
    public class Capacity : ValueObject
    {
        public int Value { get; private set; }

        public Capacity(int value)
        {
            if (value <= 0)
            {
                throw new BusinessRuleValidationException("Capacity must be greater than zero.");
            }

            this.Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
