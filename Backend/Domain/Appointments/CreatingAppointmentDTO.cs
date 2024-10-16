using DDDSample1.Domain.Appointments;
using DDDSample1.Domain.OperationRequests;
using DDDSample1.Domain.OperationsType;
using DDDSample1.Domain.Staff;


namespace DDDSample1.Appointments
{
    public class CreatingAppointmentDTO
    {

    public Date date { get; set; }
    public Time time { get; set; }
    public OperationRequestId operationRequestId { get; set; }
    public int roomNumber { get; set; }

    public CreatingAppointmentDTO(Date date, Time time, OperationRequestId operationRequestId, int roomNumber)
    {
        this.date = date;
        this.time = time;
        this.operationRequestId = operationRequestId;
        this.roomNumber = roomNumber;
    }
}
}
