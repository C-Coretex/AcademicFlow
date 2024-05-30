﻿// <auto-generated />
using System;
using AcademicFlow.Migrations.Factory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AcademicFlow.Migrations.Migrations
{
    [DbContext(typeof(MigrationsDbContext))]
    [Migration("20240528144247_AddTables_AssignmentsSchema")]
    partial class AddTables_AssignmentsSchema
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AcademicFlow.Domain.Contracts.Entities.AssignmentEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AssignmentFilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AssignmentTaskId")
                        .HasColumnType("int");

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentTaskId");

                    b.HasIndex("CreatedById");

                    b.ToTable("AssignmentEntry");
                });

            modelBuilder.Entity("AcademicFlow.Domain.Contracts.Entities.AssignmentGrade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AssignmentEntryId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<int>("GradedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentEntryId")
                        .IsUnique();

                    b.HasIndex("GradedById");

                    b.ToTable("AssignmentGrade");
                });

            modelBuilder.Entity("AcademicFlow.Domain.Contracts.Entities.AssignmentTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AssignmentDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssignmentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("AssignmentWeight")
                        .HasColumnType("real");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("CreatedById");

                    b.ToTable("AssignmentTask");
                });

            modelBuilder.Entity("AcademicFlow.Domain.Contracts.Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreditPoints")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PublicId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasAlternateKey("PublicId");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("AcademicFlow.Domain.Contracts.Entities.CourseProgram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("ProgramId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("ProgramId");

                    b.ToTable("CourseProgram");
                });

            modelBuilder.Entity("AcademicFlow.Domain.Contracts.Entities.CourseUserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("UserRoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("UserRoleId");

                    b.ToTable("CourseUserRole");
                });

            modelBuilder.Entity("AcademicFlow.Domain.Contracts.Entities.Program", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SemesterNr")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Program");
                });

            modelBuilder.Entity("AcademicFlow.Domain.Contracts.Entities.ProgramUserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProgramId")
                        .HasColumnType("int");

                    b.Property<int>("UserRoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProgramId");

                    b.HasIndex("UserRoleId");

                    b.ToTable("ProgramUserRole");
                });

            modelBuilder.Entity("AcademicFlow.Domain.Contracts.Entities.UserCredentials", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserCredentials");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PasswordHash = "9D5224C863CDFF320DF4CA0A4FC4450EF3CAAE32C7683FB7D91EAA1E0ECDF5A7",
                            Salt = "jqh08bf8",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("AcademicFlow.Domain.Contracts.Entities.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Role = 0,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("AcademicFlow.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("PersonalCode")
                        .IsUnique();

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Age = -1,
                            Email = "adm@adm.adm",
                            IsDeleted = false,
                            Name = "Admin",
                            PersonalCode = "000000-00000",
                            PhoneNumber = "0000000",
                            Surname = "Admin"
                        });
                });

            modelBuilder.Entity("AcademicFlow.Domain.Contracts.Entities.AssignmentEntry", b =>
                {
                    b.HasOne("AcademicFlow.Domain.Contracts.Entities.AssignmentTask", "AssignmentTask")
                        .WithMany("AssignmentEntries")
                        .HasForeignKey("AssignmentTaskId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("AcademicFlow.Domain.Entities.User", "User")
                        .WithMany("AssignmentEntries")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("AssignmentTask");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AcademicFlow.Domain.Contracts.Entities.AssignmentGrade", b =>
                {
                    b.HasOne("AcademicFlow.Domain.Contracts.Entities.AssignmentEntry", "AssignmentEntry")
                        .WithOne("AssignmentGrade")
                        .HasForeignKey("AcademicFlow.Domain.Contracts.Entities.AssignmentGrade", "AssignmentEntryId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("AcademicFlow.Domain.Entities.User", "User")
                        .WithMany("AssignmentGrades")
                        .HasForeignKey("GradedById")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("AssignmentEntry");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AcademicFlow.Domain.Contracts.Entities.AssignmentTask", b =>
                {
                    b.HasOne("AcademicFlow.Domain.Entities.User", "User")
                        .WithMany("AssignmentTasks")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AcademicFlow.Domain.Contracts.Entities.CourseProgram", b =>
                {
                    b.HasOne("AcademicFlow.Domain.Contracts.Entities.Course", "Course")
                        .WithMany("Programs")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AcademicFlow.Domain.Contracts.Entities.Program", "Program")
                        .WithMany("Courses")
                        .HasForeignKey("ProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Program");
                });

            modelBuilder.Entity("AcademicFlow.Domain.Contracts.Entities.CourseUserRole", b =>
                {
                    b.HasOne("AcademicFlow.Domain.Contracts.Entities.Course", "Course")
                        .WithMany("Users")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AcademicFlow.Domain.Contracts.Entities.UserRole", "UserRole")
                        .WithMany("Courses")
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("AcademicFlow.Domain.Contracts.Entities.ProgramUserRole", b =>
                {
                    b.HasOne("AcademicFlow.Domain.Contracts.Entities.Program", "Program")
                        .WithMany("UserRoles")
                        .HasForeignKey("ProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AcademicFlow.Domain.Contracts.Entities.UserRole", "UserRole")
                        .WithMany("Programs")
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Program");

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("AcademicFlow.Domain.Contracts.Entities.UserCredentials", b =>
                {
                    b.HasOne("AcademicFlow.Domain.Entities.User", "User")
                        .WithOne("UserCredentials")
                        .HasForeignKey("AcademicFlow.Domain.Contracts.Entities.UserCredentials", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AcademicFlow.Domain.Contracts.Entities.UserRole", b =>
                {
                    b.HasOne("AcademicFlow.Domain.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AcademicFlow.Domain.Contracts.Entities.AssignmentEntry", b =>
                {
                    b.Navigation("AssignmentGrade")
                        .IsRequired();
                });

            modelBuilder.Entity("AcademicFlow.Domain.Contracts.Entities.AssignmentTask", b =>
                {
                    b.Navigation("AssignmentEntries");
                });

            modelBuilder.Entity("AcademicFlow.Domain.Contracts.Entities.Course", b =>
                {
                    b.Navigation("Programs");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("AcademicFlow.Domain.Contracts.Entities.Program", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("AcademicFlow.Domain.Contracts.Entities.UserRole", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Programs");
                });

            modelBuilder.Entity("AcademicFlow.Domain.Entities.User", b =>
                {
                    b.Navigation("AssignmentEntries");

                    b.Navigation("AssignmentGrades");

                    b.Navigation("AssignmentTasks");

                    b.Navigation("UserCredentials")
                        .IsRequired();

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
