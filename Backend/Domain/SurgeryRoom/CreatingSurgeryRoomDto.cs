using System.Collections.Generic;
using DDDSample1.Domain.SurgeryRooms.ValueObjects;

namespace Backend.Domain.SurgeryRoom
{
    public class CreatingSurgeryRoomDto
    {
        public RoomNumber RoomNumber { get; set; }
        public RoomType Type { get; set; }
        public Capacity Capacity { get; set; }
        public List<Equipment> AssignedEquipment { get; set; }
        public RoomStatus CurrentStatus { get; set; }
        public List<MaintenanceSlot> MaintenanceSlots { get; set; }

        public CreatingSurgeryRoomDto(RoomNumber roomNumber, RoomType type, Capacity capacity, List<Equipment> assignedEquipment, RoomStatus currentStatus, List<MaintenanceSlot> maintenanceSlots)
        {
            RoomNumber = roomNumber;
            Type = type;
            Capacity = capacity;
            AssignedEquipment = assignedEquipment;
            CurrentStatus = currentStatus;
            MaintenanceSlots = maintenanceSlots;
        }
    }
}
