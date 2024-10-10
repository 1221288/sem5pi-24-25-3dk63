using DDDSample1.Domain.Shared;
using System.Collections.Generic;

namespace DDDSample1.Domain.Users
{
    public class Username : ValueObject
    {
        public string Value { get; private set; }

        public Username(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new BusinessRuleValidationException("Username cannot be empty.");
            }
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}
