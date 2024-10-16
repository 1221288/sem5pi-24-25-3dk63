using System;
using DDDSample1.Domain.SurgeryRooms.ValueObjects;

namespace Backend.Domain.SurgeryRoom
{
    public class SurgeryRoomDTO
    {
        public Guid RoomId { get; set; }
        public RoomNumber RoomNumber { get; set; }
        public RoomType Type { get; set; }
        public Capacity Capacity { get; set; }
        public List<Equipment> AssignedEquipment { get; set; }
        public RoomStatus CurrentStatus { get; set; }
        public List<MaintenanceSlot> MaintenanceSlots { get; set; }
    }
}
