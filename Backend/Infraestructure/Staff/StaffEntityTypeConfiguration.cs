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
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasColumnName("LicenseNumber")
                .HasConversion(
                    id => id.AsString(),
                    idString => new LicenseNumber(idString)) 
                .IsRequired();

            builder.Property(s => s.UserId)
                .HasColumnName("UserId")
                .HasConversion(
                    userId => userId.AsString(),
                    userIdString => new UserId(userIdString))
                .IsRequired();

            builder.Property(s => s.SpecializationId)
                .HasColumnName("SpecializationId")
                .HasConversion(
                    specializationId => specializationId.AsString(),
                    specializationIdString => new SpecializationId(specializationIdString))
                .IsRequired();

            // Configure AvailabilitySlots as a serialized value
            builder.Property(s => s.AvailabilitySlots)
                .HasConversion(
                    availabilitySlots => availabilitySlots != null ? availabilitySlots.SerializeSlots() : null,
                    json => AvailabilitySlots.DeserializeSlots(json))
                .HasColumnName("AvailabilitySlots")
                .IsRequired(false);
        }
    }
}
