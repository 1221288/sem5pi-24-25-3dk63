using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            // cf. https://www.entityframeworktutorial.net/efcore/fluent-api-in-entity-framework-core.aspx

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Username)
                    .HasConversion(
                        username => username.ToString(),
                        usernameString => new Username(usernameString))
                    .IsRequired();

            builder.Property(u => u.Email)
                    .HasConversion(
                        email => email.ToString(),
                        emailString => new Email(emailString))
                    .IsRequired();

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

            builder.Property(u => u.Active).IsRequired();
            builder.Property(u => u.SequentialNumber).IsRequired();
            }
    }
}