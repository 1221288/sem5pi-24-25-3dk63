using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Users.ValueObjects;
using DDDSample1.Domain;
using DDDSample1.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDSample1.Infraestructure.Users
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Username)
                .HasConversion(
                    username => username.ToString(),
                    usernameString => new Username(usernameString))
                .IsRequired();

            builder.HasIndex(u => u.Username).IsUnique();

            builder.Property(u => u.Email)
                .HasConversion(
                    email => email.ToString(),
                    emailString => new Email(emailString))
                .IsRequired();

            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.Role)
                .HasConversion(
                    role => role.ToString(),
                    roleString => new Role(Enum.Parse<RoleType>(roleString)))
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

            builder.Property(u => u.Active).IsRequired();
            builder.Property(u => u.SequentialNumber).IsRequired();

            builder.Property(u => u.ConfirmationToken)
                .IsRequired(false);

            builder.Property(u => u.MarkedForDeletionDate)
                .IsRequired(false);
        }
    }
}
