using Backend.Domain.Users.ValueObjects;
using DDDSample1.Domain.Appointments;
using DDDSample1.Domain.OperationRequests;
using DDDSample1.Domain.OperationsType;
using DDDSample1.Domain.Staff;

namespace DDDSample1.Domain.Appointments
{
    public class AppointmentDTO
    {
        public Guid Id { get; set; }
        public Date date { get; set; }
        public Time time { get; set; }
        public OperationRequestId operationRequestId { get; set; }
        public int roomNumber { get; set; }

    }
}
 
