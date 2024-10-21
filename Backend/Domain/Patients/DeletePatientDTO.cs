namespace DDDSample1.Domain.Patients
{

    public class DeletePatientDTO
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Role { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}