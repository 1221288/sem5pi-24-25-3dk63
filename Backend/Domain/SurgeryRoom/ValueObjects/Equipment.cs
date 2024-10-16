// using DDDSample1.Domain.Shared;
// using System;

// namespace DDDSample1.Domain.SurgeryRooms.Entities
// {
//     public class Equipment : Entity<EquipmentId>
//     {
//         public string Name { get; private set; }
//         public string SerialNumber { get; private set; }

//         public Equipment(string name, string serialNumber)
//         {
//             if (string.IsNullOrWhiteSpace(name))
//             {
//                 throw new BusinessRuleValidationException("Equipment name cannot be empty.");
//             }

//             if (string.IsNullOrWhiteSpace(serialNumber))
//             {
//                 throw new BusinessRuleValidationException("Serial number cannot be empty.");
//             }

//             this.Id = new EquipmentId(Guid.NewGuid());
//             this.Name = name;
//             this.SerialNumber = serialNumber;
//         }
//     }

//     public class EquipmentId : EntityId
//     {
//         public EquipmentId(Guid value) : base(value) { }
//     }
// }
