
using DiplomaSite3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DiplomaSite3.Data;

public static class SeedDB
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new DiplomaSite3Context(
            serviceProvider.GetRequiredService<
                DbContextOptions<DiplomaSite3Context>>()))
        {
            Guid teach3 = Guid.NewGuid();
            Guid stud3 = Guid.NewGuid();

            // Look for any movies.
            if (context.DiplomaModel.Any())
            { return; } // has diplomas
            else
            {
                
                context.DiplomaModel.AddRange(
                    new DiplomaModel
                    {
                        Title = "DCDN: Distributed content delivery for the modern web",
                        Description = "This thesis explores the state of the art in browser-based P2P content delivery for the web and seeks to answer whether such systems can be used to efficiently and invisibly deliver content.",
                        DefendDate = DateTime.Parse("5-6-2014"),
                        Grade = 7.99M,
                        Tags = "p2p,web,dcdn,content delivery",
                        Status = StatusEnum.Archived,
                        TeacherID = Guid.NewGuid(),
                        StudentID = Guid.NewGuid()
                    },
                    new DiplomaModel
                    {
                        Title = "Diploma 2",
                        Description = "desc2",
                        Status = StatusEnum.InAppraisal,

                        DefendDate = DateTime.Parse("20-12-2024"),
                        StudentID = Guid.NewGuid(),
                        TeacherID = Guid.NewGuid()
                    },
                    new DiplomaModel
                    {
                        Title = "Diploma 3",
                        Description = "desc3",
                        Status = StatusEnum.WIP,

                        Tags = "tag303,tag3,tag33",
                        TeacherID = teach3,
                        StudentID = stud3
                    },
                    new DiplomaModel
                    {
                        Title = "Diploma 4",
                        Description = "desc4",
                        Status = StatusEnum.Posted,

                        TeacherID = Guid.NewGuid()
                    }
                );
                context.SaveChanges();
            }

            if (context.UserModel.Any())
            { return; } //has users
            else
            {
                context.UserModel.AddRange(
                    new AdminModel
                    {
                        Username = "adminadmin",
                        Email = "admin@ud3.bg",
                        Password = "admin",
                        FirstName = "Admin1",
                        LastName = "1Admin",
                        UserType = UserType.Admin,
                        AdminPassword = "parolataparolata"                        
                    },
                    new TeacherModel
                    {
                        UserID = teach3,
                        Username = "teacher3",
                        Email = "teach3@ud3.bg",
                        Password = "teach",
                        FirstName = "Admin3",
                        LastName = "3Admin",
                        UserType = UserType.Teacher,
                        Approved = true
                    },
                    new StudentModel
                    {
                        UserID = stud3,
                        Username = "student2",
                        Email = "stud2@ud3.bg",
                        Password = "stud",
                        FirstName = "Stud2",
                        LastName = "2Admin",
                        UserType = UserType.Admin,
                        FacultyNumber = "230123"
                    }
                );
            
            context.SaveChanges();
            }
        }
    }
}