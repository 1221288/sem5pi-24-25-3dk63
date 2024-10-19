using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Patients;
using System.Collections.Generic;
using DDDSample1.Domain.Users;

namespace DDDSample1.Domain
{
    public class Patient : Entity<MedicalRecordNumber>, IAggregateRoot
    {
        public UserId UserId { get; set; }
        public DateOfBirth dateOfBirth { get; private set; }
        public Gender gender { get; private set; }
        public List<AllergiesMedicalConditionals> allergiesMedicalConditionalsList { get; private set; }
        public EmergencyContact emergencyContact { get; private set; }
        public List<AppointmentHistory> appointmentHistoryList { get; private set; }
        public bool Active { get; private set; }

        private Patient()
        {
            this.Active = true;
            this.allergiesMedicalConditionalsList = new List<AllergiesMedicalConditionals>();
            this.appointmentHistoryList = new List<AppointmentHistory>();
        }

        public Patient(DateOfBirth dateOfBirth, Gender gender, EmergencyContact emergencyContact)
        {
            this.Id = new MedicalRecordNumber(Guid.NewGuid());
            this.Active = false;
            this.dateOfBirth = dateOfBirth;
            this.gender = gender;
            this.allergiesMedicalConditionalsList = new List<AllergiesMedicalConditionals>();
            this.emergencyContact = emergencyContact;
            this.appointmentHistoryList = new List<AppointmentHistory>();
        }

        public Patient(UserId userId, DateOfBirth dateOfBirth, Gender gender, EmergencyContact emergencyContact)
        {
            this.Id = new MedicalRecordNumber(Guid.NewGuid());
            this.UserId = userId;
            this.Active = false;
            this.dateOfBirth = dateOfBirth;
            this.gender = gender;
            this.allergiesMedicalConditionalsList = new List<AllergiesMedicalConditionals>();
            this.emergencyContact = emergencyContact;
            this.appointmentHistoryList = new List<AppointmentHistory>();
        }

        public void AddAllergy(AllergiesMedicalConditionals allergy)
        {
            allergiesMedicalConditionalsList.Add(allergy);
        }

        public void AddAppointment(AppointmentHistory appointment)
        {
            appointmentHistoryList.Add(appointment);
        }

        public void AddUserId(UserId userId)
        {
            if (this.UserId!=null) throw new BusinessRuleValidationException("Patient's user is already registered.");
            this.UserId = userId;
        }
    }
}
