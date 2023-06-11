
using DiplomaSite3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Security.Cryptography;
using System.Text;

namespace DiplomaSite3.Data;

public static class SeedDB
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new DiplomaSite3Context(
            serviceProvider.GetRequiredService<
                DbContextOptions<DiplomaSite3Context>>()))
        {
            Random rng = new Random();
            
            Guid admin1 = Guid.NewGuid();
            byte[] adminsalt = new byte[32]; 
            rng.NextBytes(adminsalt);

            Guid teach1 = Guid.NewGuid();
            Guid stud1 = Guid.NewGuid();
            Guid teach2 = Guid.NewGuid();
            Guid stud2 = Guid.NewGuid();
            Guid teach3 = Guid.NewGuid();
            Guid stud3 = Guid.NewGuid();
            Guid teach4 = Guid.NewGuid();

            if (!context.UsersDBS.Any())
            {
                context.Users.AddRange(
                    new AdminModel
                    {
                        Id = admin1,
                        UserName = "AdminAdmin",
                        NormalizedUserName = "AdminAdmin".Normalize(),
                        Email = "admin@ud3.bg",
                        NormalizedEmail = "admin@ud3.bg".Normalize(),
                        EmailConfirmed = true,
                        PasswordHash = Rfc2898DeriveBytes.Pbkdf2("adminadmin", adminsalt, 35716, HashAlgorithmName.SHA512, 100).ToString(),
                        PasswordSalt = adminsalt,
                        SecurityStamp = null,
                        ConcurrencyStamp = null,
                        PhoneNumber = "1234567890",
                        PhoneNumberConfirmed = true,
                        TwoFactorEnabled = true,
                        LockoutEnd = null,
                        LockoutEnabled = false,
                        AccessFailedCount = 0,
                        FirstName = "Admin1",
                        LastName = "1Admin",
                        UserType = UserType.Admin,
                        AdminPass = "123456",

                    },
                    new TeacherModel
                    {
                        Id = teach1,
                        UserName = "teacher1",
                        Email = "teach1@ud3.bg",
                        PasswordHash = "teach",
                        PasswordSalt = adminsalt,
                        FirstName = "Teach1",
                        LastName = "1Teach",
                        UserType = UserType.Teacher,
                        Approved = true
                    },
                    new TeacherModel
                    {
                        Id = teach2,
                        UserName = "teacher2",
                        Email = "teach2@ud3.bg",
                        PasswordHash = "teach",
                        PasswordSalt = adminsalt,
                        FirstName = "Teach2",
                        LastName = "2teach",
                        UserType = UserType.Teacher,
                        Approved = false
                    },
                    new TeacherModel
                    {
                        Id = teach3,
                        UserName = "teacher3",
                        Email = "teach3@ud3.bg",
                        PasswordHash = "teach",
                        PasswordSalt = adminsalt,
                        FirstName = "Teach3",
                        LastName = "3Teach",
                        UserType = UserType.Teacher,
                        Approved = true
                    },
                     new StudentModel
                     {
                         Id = stud1,
                         UserName = "student1",
                         Email = "stud1@ud3.bg",
                         PasswordHash = "stud",
                         PasswordSalt = adminsalt,
                         FirstName = "Stud1",
                         LastName = "1Stud",
                         UserType = UserType.Student,
                         FacultyNumber = "230001"
                     },
                      new StudentModel
                      {
                          Id = stud2,
                          UserName = "student2",
                          Email = "stud2@ud3.bg",
                          PasswordHash = "stud",
                          PasswordSalt = adminsalt,
                          FirstName = "Stud2",
                          LastName = "2Stud",
                          UserType = UserType.Student,
                          FacultyNumber = "230002"
                      },
                    new StudentModel
                    {
                        Id = stud3,
                        UserName = "student3",
                        Email = "stud3@ud3.bg",
                        PasswordHash = "stud",
                        PasswordSalt = adminsalt,
                        FirstName = "Stud3",
                        LastName = "3Stud",
                        UserType = UserType.Student,
                        FacultyNumber = "230123"
                    }
                );

                context.SaveChanges();
            }

            if (!context.DiplomasDBS.Any())
            {

                context.DiplomasDBS.AddRange(
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
                context.SaveChanges(); 
            }

        }
    }
}