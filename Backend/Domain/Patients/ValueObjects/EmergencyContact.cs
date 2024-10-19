using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Patients
{
    public class EmergencyContact : ValueObject
    {
        public string emergencyContact { get; private set; }

        public EmergencyContact(string emergencyContact)
        {
            this.emergencyContact = emergencyContact;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return emergencyContact;
        }
    }
}