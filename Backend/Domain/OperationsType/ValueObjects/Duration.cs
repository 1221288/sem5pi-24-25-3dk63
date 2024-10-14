using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.OperationsType
{
    public class Duration : ValueObject
    {
        public int Value { get; private set; }

        public Duration(int durationValue)
        {
            this.Value = durationValue;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}