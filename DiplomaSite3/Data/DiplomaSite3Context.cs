
using DiplomaSite3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DiplomaSite3.Data
{
    public class DiplomaSite3Context : IdentityDbContext<UserModel, IdentityRole<Guid>,Guid>
    {
        public DiplomaSite3Context (DbContextOptions<DiplomaSite3Context> options)
            : base(options)
        {
        }

        public DbSet<DiplomaModel> DiplomasDBS { get; set; } 
        
        public DbSet<StudentModel> StudentsDBS{ get; set; }
        public DbSet<TeacherModel> TeachersDBS{ get; set; }
        public DbSet<AdminModel> AdminsDBS{ get; set; }
        public DbSet<UserModel> UsersDBS{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModel>().ToTable("Users");
            
            modelBuilder.Entity<DiplomaModel>().ToTable("Diploma");

            modelBuilder.Entity<StudentModel>().ToTable("Users");
            modelBuilder.Entity<TeacherModel>().ToTable("Users");
            modelBuilder.Entity<AdminModel>().ToTable("Users");


        }

    }
}
