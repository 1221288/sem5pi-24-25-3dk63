using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Patients
{
    public class AppointmentHistory : ValueObject
    {
        public DateTime appointmentDate { get; private set; }
        public string doctorName { get; private set; }

        public AppointmentHistory(DateTime appointmentDate, string doctorName)
        {
            this.appointmentDate = appointmentDate;
            this.doctorName = doctorName;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return appointmentDate;
        }
    }
}