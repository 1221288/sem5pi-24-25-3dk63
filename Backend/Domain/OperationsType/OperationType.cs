using DDDSample1.Domain.Shared;
using Backend.Domain.Users.ValueObjects;
using DDDSample1.Domain.OperationsType;


namespace DDDSample1.Domain
{

    public class OperationType : Entity<OperationTypeId>, IAggregateRoot
    {
        public Name Name { get; private set; }
        public Duration Duration { get; private set; }
        public List<StaffSpecialization> RequiredStaff { get; private set; }
        public bool Active { get; private set; }

    private OperationType()
    {
        this.Active = true;
    }

    public OperationType(Name name, Duration duration, List<StaffSpecialization> requiredStaff)
        {
            this.Id = new OperationTypeId(Guid.NewGuid());
            this.Active = true;
            this.Name = name;
            this.Duration = duration;
            this.RequiredStaff = requiredStaff ?? throw new ArgumentNullException(nameof(requiredStaff), "RequiredStaff list cannot be null");
        }

    public void ChangeName(Name name)
    {
        if (!this.Active) throw new BusinessRuleValidationException("Operation type cannot be changed in this state");
        this.Name = name;
    }

    public void ChangeDuration(Duration duration)
    {
        if (!this.Active) throw new BusinessRuleValidationException("Operation type cannot be changed in this state");
        this.Duration = duration;
    }

     // Método para mudar o RequiredStaff (lista completa)
         // Método para mudar a lista completa de RequiredStaff (substituição)
        public void ChangeRequiredStaff(List<StaffSpecialization> requiredStaff)
        {
            if (!this.Active) throw new BusinessRuleValidationException("Operation type cannot be changed in this state");
            this.RequiredStaff = requiredStaff ?? throw new ArgumentNullException(nameof(requiredStaff), "RequiredStaff list cannot be null");
        }
    }
}

    

