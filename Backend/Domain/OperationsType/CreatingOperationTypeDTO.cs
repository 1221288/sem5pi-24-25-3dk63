using Backend.Domain.Users.ValueObjects;
using DDDSample1.Domain.OperationsType;


namespace DDDSample1.OperationsType
{
    public class CreatingOperationTypeDTO
    {

    public Name name  { get; set; }
    public Duration duration { get; set; }
    public List<StaffSpecialization> RequiredStaff { get; set; }

    public CreatingOperationTypeDTO(Name name, Duration duration,  List<StaffSpecialization> requiredStaff)
    {
        this.name = name;
        this.duration = duration;
        this.RequiredStaff = requiredStaff ?? new List<StaffSpecialization>();
    }   

    }
}
