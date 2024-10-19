using DDDSample1.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDSample1.Infraestructure.Patients
{
    public class PatientEntityTypeConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
             // Define the primary key for the Patient entity
            builder.HasKey(b => b.Id);

            // Configure the DateOfBirth value object
            builder.OwnsOne(p => p.dateOfBirth, dob =>
            {
                dob.Property(d => d.value)
                   .HasColumnName("DateOfBirth")
                   .IsRequired();
            });

            // Configure the Gender value object
            builder.OwnsOne(p => p.gender, gender =>
            {
                gender.Property(g => g.gender)
                      .HasColumnName("Gender")
                      .IsRequired();
            });

            // Configure the EmergencyContact value object
            builder.OwnsOne(p => p.emergencyContact, contact =>
            {
                contact.Property(c => c.emergencyContact)
                       .HasColumnName("EmergencyContactName")
                       .IsRequired();
            });

            // Configure AllergiesMedicalConditionals as an owned collection
            builder.OwnsMany(p => p.allergiesMedicalConditionalsList, a =>
            {
                a.Property<int>("Id"); // Shadow property as primary key
                a.HasKey("Id"); // Define primary key for the owned entity

                a.Property(a => a.allergyName)
                 .HasColumnName("AllergyName")
                 .IsRequired();

                a.Property(a => a.severity)
                 .HasColumnName("Severity")
                 .IsRequired();
            });

            // Similarly, configure AppointmentHistory if it's also a value object
            builder.OwnsMany(p => p.appointmentHistoryList, a =>
            {
                a.Property<int>("Id"); // Shadow property as primary key
                a.HasKey("Id"); // Define primary key for the owned entity
            });
        }
    }
}
