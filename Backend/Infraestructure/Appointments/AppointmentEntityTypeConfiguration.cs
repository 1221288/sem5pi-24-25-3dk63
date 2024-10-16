using DDDSample1.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDSample1.Infraestructure.Appointments
{
    public class AppointmentEntityTypeConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            // Define a chave primária
            builder.HasKey(o => o.Id);

            // Configura a propriedade Priority (associada ao Value Object Priority)
            builder.OwnsOne(o => o.date, date =>
            {
                date.Property(p => p.Value)
                    .HasColumnName("Date")
                    .IsRequired();
            });

            // Configura a propriedade Deadline (associada ao Value Object Deadline)
            builder.OwnsOne(o => o.time, time =>
            {
                time.Property(d => d.Value)
                    .HasColumnName("Time")
                    .IsRequired();
            });

            // Configura a propriedade OperationTypeId (que é uma referência a outra entidade)
            builder.Property(o => o.operationRequestId)
                .HasColumnName("OperationRequestId")
                .IsRequired();

            // Configura a propriedade MedicalRecordNumber (número simples, não um Value Object)
            builder.Property(o => o.roomNumber)
                .IsRequired();

            // Configura a propriedade Active (simples, bool)
            builder.Property(o => o.Active)
                .IsRequired();

            // Define o nome da tabela no banco de dados
            builder.ToTable("Appointments");
        }
    }
}
