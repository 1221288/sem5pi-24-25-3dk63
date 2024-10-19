using Backend.Domain.Staff.ValueObjects;
using DDDSample1.Domain.Staff;
using DDDSample1.Domain.Users;
using DDDSample1.Domain.Specialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace DDDSample1.Infrastructure.Staffs
{
    public class StaffEntityTypeConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            // Define the primary key as the LicenseNumber (Id in the Staff class)
            builder.HasKey(s => s.Id);

            // Map the LicenseNumber as the primary key (Id)
            builder.Property(s => s.Id)
                .HasColumnName("LicenseNumber")
                .HasConversion(
                    id => id.AsString(),      // Convert the LicenseNumber to a string
                    idString => new LicenseNumber(idString)) // Convert it back to LicenseNumber
                .IsRequired();

            // Configure UserId as a foreign key
            builder.Property(s => s.UserId)
                .HasColumnName("UserId")
                .HasConversion(
                    userId => userId.AsString(), // Convert UserId to a string for the database
                    userIdString => new UserId(userIdString)) // Convert it back to UserId
                .IsRequired();

            // Configure SpecializationId as a foreign key
            builder.Property(s => s.SpecializationId)
                .HasColumnName("SpecializationId")
                .HasConversion(
                    specializationId => specializationId.AsString(), // Convert SpecializationId to a string for the database
                    specializationIdString => new SpecializationId(specializationIdString)) // Convert it back to SpecializationId
                .IsRequired();

            // Configure AvailabilitySlots as a serialized value
            builder.Property(s => s.AvailabilitySlots)
                .HasConversion(
                    availabilitySlots => availabilitySlots != null ? availabilitySlots.SerializeSlots() : null, // Serialize AvailabilitySlots to JSON string
                    json => AvailabilitySlots.DeserializeSlots(json)) // Deserialize JSON string back to AvailabilitySlots
                .HasColumnName("AvailabilitySlots")
                .IsRequired(false);
        }
    }
}
