﻿// <auto-generated />
using System;
using DDDSample1.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DDDNetCore.Migrations
{
    [DbContext(typeof(DDDSample1DbContext))]
    [Migration("20241022172812_FixOperationType2")]
    partial class FixOperationType2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("DDDSample1.Domain.Appointment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("operationRequestId")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("OperationRequestId");

                    b.Property<int>("roomNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Appointments", (string)null);
                });

            modelBuilder.Entity("DDDSample1.Domain.Categories.Category", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DDDSample1.Domain.Families.Family", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Families");
                });

            modelBuilder.Entity("DDDSample1.Domain.OperationRequest", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("medicalRecordNumber")
                        .HasColumnType("int");

                    b.Property<string>("operationTypeId")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("OperationTypeId");

                    b.HasKey("Id");

                    b.ToTable("OperationRequests", (string)null);
                });

            modelBuilder.Entity("DDDSample1.Domain.OperationType", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("OperationTypes", (string)null);
                });

            modelBuilder.Entity("DDDSample1.Domain.Patient", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext");

                    b.Property<int>("sequentialNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("DDDSample1.Domain.Products.Product", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("DDDSample1.Domain.Specialization.Specialization", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("SpecializationId");

                    b.Property<int>("SequentialNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Specializations");
                });

            modelBuilder.Entity("DDDSample1.Domain.Staff.Staff", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("LicenseNumber");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("AvailabilitySlots")
                        .HasColumnType("longtext")
                        .HasColumnName("AvailabilitySlots");

                    b.Property<string>("SpecializationId")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("SpecializationId");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("SpecializationId");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("DDDSample1.Domain.SurgeryRooms.SurgeryRoomEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("CurrentStatus")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("RoomNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("SurgeryRooms");
                });

            modelBuilder.Entity("DDDSample1.Domain.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ConfirmationToken")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("SequentialNumber")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DDDSample1.Domain.Appointment", b =>
                {
                    b.OwnsOne("DDDSample1.Domain.Appointments.Date", "date", b1 =>
                        {
                            b1.Property<string>("AppointmentId")
                                .HasColumnType("varchar(255)");

                            b1.Property<DateTime>("Value")
                                .HasColumnType("datetime(6)")
                                .HasColumnName("Date");

                            b1.HasKey("AppointmentId");

                            b1.ToTable("Appointments");

                            b1.WithOwner()
                                .HasForeignKey("AppointmentId");
                        });

                    b.OwnsOne("DDDSample1.Domain.Appointments.Time", "time", b1 =>
                        {
                            b1.Property<string>("AppointmentId")
                                .HasColumnType("varchar(255)");

                            b1.Property<TimeSpan>("Value")
                                .HasColumnType("time(6)")
                                .HasColumnName("Time");

                            b1.HasKey("AppointmentId");

                            b1.ToTable("Appointments");

                            b1.WithOwner()
                                .HasForeignKey("AppointmentId");
                        });

                    b.Navigation("date")
                        .IsRequired();

                    b.Navigation("time")
                        .IsRequired();
                });

            modelBuilder.Entity("DDDSample1.Domain.OperationRequest", b =>
                {
                    b.OwnsOne("DDDSample1.Domain.OperationRequests.Deadline", "deadline", b1 =>
                        {
                            b1.Property<string>("OperationRequestId")
                                .HasColumnType("varchar(255)");

                            b1.Property<DateTime>("Value")
                                .HasColumnType("datetime(6)")
                                .HasColumnName("Deadline");

                            b1.HasKey("OperationRequestId");

                            b1.ToTable("OperationRequests");

                            b1.WithOwner()
                                .HasForeignKey("OperationRequestId");
                        });

                    b.OwnsOne("DDDSample1.Domain.OperationRequests.Priority", "priority", b1 =>
                        {
                            b1.Property<string>("OperationRequestId")
                                .HasColumnType("varchar(255)");

                            b1.Property<int>("Value")
                                .HasColumnType("int")
                                .HasColumnName("Priority");

                            b1.HasKey("OperationRequestId");

                            b1.ToTable("OperationRequests");

                            b1.WithOwner()
                                .HasForeignKey("OperationRequestId");
                        });

                    b.OwnsOne("DDDSample1.Domain.Staff.LicenseNumber", "licenseNumber", b1 =>
                        {
                            b1.Property<string>("OperationRequestId")
                                .HasColumnType("varchar(255)");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("LicenseNumber");

                            b1.HasKey("OperationRequestId");

                            b1.ToTable("OperationRequests");

                            b1.WithOwner()
                                .HasForeignKey("OperationRequestId");
                        });

                    b.Navigation("deadline")
                        .IsRequired();

                    b.Navigation("licenseNumber")
                        .IsRequired();

                    b.Navigation("priority")
                        .IsRequired();
                });

            modelBuilder.Entity("DDDSample1.Domain.OperationType", b =>
                {
                    b.OwnsOne("Backend.Domain.Users.ValueObjects.Name", "Name", b1 =>
                        {
                            b1.Property<string>("OperationTypeId")
                                .HasColumnType("varchar(255)");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("OperationFirstName");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("OperationLastName");

                            b1.HasKey("OperationTypeId");

                            b1.ToTable("OperationTypes");

                            b1.WithOwner()
                                .HasForeignKey("OperationTypeId");
                        });

                    b.OwnsOne("DDDSample1.Domain.OperationsType.Duration", "Duration", b1 =>
                        {
                            b1.Property<string>("OperationTypeId")
                                .HasColumnType("varchar(255)");

                            b1.Property<int>("CleaningPhase")
                                .HasColumnType("int")
                                .HasColumnName("CleaningPhaseDuration");

                            b1.Property<int>("PreparationPhase")
                                .HasColumnType("int")
                                .HasColumnName("PreparationPhaseDuration");

                            b1.Property<int>("SurgeryPhase")
                                .HasColumnType("int")
                                .HasColumnName("SurgeryPhaseDuration");

                            b1.HasKey("OperationTypeId");

                            b1.ToTable("OperationTypes");

                            b1.WithOwner()
                                .HasForeignKey("OperationTypeId");
                        });

                    b.OwnsMany("DDDSample1.Domain.OperationsType.StaffSpecialization", "RequiredStaff", b1 =>
                        {
                            b1.Property<string>("OperationTypeId")
                                .HasColumnType("varchar(255)");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<int>("RequiredNumber")
                                .HasColumnType("int")
                                .HasColumnName("RequiredNumber");

                            b1.Property<string>("Specialization")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("Specialization");

                            b1.HasKey("OperationTypeId", "Id");

                            b1.ToTable("RequiredStaffBySpecialization", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("OperationTypeId");
                        });

                    b.Navigation("Duration")
                        .IsRequired();

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("RequiredStaff");
                });

            modelBuilder.Entity("DDDSample1.Domain.Patient", b =>
                {
                    b.OwnsOne("DDDSample1.Domain.Patients.Allergy", "allergy", b1 =>
                        {
                            b1.Property<string>("PatientId")
                                .HasColumnType("varchar(255)");

                            b1.Property<string>("allergy")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("Allergy");

                            b1.HasKey("PatientId");

                            b1.ToTable("Patients");

                            b1.WithOwner()
                                .HasForeignKey("PatientId");
                        });

                    b.OwnsMany("DDDSample1.Domain.Patients.AppointmentHistory", "appointmentHistoryList", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("PatientId")
                                .IsRequired()
                                .HasColumnType("varchar(255)");

                            b1.Property<DateTime>("appointmentDate")
                                .HasColumnType("datetime(6)");

                            b1.Property<string>("doctorName")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.HasKey("Id");

                            b1.HasIndex("PatientId");

                            b1.ToTable("AppointmentHistory");

                            b1.WithOwner()
                                .HasForeignKey("PatientId");
                        });

                    b.OwnsOne("DDDSample1.Domain.Patients.DateOfBirth", "dateOfBirth", b1 =>
                        {
                            b1.Property<string>("PatientId")
                                .HasColumnType("varchar(255)");

                            b1.Property<DateTime>("date")
                                .HasColumnType("datetime(6)")
                                .HasColumnName("DateOfBirth");

                            b1.HasKey("PatientId");

                            b1.ToTable("Patients");

                            b1.WithOwner()
                                .HasForeignKey("PatientId");
                        });

                    b.OwnsOne("DDDSample1.Domain.Patients.EmergencyContact", "emergencyContact", b1 =>
                        {
                            b1.Property<string>("PatientId")
                                .HasColumnType("varchar(255)");

                            b1.Property<string>("emergencyContact")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("EmergencyContactName");

                            b1.HasKey("PatientId");

                            b1.ToTable("Patients");

                            b1.WithOwner()
                                .HasForeignKey("PatientId");
                        });

                    b.OwnsOne("DDDSample1.Domain.Patients.Gender", "gender", b1 =>
                        {
                            b1.Property<string>("PatientId")
                                .HasColumnType("varchar(255)");

                            b1.Property<string>("gender")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("Gender");

                            b1.HasKey("PatientId");

                            b1.ToTable("Patients");

                            b1.WithOwner()
                                .HasForeignKey("PatientId");
                        });

                    b.Navigation("allergy")
                        .IsRequired();

                    b.Navigation("appointmentHistoryList");

                    b.Navigation("dateOfBirth")
                        .IsRequired();

                    b.Navigation("emergencyContact")
                        .IsRequired();

                    b.Navigation("gender")
                        .IsRequired();
                });

            modelBuilder.Entity("DDDSample1.Domain.Specialization.Specialization", b =>
                {
                    b.OwnsOne("Backend.Domain.Specialization.ValueObjects.Description", "Description", b1 =>
                        {
                            b1.Property<string>("SpecializationId")
                                .HasColumnType("varchar(255)");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("varchar(200)")
                                .HasColumnName("Description");

                            b1.HasKey("SpecializationId");

                            b1.HasIndex("Value")
                                .IsUnique()
                                .HasDatabaseName("IX_Unique_Description");

                            b1.ToTable("Specializations");

                            b1.WithOwner()
                                .HasForeignKey("SpecializationId");
                        });

                    b.Navigation("Description")
                        .IsRequired();
                });

            modelBuilder.Entity("DDDSample1.Domain.Staff.Staff", b =>
                {
                    b.HasOne("DDDSample1.Domain.Specialization.Specialization", null)
                        .WithMany()
                        .HasForeignKey("SpecializationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("DDDSample1.Domain.SurgeryRooms.SurgeryRoomEntity", b =>
                {
                    b.OwnsMany("Backend.Domain.SurgeryRoom.Equipment", "AssignedEquipment", b1 =>
                        {
                            b1.Property<string>("SurgeryRoomEntityId")
                                .HasColumnType("varchar(255)");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<bool>("IsOperational")
                                .HasColumnType("tinyint(1)");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("EquipmentName");

                            b1.Property<string>("Type")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("EquipmentType");

                            b1.HasKey("SurgeryRoomEntityId", "Id");

                            b1.ToTable("Equipment");

                            b1.WithOwner()
                                .HasForeignKey("SurgeryRoomEntityId");
                        });

                    b.OwnsMany("DDDSample1.Domain.SurgeryRooms.ValueObjects.MaintenanceSlot", "MaintenanceSlots", b1 =>
                        {
                            b1.Property<string>("SurgeryRoomEntityId")
                                .HasColumnType("varchar(255)");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<DateTime>("EndTime")
                                .HasColumnType("datetime(6)")
                                .HasColumnName("MaintenanceEndTime");

                            b1.Property<DateTime>("StartTime")
                                .HasColumnType("datetime(6)")
                                .HasColumnName("MaintenanceStartTime");

                            b1.HasKey("SurgeryRoomEntityId", "Id");

                            b1.ToTable("MaintenanceSlot");

                            b1.WithOwner()
                                .HasForeignKey("SurgeryRoomEntityId");
                        });

                    b.Navigation("AssignedEquipment");

                    b.Navigation("MaintenanceSlots");
                });

            modelBuilder.Entity("DDDSample1.Domain.User", b =>
                {
                    b.OwnsOne("Backend.Domain.Users.ValueObjects.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<string>("UserId")
                                .HasColumnType("varchar(255)");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .IsUnicode(true)
                                .HasColumnType("longtext")
                                .HasColumnName("PhoneNumber");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("Backend.Domain.Users.ValueObjects.Name", "Name", b1 =>
                        {
                            b1.Property<string>("UserId")
                                .HasColumnType("varchar(255)");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("FirstName");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("LastName");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("PhoneNumber")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}