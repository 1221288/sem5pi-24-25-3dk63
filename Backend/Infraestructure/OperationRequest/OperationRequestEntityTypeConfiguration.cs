using DDDSample1.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDSample1.Infraestructure.OperationTypes
{
    public class OperationRequestEntityTypeConfiguration : IEntityTypeConfiguration<OperationRequest>
    {
        public void Configure(EntityTypeBuilder<OperationRequest> builder)
        {
            // Define a chave primária
            builder.HasKey(o => o.Id);

            // Configura a propriedade Priority (associada ao Value Object Priority)
            builder.OwnsOne(o => o.priority, priority =>
            {
                priority.Property(p => p.Value)
                    .HasColumnName("Priority")
                    .IsRequired();
            });

            // Configura a propriedade Deadline (associada ao Value Object Deadline)
            builder.OwnsOne(o => o.deadline, deadline =>
            {
                deadline.Property(d => d.Value)
                    .HasColumnName("Deadline")
                    .IsRequired();
            });

            // Configura a propriedade LicenseNumber (associada ao Value Object LicenseNumber)
            builder.OwnsOne(o => o.licenseNumber, licenseNumber =>
            {
                licenseNumber.Property(l => l.Value)
                    .HasColumnName("LicenseNumber")
                    .IsRequired();
            });

            // Configura a propriedade OperationTypeId (que é uma referência a outra entidade)
            builder.Property(o => o.operationTypeId)
                .HasColumnName("OperationTypeId")
                .IsRequired();

            // Configura a propriedade MedicalRecordNumber (número simples, não um Value Object)
            builder.Property(o => o.medicalRecordNumber)
                .IsRequired();

            // Configura a propriedade Active (simples, bool)
            builder.Property(o => o.Active)
                .IsRequired();

            // Define o nome da tabela no banco de dados
            builder.ToTable("OperationRequests");
        }
    }
}
