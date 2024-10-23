using DDDSample1.Domain;
using DDDSample1.Domain.OperationsType;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDSample1.Infraestructure.OperationTypes
{
    public class OperationTypeEntityTypeConfiguration : IEntityTypeConfiguration<OperationType>
    {
        public void Configure(EntityTypeBuilder<OperationType> builder)
        {
            // Define a chave primária
            builder.HasKey(o => o.Id);

            // Configura a propriedade Name (associada ao Value Object Name)
            builder.OwnsOne(o => o.Name, name =>
            {
                name.Property(n => n.FirstName)
                    .HasColumnName("OperationFirstName")
                    .IsRequired();

                name.Property(n => n.LastName)
                    .HasColumnName("OperationLastName")
                    .IsRequired();
            });


            builder.OwnsOne(o => o.Duration, duration =>
            {
                duration.Property(d => d.PreparationPhase)
                    .HasColumnName("PreparationPhaseDuration")
                    .IsRequired();

                duration.Property(d => d.SurgeryPhase)
                    .HasColumnName("SurgeryPhaseDuration")
                    .IsRequired();

                duration.Property(d => d.CleaningPhase)
                    .HasColumnName("CleaningPhaseDuration")
                    .IsRequired();

                duration.Property(d => d.TotalDuration)
                    .HasColumnName("TotalDuration")
                    .IsRequired();
            });  

            // Configura a coleção de RequiredStaff (associada à lista de StaffSpecialization)
            builder.OwnsMany(o => o.RequiredStaff, staff =>
            {
                staff.WithOwner().HasForeignKey("OperationTypeId"); // Configura a chave estrangeira
                staff.Property(s => s.Specialization)
                    .HasColumnName("Specialization")
                    .IsRequired();

                staff.Property(s => s.RequiredNumber)
                    .HasColumnName("RequiredNumber")
                    .IsRequired();

                staff.ToTable("RequiredStaffBySpecialization"); // Define a tabela para a coleção
            });

            // Configura o campo Active
            builder.Property(o => o.Active)
                .IsRequired();

            // Define o nome da tabela no banco de dados (opcional)
            builder.ToTable("OperationTypes");
        }
    }
}
