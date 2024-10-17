using DDDSample1.Domain.Specialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDSample1.Infrastructure.Specializations
{
    public class SpecializationEntityTypeConfiguration : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasConversion(
                    specializationId => specializationId.AsString(),
                    value => new SpecializationId(value))
                .HasColumnName("SpecializationId")
                .IsRequired();

            builder.OwnsOne(s => s.Description, description =>
            {
                description.Property(d => d.Value)
                    .HasColumnName("Description")
                    .IsRequired();
            });

            builder.Property(s => s.SequentialNumber)
                .IsRequired();
        }
    }
}
