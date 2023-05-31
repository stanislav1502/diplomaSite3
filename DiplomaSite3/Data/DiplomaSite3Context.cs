using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DiplomaSite3.Models;

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


    }
}
