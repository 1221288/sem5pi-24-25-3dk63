using Backend.Domain.Users.ValueObjects;
using DDDSample1.Domain.OperationsType;


namespace DDDSample1.OperationsType
{
    public class CreatingOperationTypeDTO
    {

    public string FirstName { get; set; }
    public string LastName { get; set; }   
    public Duration duration { get; set; }
    public List<StaffSpecialization> RequiredStaff { get; set; }

    public CreatingOperationTypeDTO(string firstName, string lastName, Duration duration,  List<StaffSpecialization> requiredStaff)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.duration = duration;
        this.RequiredStaff = requiredStaff ?? new List<StaffSpecialization>();
    }   

    }
}
