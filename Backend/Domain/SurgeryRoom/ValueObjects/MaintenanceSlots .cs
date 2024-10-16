// using DDDSample1.Domain.Shared;
// using System;

// namespace DDDSample1.Domain.SurgeryRooms.Entities
// {
//     public class MaintenanceSlot : Entity<MaintenanceSlotId>
//     {
//         public DateTime StartTime { get; private set; }
//         public DateTime EndTime { get; private set; }

//         public MaintenanceSlot(DateTime startTime, DateTime endTime)
//         {
//             if (startTime >= endTime)
//             {
//                 throw new BusinessRuleValidationException("End time must be after start time.");
//             }

//             this.Id = new MaintenanceSlotId(Guid.NewGuid());
//             this.StartTime = startTime;
//             this.EndTime = endTime;
//         }
//     }

//     public class MaintenanceSlotId : EntityId
//     {
//         public MaintenanceSlotId(Guid value) : base(value) { }
//     }
// }
