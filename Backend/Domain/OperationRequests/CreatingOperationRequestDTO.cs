using DDDSample1.Domain.OperationRequests;
using DDDSample1.Domain.OperationsType;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Staff;


namespace DDDSample1.OperationRequests
{
    public class CreatingOperationRequestDTO
    {

    public Deadline Deadline { get; set; }
    public Priority Priority { get; set; }
    public LicenseNumber LicenseNumber { get; set; }
    public MedicalRecordNumber MedicalRecordNumber { get; set; }
    public OperationTypeId OperationTypeId { get; set; }

    public CreatingOperationRequestDTO(Deadline deadline, Priority priority, LicenseNumber licenseNumber, MedicalRecordNumber medicalRecordNumber, OperationTypeId operationTypeId)
    {
        this.Deadline = deadline;
        this.Priority = priority;
        this.LicenseNumber = licenseNumber;
        this.MedicalRecordNumber = medicalRecordNumber;
        this.OperationTypeId = operationTypeId;

    }
}
}
