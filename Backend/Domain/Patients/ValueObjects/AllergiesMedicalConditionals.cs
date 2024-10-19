using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Patients
{
    public class AllergiesMedicalConditionals : ValueObject
    {
        public string allergyName { get; private set; }
        public string severity { get; private set; }
        public string description { get; private set; }

        public AllergiesMedicalConditionals(string allergyName, string severity, string description)
        {
            this.allergyName = allergyName;
            this.severity = severity;
            this.description = description;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return allergyName;
        }
    }
}