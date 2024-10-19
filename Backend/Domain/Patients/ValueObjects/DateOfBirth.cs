using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Patients
{
    public class DateOfBirth : ValueObject
    {
        public DateTime value { get; private set; }

        public DateOfBirth(DateTime value)
        {
            this.value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return value;
        }
    }
}