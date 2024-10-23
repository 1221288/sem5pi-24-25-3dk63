using Backend.Domain.Users.ValueObjects;
using DDDSample1.Domain.OperationsType;


namespace DDDSample1.OperationsType
{
    public class CreatingOperationTypeDTO
    {

    public string FirstName { get; set; }
    public string LastName { get; set; }   
    public int Preparation { get; set; }
    public int Surgery { get; set; }
    public int Cleaning { get; set; }
    public int Duration { get; set; }
    public List<StaffSpecialization> RequiredStaff { get; set; }

    public CreatingOperationTypeDTO(string firstName, string lastName, int cleaning, int preparation, int surgery,int duration ,List<StaffSpecialization> requiredStaff)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Cleaning = cleaning;
        this.Preparation = preparation;
        this.Surgery = surgery;
        this.Duration = duration;
        this.RequiredStaff = requiredStaff ?? new List<StaffSpecialization>();
    }
    }
}
