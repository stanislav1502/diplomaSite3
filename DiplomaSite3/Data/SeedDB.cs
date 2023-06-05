
using DiplomaSite3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace DiplomaSite3.Data;

public static class SeedDB
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new DiplomaSite3Context(
            serviceProvider.GetRequiredService<
                DbContextOptions<DiplomaSite3Context>>()))
        {
            Guid admin1 = Guid.NewGuid();

            Guid teach1 = Guid.NewGuid();
            Guid stud1 = Guid.NewGuid();
            Guid teach2 = Guid.NewGuid();
            Guid stud2 = Guid.NewGuid();
            Guid teach3 = Guid.NewGuid();
            Guid stud3 = Guid.NewGuid();
            Guid teach4 = Guid.NewGuid();

            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new AdminModel
                    {
                        UserID = admin1,
                        Username = "adminadmin",
                        Email = "admin@ud3.bg",
                        Password = "admin",
                        FirstName = "Admin1",
                        LastName = "1Admin",
                        UserType = UserType.Admin,
                        AdminPass = "parolataparolata"
                    },
                    new TeacherModel
                    {
                        UserID = teach1,
                        Username = "teacher1",
                        Email = "teach1@ud3.bg",
                        Password = "teach",
                        FirstName = "Teach1",
                        LastName = "1Teach",
                        UserType = UserType.Teacher,
                        Approved = true
                    },
                    new TeacherModel
                    {
                        UserID = teach2,
                        Username = "teacher2",
                        Email = "teach2@ud3.bg",
                        Password = "teach",
                        FirstName = "Teach2",
                        LastName = "2teach",
                        UserType = UserType.Teacher,
                        Approved = false
                    },
                    new TeacherModel
                    {
                        UserID = teach3,
                        Username = "teacher3",
                        Email = "teach3@ud3.bg",
                        Password = "teach",
                        FirstName = "Teach3",
                        LastName = "3Teach",
                        UserType = UserType.Teacher,
                        Approved = true
                    },
                     new StudentModel
                     {
                         UserID = stud1,
                         Username = "student1",
                         Email = "stud1@ud3.bg",
                         Password = "stud",
                         FirstName = "Stud1",
                         LastName = "1Stud",
                         UserType = UserType.Student,
                         FacultyNumber = "230001"
                     },
                      new StudentModel
                      {
                          UserID = stud2,
                          Username = "student2",
                          Email = "stud2@ud3.bg",
                          Password = "stud",
                          FirstName = "Stud2",
                          LastName = "2Stud",
                          UserType = UserType.Student,
                          FacultyNumber = "230002"
                      },
                    new StudentModel
                    {
                        UserID = stud3,
                        Username = "student3",
                        Email = "stud3@ud3.bg",
                        Password = "stud",
                        FirstName = "Stud3",
                        LastName = "3Stud",
                        UserType = UserType.Student,
                        FacultyNumber = "230123"
                    }
                );

                context.SaveChanges();
            }

            if (!context.Diplomas.Any())
            {

                context.Diplomas.AddRange(
                    new DiplomaModel
                    {
                        Title = "DCDN: Distributed content delivery for the modern web",
                        Description = "This thesis explores the state of the art in browser-based P2P content delivery for the web and seeks to answer whether such systems can be used to efficiently and invisibly deliver content.",
                        DefendDate = DateTime.Parse("5-6-2014"),
                        Grade = 7.99M,
                        Tags = "p2p,web,dcdn,content delivery",
                        Status = StatusEnum.Archived,
                        TeacherID = teach1,
                        StudentID = stud1
                    },
                    new DiplomaModel
                    {
                        Title = "Diploma 2",
                        Description = "desc2",
                        Status = StatusEnum.InAppraisal,

                        DefendDate = DateTime.Parse("20-12-2024"),
                        StudentID = stud2
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

                        TeacherID = teach4
                    }
                );
                context.SaveChanges(); // TODO: fix seeding 
            }


        }
    }
}