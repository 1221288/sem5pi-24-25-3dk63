using DDDSample1.Domain.Shared;
using System.Collections.Generic;
using System.Linq;

namespace DDDSample1.Domain.Users
{
    public class Role : ValueObject
    {
        public string Value { get; private set; }

        private static readonly string[] ValidRoles = { "Admin", "Doctor", "Nurse", "Technician", "Patient" };

        public Role(string value)
        {
            if (!ValidRoles.Contains(value))
            {
                throw new BusinessRuleValidationException($"Invalid role: {value}. Must be one of: {string.Join(", ", ValidRoles)}");
            }
            this.Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}
