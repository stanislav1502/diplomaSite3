
using DiplomaSite3.Models;
using Microsoft.EntityFrameworkCore;

namespace DiplomaSite3.Data
{
    public class DiplomaSite3Context : DbContext
    {
        public DiplomaSite3Context (DbContextOptions<DiplomaSite3Context> options)
            : base(options)
        {
        }

        public DbSet<DiplomaModel> Diplomas { get; set; } 
        
        public DbSet<StudentModel> Students{ get; set; }
        public DbSet<TeacherModel> Teachers{ get; set; }
        public DbSet<AdminModel> Admins{ get; set; }
        public DbSet<UserModel> Users{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().ToTable("User");
            
            modelBuilder.Entity<DiplomaModel>().ToTable("Diploma");

            modelBuilder.Entity<StudentModel>().ToTable("User");
            modelBuilder.Entity<TeacherModel>().ToTable("User");
            modelBuilder.Entity<AdminModel>().ToTable("User");


        }

    }
}
