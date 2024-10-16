using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Appointments
{
    public class Time : ValueObject
    {
        public TimeSpan Value { get; private set; }

        public Time(TimeSpan value)
        {
            if (value < TimeSpan.Zero || value >= TimeSpan.FromHours(24))
                throw new BusinessRuleValidationException("Time must be between 00:00 and 23:59");

            this.Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString(@"hh\:mm");
    }
}
