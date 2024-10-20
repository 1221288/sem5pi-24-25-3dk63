using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Patients
{
    public class DateOfBirth : ValueObject
    {
        public DateTime value { get; private set; }

        public DateOfBirth(DateTime date)
        {
            if (date > DateTime.Now)
            {
                throw new ArgumentException("Date of birth cannot be in the future.");
            }

            this.value = date;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return value;
        }

        public override string ToString()
        {
            return value.ToString("yyyy-MM-dd");
        }
    }
}