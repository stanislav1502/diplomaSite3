
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

        public DbSet<DiplomaModel> DiplomaModel { get; set; } 
        public DbSet<UserModel> UserModel { get; set; }
        public DbSet<AdminModel> AdminModel { get; set; }

    }
}
