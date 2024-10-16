using DDDSample1.Domain.Shared;
using System;
using System.Collections.Generic;

namespace DDDSample1.Domain.SurgeryRooms.ValueObjects
{
    public class RoomType : ValueObject
    {


        public enum RoomTypeEnum
        {
            OperatingRoom,
            ConsultationRoom,
            ICU,
            RecoveryRoom,
            SurgeryRoom,
            RadiologyRoom
        }

        public RoomTypeEnum Type { get; private set; }

        public RoomType(RoomTypeEnum type)
        {
            Type = type;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Type;
        }

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}
