using DDDSample1.Domain.PendingChange;
using DDDSample1.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDSample1.Infrastructure.PendingChange
{
    public class PendingChangesEntityTypeConfiguration : IEntityTypeConfiguration<PendingChanges>
    {
        public void Configure(EntityTypeBuilder<PendingChanges> builder)
        {
            builder.HasKey(pc => pc.UserId);

            builder.Property(pc => pc.UserId)
                .HasColumnName("UserId")
                .HasConversion(
                    userId => userId.AsString(),
                    userIdString => new UserId(userIdString))
                .IsRequired();
                
            builder.HasIndex(pc => pc.UserId).IsUnique();

            builder.Property(u => u.Email)
                .HasConversion(
                    email => email.ToString(),
                    emailString => new Email(emailString))
                .IsRequired();

            builder.OwnsOne(u => u.Name, name =>
            {
                name.Property(n => n.FirstName)
                    .HasColumnName("FirstName")
                    .IsRequired();

                name.Property(n => n.LastName)
                    .HasColumnName("LastName")
                    .IsRequired();

                name.Ignore(n => n.FullName);
            });

            builder.OwnsOne(u => u.PhoneNumber, pn =>
            {
                pn.Property(p => p.Number)
                  .HasColumnName("PhoneNumber")
                  .IsRequired()
                  .IsUnicode();
            });

            builder.OwnsOne(p => p.EmergencyContact, contact =>
            {
                contact.Property(c => c.emergencyContact)
                       .HasColumnName("EmergencyContactName")
                       .IsRequired();
            });

            builder.OwnsOne(p => p.Allergy, contact =>
            {
                contact.Property(c => c.allergy)
                       .HasColumnName("Allergy");
            });
        }
    }
}
