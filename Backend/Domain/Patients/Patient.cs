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
        public Allergy allergy { get; private set; }
        public EmergencyContact emergencyContact { get; private set; }
        public List<AppointmentHistory> appointmentHistoryList { get; private set; }
        public int sequentialNumber { get; private set; }
        public bool Active { get; private set; }
        private Patient()
        {
            this.Active = false;
            this.appointmentHistoryList = new List<AppointmentHistory>();
        }

        public Patient(DateOfBirth dateOfBirth, Gender gender, EmergencyContact emergencyContact, int sequentialNumber)
        {
            this.Id = new MedicalRecordNumber(MedicalRecordNumber.GenerateNewRecordNumber(dateOfBirth.date, sequentialNumber));
            this.Active = false;
            this.dateOfBirth = dateOfBirth;
            this.gender = gender;
            this.allergy = new Allergy("");
            this.emergencyContact = emergencyContact;
            this.appointmentHistoryList = new List<AppointmentHistory>();
            this.sequentialNumber = sequentialNumber;
        }

        public Patient(DateOfBirth dateOfBirth, Gender gender, EmergencyContact emergencyContact, Allergy allergy, int sequentialNumber)
        {
            this.Id = new MedicalRecordNumber(MedicalRecordNumber.GenerateNewRecordNumber(dateOfBirth.date, sequentialNumber));
            this.Active = false;
            this.dateOfBirth = dateOfBirth;
            this.gender = gender;
            this.allergy = allergy;
            this.emergencyContact = emergencyContact;
            this.appointmentHistoryList = new List<AppointmentHistory>();
            this.sequentialNumber = sequentialNumber;
        }

        public Patient(UserId userId, DateOfBirth dateOfBirth, Gender gender, EmergencyContact emergencyContact, int sequentialNumber)
        {
            this.Id = new MedicalRecordNumber(MedicalRecordNumber.GenerateNewRecordNumber(dateOfBirth.date, sequentialNumber));
            this.UserId = userId;
            this.Active = false;
            this.dateOfBirth = dateOfBirth;
            this.gender = gender;
            this.allergy = new Allergy("");
            this.emergencyContact = emergencyContact;
            this.appointmentHistoryList = new List<AppointmentHistory>();
            this.sequentialNumber = sequentialNumber;
        }

        public Patient(UserId userId, DateOfBirth dateOfBirth, Gender gender, EmergencyContact emergencyContact, Allergy allergy, int sequentialNumber)
        {
            this.Id = new MedicalRecordNumber(MedicalRecordNumber.GenerateNewRecordNumber(dateOfBirth.date, sequentialNumber));
            this.UserId = userId;
            this.Active = false;
            this.dateOfBirth = dateOfBirth;
            this.gender = gender;
            this.allergy = allergy;
            this.emergencyContact = emergencyContact;
            this.appointmentHistoryList = new List<AppointmentHistory>();
            this.sequentialNumber = sequentialNumber;
        }

        public void AddAppointment(AppointmentHistory appointment)
        {
            appointmentHistoryList.Add(appointment);
        }

        public void ChangeActiveTrue()
        {
            this.Active = true;
        }

        public void ChangeActiveFalse()
        {
            this.Active = false;
        }

        public void ChangeAllergy(string allergy)
        {
            this.allergy = new Allergy(allergy);
        }

    }
}
