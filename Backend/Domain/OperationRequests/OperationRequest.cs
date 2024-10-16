using DDDSample1.Domain.Shared;
using DDDSample1.Domain.OperationRequests;
using DDDSample1.Domain.Staff;
using DDDSample1.Domain.OperationsType;


namespace DDDSample1.Domain
{

    public class OperationRequest : Entity<OperationRequestId>, IAggregateRoot
    {
        public Deadline deadline { get; private set; }
        public Priority priority { get; private set; }
        public LicenseNumber licenseNumber  { get; private set; }
        public OperationTypeId operationTypeId { get; private set; }
        public int medicalRecordNumber { get; private set; }
        public bool Active { get; private set; }

    private OperationRequest()
    {
        this.Active = true;
    }

    public OperationRequest(OperationTypeId operationTypeId,Deadline deadline, Priority priority, LicenseNumber licenseNumber, 
    int medicalRecordNumber){
        this.Id = new OperationRequestId(Guid.NewGuid());
        this.Active = true;
        this.deadline=  deadline;
        this.priority = priority;
        this.medicalRecordNumber = medicalRecordNumber;
        this.operationTypeId = operationTypeId;
        this.licenseNumber = licenseNumber;

    }

    public void ChangeDeadLine(Deadline deadline)
    {
        if (!this.Active) throw new BusinessRuleValidationException("Operation request cannot be changed in this state");
        this.deadline = deadline;
    }

    public void ChangePriority(Priority priority)
    {
        if (!this.Active) throw new BusinessRuleValidationException("Operation request cannot be changed in this state");
        this.priority = priority;
    }
}
}

    

