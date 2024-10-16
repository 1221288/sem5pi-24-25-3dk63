// using DDDSample1.Domain.SurgeryRooms.Entities;
// using DDDSample1.Domain.SurgeryRooms.ValueObjects;
// using DDDSample1.Domain.Shared;
// using System;
// using System.Collections.Generic;
// using Backend.Domain.SurgeryRoom;

// namespace DDDSample1.Domain.SurgeryRooms
// {
//     public class SurgeryRoom : Entity<RoomId>, IAggregateRoot
//     {
//         public RoomNumber RoomNumber { get; private set; }
//         public RoomType Type { get; private set; }
//         public Capacity Capacity { get; private set; }
//         public List<Equipment> AssignedEquipment { get; private set; }
//         public RoomStatus CurrentStatus { get; private set; }
//         public List<MaintenanceSlot> MaintenanceSlots { get; private set; }

//         public SurgeryRoom(RoomNumber roomNumber, RoomType type, Capacity capacity, List<Equipment> assignedEquipment, RoomStatus status)
//         {
//             this.Id = new RoomId(Guid.NewGuid());
//             this.RoomNumber = roomNumber;
//             this.Type = type;
//             this.Capacity = capacity;
//             this.AssignedEquipment = assignedEquipment ?? new List<Equipment>();
//             this.CurrentStatus = status;
//             this.MaintenanceSlots = new List<MaintenanceSlot>();
//         }

//         public void ScheduleMaintenance(MaintenanceSlot slot)
//         {
//             if (this.CurrentStatus.Value != "Available")
//             {
//                 throw new BusinessRuleValidationException("Room is not available for maintenance.");
//             }
//             this.MaintenanceSlots.Add(slot);
//             this.CurrentStatus = new RoomStatus("Under Maintenance");
//         }
//     }
// }
