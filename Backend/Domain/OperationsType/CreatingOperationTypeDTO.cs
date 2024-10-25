using Backend.Domain.Users.ValueObjects;
using DDDSample1.Domain.OperationsType;


namespace DDDSample1.OperationsType
{
    public class CreatingOperationTypeDTO
    {

    public string Name { get; set; }
    public int Preparation { get; set; }
    public int Surgery { get; set; }
    public int Cleaning { get; set; }
    public int Duration { get; set; }
    public int RequiredStaff { get; set; }
    public String speciality { get; set; }

    public CreatingOperationTypeDTO(string Name, int cleaning, int preparation, int surgery,int duration ,int requiredStaff, String speciality)
    {
        this.Name = Name;
        this.Cleaning = cleaning;
        this.Preparation = preparation;
        this.Surgery = surgery;
        this.Duration = duration;
        this.RequiredStaff = requiredStaff;
        this.speciality = speciality;
    }
    }
}
