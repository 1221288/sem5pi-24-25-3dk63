using Backend.Domain.Users.ValueObjects;

namespace DDDSample1.Domain.OperationsType
{
    public class OperationTypeDTO
    {
        public Guid Id { get; set; }
        public Name Name { get; set; }
        public Duration Duration { get; set; }
        public RequiredStaff RequiredStaff { get; set; }
    }
}
 
