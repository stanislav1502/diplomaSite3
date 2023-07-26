
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

        public DbSet<ThesisModel> ThesisDBS { get; set; }
        public DbSet<StudentModel> StudentsDBS { get; set; }
        public DbSet<TeacherModel> TeachersDBS { get; set; }
        public DbSet<AdminModel> AdminsDBS { get; set; }
        public DbSet<UserModel> UsersDBS { get; set; }
        public DbSet<FacultyModel> FacultiesDBS { get; set; }
        public DbSet<DepartmentModel> DepartmentsDBS { get; set; }
        public DbSet<ProgrammeModel> ProgrammesDBS { get; set; }
        public DbSet<DegreeModel> DegreesDBS { get; set; }
        public DbSet<AssignedThesisModel> AssignedThesesDBS { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModel>().ToTable("Users");

            modelBuilder.Entity<ThesisModel>().ToTable("Thesis");


            modelBuilder.Entity<StudentModel>().ToTable("Students")
                     .HasOne(s=>s.AssignedThesis).WithOne(d=>d.Student).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TeacherModel>().ToTable("Teachers")
                .HasMany(t => t.PostedTheses).WithOne(d => d.Teacher).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<AdminModel>().ToTable("Admins");

            
            modelBuilder.Entity<FacultyModel>().ToTable("Faculties")
                .HasMany(f=>f.Departments).WithOne(d=>d.Faculty);

            modelBuilder.Entity<DepartmentModel>().ToTable("Departments")
                .HasMany(d => d.Programmes).WithOne(p => p.Department);

            modelBuilder.Entity<ProgrammeModel>().ToTable("Programmes")
                .HasMany(p=>p.Degrees).WithOne(d=>d.Programme);

            modelBuilder.Entity<DegreeModel>().ToTable("Degrees")
                .HasOne(d => d.Programme).WithMany(p => p.Degrees).OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<AssignedThesisModel>().ToTable("AssignedTheses");
            modelBuilder.Entity<AssignedThesisModel>()
                .HasOne(a=>a.Thesis).WithOne(t=>t.Assigned).OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<AssignedThesisModel>()
                .HasOne(a=>a.Teacher).WithMany(t=>t.PostedTheses).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<AssignedThesisModel>()
                .HasOne(a=>a.Student).WithOne(s=>s.AssignedThesis).OnDelete(DeleteBehavior.ClientSetNull);

        }

    }
}