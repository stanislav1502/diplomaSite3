
using DiplomaSite3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace DiplomaSite3.Data
{
    public class DiplomaSite3Context : IdentityDbContext<UserModel, IdentityRole<Guid>, Guid>
    {
        public DiplomaSite3Context(DbContextOptions<DiplomaSite3Context> options) : base(options)
        { }

        public DbSet<DiplomaModel> DiplomasDBS { get; set; }
        public DbSet<StudentModel> StudentsDBS { get; set; }
        public DbSet<TeacherModel> TeachersDBS { get; set; }
        public DbSet<AdminModel> AdminsDBS { get; set; }
        public DbSet<UserModel> UsersDBS { get; set; }
        public DbSet<FacultyModel> FacultiesDBS { get; set; }
        public DbSet<DepartmentModel> DepartmentsDBS { get; set; }
        public DbSet<ProgrammeModel> ProgrammesDBS { get; set; }
        public DbSet<DegreeModel> DegreesDBS { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<UserModel>().ToTable("Users");

            modelBuilder.Entity<DiplomaModel>().ToTable("Diploma");

            modelBuilder.Entity<StudentModel>().ToTable("Users");
            modelBuilder.Entity<TeacherModel>().ToTable("Users");
            modelBuilder.Entity<AdminModel>().ToTable("Users");

            modelBuilder.Entity<FacultyModel>().ToTable("Faculties")
                .HasMany(f=>f.Departments).WithOne(d=>d.Faculty).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<DepartmentModel>().ToTable("Departments")
                .HasMany(d => d.Programmes).WithOne(p => p.Department).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ProgrammeModel>().ToTable("Programmes")
                .HasMany(p=>p.Degrees).WithOne(d=>d.Programme).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<DegreeModel>().ToTable("Degrees");
            modelBuilder.Entity<DegreeModel>()
                .HasOne(d => d.Programme)
                .WithMany(p => p.Degrees)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<DegreeModel>()
                .HasOne(d => d.Department)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<DegreeModel>()
                            .HasOne(d => d.Faculty)
                            .WithMany()
                            .OnDelete(DeleteBehavior.SetNull);


        }

    }
}