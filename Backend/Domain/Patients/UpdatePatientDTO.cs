
namespace DDDSample1.Domain.Patients
{

public class UpdatePatientDTO
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Role { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public List<string>? Allergies { get; set; }
    public EmergencyContact? EmergencyContact { get; set; }
    public List<AppointmentHistory>? AppointmentHistoryList { get; set; }
}
}
