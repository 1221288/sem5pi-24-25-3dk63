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

            // Configura a propriedade pRIOridade (associada ao Value Object Name)
        
            builder.OwnsOne(o => o.priority, priority =>
            {
                priority.Property(p => p.Value)

                    .HasColumnName("Priority")

                    .IsRequired();
            });

            // Configura a propriedade Deadline (associada ao Value Object Duration)
            builder.OwnsOne(o => o.deadline, deadline =>
            {
                deadline.Property(d => d.Value)

                    .HasColumnName("Deadline")

                    .IsRequired();

            });

            // Configura a propriedade LicenseNumber (associada ao Value Object RequiredStaffBySpecialization)
            builder.OwnsOne(o => o.licenseNumber, licenseNumber =>
            {
                licenseNumber.Property(l => l.Value) // Aqui, depende de como RequiredStaff foi implementado.

                    .HasColumnName("LicenseNumber")

                    .IsRequired();
            });

            // Configura a propriedade OperationTypeId (associada ao Value Object RequiredStaffBySpecialization)
            builder.OwnsOne(o => o.operationTypeId, operationTypeId =>
            {
                operationTypeId.Property(o => o.Value) // Aqui, depende de como RequiredStaff foi implementado.


                    .HasColumnName("OperationTypeId")

                    .IsRequired();
            });

            // Configura a propriedade MedicalRecordNumber (associada ao Value Object RequiredStaffBySpecialization)
            builder.Property(o => o.medicalRecordNumber)
                .IsRequired();


                

            // Configura o campo Active
            builder.Property(o => o.Active)
                .IsRequired();

            // Define o nome da tabela no banco de dados (opcional)
            builder.ToTable("OperationRequests");
        }
    }
}
