using DDDSample1.Domain.Shared;
using System.Collections.Generic;

namespace Backend.Domain.Users.ValueObjects
{
    public class Name : ValueObject
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName => $"{FirstName} {LastName}";

        public Name(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                throw new BusinessRuleValidationException("First and Last names cannot be empty.");
            }
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }

        public override string ToString() => FullName;
    }
}
