
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DiplomaModel>()
                .HasOne(d => d.Student)
                .WithOne(s => s.AssignedDiploma)
                .IsRequired(false);
        }

        public DbSet<DiplomaSite3.Models.DiplomaModel> DiplomaModel { get; set; } 
        public DbSet<UserModel> UserModel { get; set; }


    }
}
