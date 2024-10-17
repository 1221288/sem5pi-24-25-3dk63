using Backend.Domain.Staff.ValueObjects;
using DDDSample1.Domain.Staff;
using DDDSample1.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace DDDSample1.Infrastructure.StaffS
{
    public class StaffEntityTypeConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasConversion(
                    licenseNumber => licenseNumber.ToString(),
                    licenseNumberString => new LicenseNumber(licenseNumberString))
                .HasColumnName("LicenseNumber")
                .IsRequired();

            builder.Property(s => s.UserId)
                .HasConversion(
                    userId => userId.ToString(),
                    userIdString => new UserId(Guid.Parse(userIdString)))
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(s => s.AvailabilitySlots)
                .HasConversion(
                    availabilitySlots => availabilitySlots.SerializeSlots(),
                    json => AvailabilitySlots.DeserializeSlots(json))
                .HasColumnName("AvailabilitySlots")
                .IsRequired(false);
        }
    }
}
