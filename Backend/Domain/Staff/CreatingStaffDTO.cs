using Backend.Domain.Staff.ValueObjects;

public class CreatingStaffDTO
{
    public string LicenseNumber { get; set; }
    public string SpecializationDescription { get; set; }
    public List<AvailabilitySlot>? AvailabilitySlots { get; set; }
    public string Email { get; set; } 
    public string Role { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }

    public CreatingStaffDTO(
        string licenseNumber, 
        string specializationDescription,
        List<AvailabilitySlot> availabilitySlots,
        string email, 
        string role, 
        string firstName,
        string lastName, 
        string phoneNumber)
    {
        this.LicenseNumber = licenseNumber;
        this.SpecializationDescription = specializationDescription;
        this.AvailabilitySlots = availabilitySlots;
        this.Email = email;
        this.Role = role;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.PhoneNumber = phoneNumber;
    }
}
