using Microsoft.EntityFrameworkCore;
using DDDSample1.Domain.Categories;
using DDDSample1.Domain.Products;
using DDDSample1.Domain.Families;
using DDDSample1.Infrastructure.Categories;
using DDDSample1.Infrastructure.Products;
using DDDSample1.Domain;
using DDDSample1.Infraestructure.Users;
using DDDSample1.Infraestructure.OperationTypes;
using Backend.Infraestructure.SurgeryRoom;
using DDDSample1.Domain.SurgeryRooms;

using DDDSample1.Infraestructure.Appointments;
using DDDSample1.Domain.Specialization;
using DDDSample1.Infrastructure.StaffS;
using DDDSample1.Infrastructure.Specializations;
using DDDSample1.Domain.Staff;

namespace DDDSample1.Infrastructure
{
    public class DDDSample1DbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationType> OperationsTypes { get; set; }

        public DbSet<SurgeryRoomEntity> SurgeryRooms { get; set; }

        public DbSet<OperationRequest> OperationRequests { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Specialization> Specializations { get; set; }

        public DDDSample1DbContext(DbContextOptions<DDDSample1DbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FamilyEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OperationTypeEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new SurgeryRoomEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new OperationRequestEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AppointmentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SurgeryRoomEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StaffEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SpecializationEntityTypeConfiguration());

        }
    }
}
