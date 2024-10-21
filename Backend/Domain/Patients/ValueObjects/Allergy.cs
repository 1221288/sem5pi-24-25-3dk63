using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Patients
{
    public class Allergy : ValueObject
    {
        public string allergy { get; private set; }

        public Allergy(string allergy)
        {
            this.allergy = allergy;
 
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return allergy;
        }
    }
}