using Backend.Domain.Users.ValueObjects;

namespace DDDSample1.Domain.OperationsType
{
    public class OperationTypeDTO
    {
        public Guid Id { get; set; }
        public Name Name { get; set; }
        public Duration Duration { get; set; }
        public List<StaffSpecialization> RequiredStaff { get; set; }
        public bool Active { get; set; }
    }
}
 
