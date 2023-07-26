﻿// <auto-generated />
using System;
using DiplomaSite3.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DiplomaSite3.Migrations
{
    [DbContext(typeof(DiplomaSite3Context))]
    [Migration("20230726151923_BetterUserTable2")]
    partial class BetterUserTable2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DiplomaSite3.Models.AssignedThesisModel", b =>
                {
                    b.Property<Guid>("ThesisID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StudentID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TeacherID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ThesisID");

                    b.HasIndex("StudentID")
                        .IsUnique()
                        .HasFilter("[StudentID] IS NOT NULL");

                    b.HasIndex("TeacherID");

                    b.ToTable("AssignedTheses", (string)null);
                });

            modelBuilder.Entity("DiplomaSite3.Models.DegreeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Degree")
                        .HasColumnType("int");

                    b.Property<int?>("ProgrammeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProgrammeId");

                    b.ToTable("Degrees", (string)null);
                });

            modelBuilder.Entity("DiplomaSite3.Models.DepartmentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FacultyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.ToTable("Departments", (string)null);
                });

            modelBuilder.Entity("DiplomaSite3.Models.FacultyModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FacultyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Faculties", (string)null);
                });

            modelBuilder.Entity("DiplomaSite3.Models.ProgrammeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("ProgrammeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Programmes", (string)null);
                });

            modelBuilder.Entity("DiplomaSite3.Models.ThesisModel", b =>
                {
                    b.Property<Guid>("ThesisID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AssignDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DefendDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DegreeId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal?>("Grade")
                        .HasPrecision(5, 3)
                        .HasColumnType("decimal(5,3)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Tags")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ThesisID");

                    b.HasIndex("DegreeId");

                    b.ToTable("Thesis", (string)null);
                });

            modelBuilder.Entity("DiplomaSite3.Models.UserModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("DiplomaSite3.Models.AdminModel", b =>
                {
                    b.HasBaseType("DiplomaSite3.Models.UserModel");

                    b.Property<string>("AdminPass")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.ToTable("Admins", (string)null);
                });

            modelBuilder.Entity("DiplomaSite3.Models.StudentModel", b =>
                {
                    b.HasBaseType("DiplomaSite3.Models.UserModel");

                    b.Property<string>("FacultyNumber")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.ToTable("Students", (string)null);
                });

            modelBuilder.Entity("DiplomaSite3.Models.TeacherModel", b =>
                {
                    b.HasBaseType("DiplomaSite3.Models.UserModel");

                    b.Property<bool?>("Approved")
                        .HasColumnType("bit");

                    b.ToTable("Teachers", (string)null);
                });

            modelBuilder.Entity("DiplomaSite3.Models.AssignedThesisModel", b =>
                {
                    b.HasOne("DiplomaSite3.Models.StudentModel", "Student")
                        .WithOne("AssignedThesis")
                        .HasForeignKey("DiplomaSite3.Models.AssignedThesisModel", "StudentID");

                    b.HasOne("DiplomaSite3.Models.TeacherModel", "Teacher")
                        .WithMany("PostedTheses")
                        .HasForeignKey("TeacherID");

                    b.HasOne("DiplomaSite3.Models.ThesisModel", "Thesis")
                        .WithOne("Assigned")
                        .HasForeignKey("DiplomaSite3.Models.AssignedThesisModel", "ThesisID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Teacher");

                    b.Navigation("Thesis");
                });

            modelBuilder.Entity("DiplomaSite3.Models.DegreeModel", b =>
                {
                    b.HasOne("DiplomaSite3.Models.ProgrammeModel", "Programme")
                        .WithMany("Degrees")
                        .HasForeignKey("ProgrammeId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Programme");
                });

            modelBuilder.Entity("DiplomaSite3.Models.DepartmentModel", b =>
                {
                    b.HasOne("DiplomaSite3.Models.FacultyModel", "Faculty")
                        .WithMany("Departments")
                        .HasForeignKey("FacultyId");

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("DiplomaSite3.Models.ProgrammeModel", b =>
                {
                    b.HasOne("DiplomaSite3.Models.DepartmentModel", "Department")
                        .WithMany("Programmes")
                        .HasForeignKey("DepartmentId");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("DiplomaSite3.Models.ThesisModel", b =>
                {
                    b.HasOne("DiplomaSite3.Models.DegreeModel", "Degree")
                        .WithMany("Thesis")
                        .HasForeignKey("DegreeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Degree");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("DiplomaSite3.Models.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("DiplomaSite3.Models.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiplomaSite3.Models.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("DiplomaSite3.Models.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DiplomaSite3.Models.AdminModel", b =>
                {
                    b.HasOne("DiplomaSite3.Models.UserModel", null)
                        .WithOne()
                        .HasForeignKey("DiplomaSite3.Models.AdminModel", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DiplomaSite3.Models.StudentModel", b =>
                {
                    b.HasOne("DiplomaSite3.Models.UserModel", null)
                        .WithOne()
                        .HasForeignKey("DiplomaSite3.Models.StudentModel", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DiplomaSite3.Models.TeacherModel", b =>
                {
                    b.HasOne("DiplomaSite3.Models.UserModel", null)
                        .WithOne()
                        .HasForeignKey("DiplomaSite3.Models.TeacherModel", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DiplomaSite3.Models.DegreeModel", b =>
                {
                    b.Navigation("Thesis");
                });

            modelBuilder.Entity("DiplomaSite3.Models.DepartmentModel", b =>
                {
                    b.Navigation("Programmes");
                });

            modelBuilder.Entity("DiplomaSite3.Models.FacultyModel", b =>
                {
                    b.Navigation("Departments");
                });

            modelBuilder.Entity("DiplomaSite3.Models.ProgrammeModel", b =>
                {
                    b.Navigation("Degrees");
                });

            modelBuilder.Entity("DiplomaSite3.Models.ThesisModel", b =>
                {
                    b.Navigation("Assigned")
                        .IsRequired();
                });

            modelBuilder.Entity("DiplomaSite3.Models.StudentModel", b =>
                {
                    b.Navigation("AssignedThesis");
                });

            modelBuilder.Entity("DiplomaSite3.Models.TeacherModel", b =>
                {
                    b.Navigation("PostedTheses");
                });
#pragma warning restore 612, 618
        }
    }
}
