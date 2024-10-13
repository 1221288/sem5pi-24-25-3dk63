using Microsoft.EntityFrameworkCore;
using DDDSample1.Domain.Categories;
using DDDSample1.Domain.Products;
using DDDSample1.Domain.Families;
using DDDSample1.Domain.Users;
using DDDSample1.Infrastructure.Categories;
using DDDSample1.Infrastructure.Products;
using DDDSample1.Domain;

namespace DDDSample1.Infrastructure
{
    public class DDDSample1DbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<User> Users { get; set; }

        public DDDSample1DbContext(DbContextOptions<DDDSample1DbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FamilyEntityTypeConfiguration());

            // Configure the User entity
            modelBuilder.Entity<User>(entity =>
            {
                // Set the key
                entity.HasKey(u => u.Id);

                // Configure the Username
                entity.Property(u => u.Username)
                    .HasConversion(
                        username => username.ToString(),
                        usernameString => new Username(usernameString))
                    .IsRequired();

                // Configure the Email
                entity.Property(u => u.Email)
                    .HasConversion(
                        email => email.ToString(),
                        emailString => new Email(emailString))
                    .IsRequired();

                // Configure the Role
                entity.Property(u => u.Role)
                    .HasConversion(
                        role => role.ToString(),
                        roleString => new Role(Enum.Parse<RoleType>(roleString)))
                    .IsRequired();

                // Configure the Name Value Object
                entity.OwnsOne(u => u.Name, name =>
                {
                    name.Property(n => n.FirstName)
                        .HasColumnName("FirstName")
                        .IsRequired();

                    name.Property(n => n.LastName)
                        .HasColumnName("LastName")
                        .IsRequired();

                    name.Ignore(n => n.FullName); 
                });

                entity.Property(u => u.Active).IsRequired();
                entity.Property(u => u.SequentialNumber).IsRequired();
            });
        }
    }
}
