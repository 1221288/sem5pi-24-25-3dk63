using DDDSample1.Domain;
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

            // Configura a propriedade Duration (associada ao Value Object Duration)
            builder.OwnsOne(o => o.Duration, duration =>
            {
                duration.Property(d => d.Value)
                    .HasColumnName("Duration")
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
