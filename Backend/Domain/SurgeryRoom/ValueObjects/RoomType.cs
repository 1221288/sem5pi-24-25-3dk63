using DDDSample1.Domain.Shared;
using System;

namespace DDDSample1.Domain.SurgeryRooms.ValueObjects
{
    public class RoomType : ValueObject
    {
        public string Value { get; private set; }

        public RoomType(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new BusinessRuleValidationException("Room type cannot be empty.");
            }

            this.Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
