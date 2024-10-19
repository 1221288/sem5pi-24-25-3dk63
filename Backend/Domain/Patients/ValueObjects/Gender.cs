using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Patients
{
    public class Gender : ValueObject
    {
        public string gender { get; private set; }

        public Gender(string gender)
        {
            this.gender = gender;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return gender;
        }
    }
}