using Backend.Domain.Users.ValueObjects;
using DDDSample1.Domain.OperationsType;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Staff;

namespace DDDSample1.Domain.OperationRequests
{
    public class OperationRequestDTO
    {
        public Guid Id { get; set; }
        public Deadline Deadline { get; set; }
        public Priority Priority { get; set; }
        public required LicenseNumber LicenseNumber { get; set; }
        public MedicalRecordNumber MedicalRecordNumber { get; set; }
        public OperationTypeId OperationTypeId { get; set; }

    }
}
 
