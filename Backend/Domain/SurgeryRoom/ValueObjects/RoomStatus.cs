using System;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.SurgeryRooms.ValueObjects
{
    public enum RoomStatusEnum
    {
        Available,
        Occupied,
        UnderMaintenance
    }

    public class RoomStatus : ValueObject
    {
        public RoomStatusEnum Value { get; private set; }

        public RoomStatus(RoomStatusEnum value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
