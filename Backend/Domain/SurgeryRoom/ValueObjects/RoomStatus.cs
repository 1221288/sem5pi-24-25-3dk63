using DDDSample1.Domain.Shared;
using System;

namespace DDDSample1.Domain.SurgeryRooms.ValueObjects
{
    public class RoomStatus : ValueObject
    {
        public string Value { get; private set; }

        private static readonly List<string> ValidStatuses = new List<string> { "Available", "Occupied", "Under Maintenance" };

        public RoomStatus(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !ValidStatuses.Contains(value))
            {
                throw new BusinessRuleValidationException("Invalid room status.");
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
