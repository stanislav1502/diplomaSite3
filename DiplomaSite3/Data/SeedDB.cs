
using DiplomaSite3.Models;
using DiplomaSite3.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace DiplomaSite3.Data;

public static class SeedDB
{
    private static readonly bool cofirmAllEmails = false;

    public static async void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new DiplomaSite3Context(
            serviceProvider.GetRequiredService<
                DbContextOptions<DiplomaSite3Context>>()))
        {

            if (!context.AdminsDBS.Any())
            {
                AdminModel user = Activator.CreateInstance<AdminModel>();
                string username = "Admin";
                string useremail = "admin@admin.haha";
                string userpass = "admin1-Admin";
                string userfirstname = "admin";
                string userlastname = "adminov";

                context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
                //      await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Admin;
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);

                user.Verified = true;
                context.AdminsDBS.Add(user);

                context.SaveChanges();

            }

            if (!context.TeachersDBS.Any())
            {
                TeacherModel user = Activator.CreateInstance<TeacherModel>();
                string username = "Teacher";
                string useremail = "teach@teach.haha";
                string userpass = ",a'%4n,k9Yp&#X\"";
                string userfirstname = "Teacher";
                string userlastname = "Teacherov";

                context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
                //       await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Teacher;
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);

                context.TeachersDBS.Add(user);

                context.SaveChanges();

            }

            if (!context.StudentsDBS.Any())
            {
                StudentModel user = Activator.CreateInstance<StudentModel>();
                string username = "Student";
                   string useremail = "student@stud.haha";
                string userpass = ",9BkiGkf2s:kSn7";
                string userfirstname = "student";
                string userlastname = "Studentski";

                context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
                //   await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Student;
                user.FacultyNumber = "123456";
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);

                context.StudentsDBS.Add(user);

                context.SaveChanges();

            }


            if (context.UsersDBS.Any() && cofirmAllEmails)
            {
                var users = context.UsersDBS;
                var userQuerry = from u in users
                                     select u;

                foreach (var u in userQuerry)
                    u.EmailConfirmed = true;

                context.SaveChanges(true);
            }


            if (!context.FacultiesDBS.Any())
            {

                context.FacultiesDBS.AddRange(

                    new FacultyModel
                    {
                        FacultyName = "Аграрно-индустриален"
                    },
                    new FacultyModel
                    {
                        FacultyName = "Машинно-технологичен"
                    },
                    new FacultyModel
                    {
                        FacultyName = "Електротехника, електроника и автоматика"
                    },
                    new FacultyModel
                    {
                        FacultyName = "Транспортен"
                    },
                    new FacultyModel
                    {
                        FacultyName = "Бизнес и мениджмънт"
                    },
                    new FacultyModel
                    {
                        FacultyName = "Природни науки и образование"
                    },
                    new FacultyModel
                    {
                        FacultyName = "Юридически"
                    },
                    new FacultyModel
                    {
                        FacultyName = "Обществено здраве и здравни грижи​​"
                    });
                context.SaveChanges();
            }

            if (!context.DepartmentsDBS.Any())
            {
                context.AddRange(

                    new DepartmentModel
                    {
                        DepartmentName = "Земеделска техника",
                        FacultyId = 1,
                    },
                    new DepartmentModel
                    {
                        DepartmentName = "Материалознание и технология на материалите",
                        FacultyId = 2,
                    },
                    new DepartmentModel
                    {
                        DepartmentName = "Електроника",
                        FacultyId = 2,
                    },
                    new DepartmentModel
                    {
                        DepartmentName = "Двигатели и транспортна техника",
                        FacultyId = 1,
                    },
                    new DepartmentModel
                    {
                        DepartmentName = "Икономика и международни отношения",
                        FacultyId = 2,
                    },
                    new DepartmentModel
                    {
                        DepartmentName = "Информатика и информационни технологии",
                        FacultyId = 4,
                    },
                    new DepartmentModel
                    {
                        DepartmentName = "Публичноправни науки",
                        FacultyId = 2,
                    },
                    new DepartmentModel
                    {
                        DepartmentName = "Обществено здраве",
                        FacultyId = 2,
                    });
            }

            if (!context.ProgrammesDBS.Any())
            {

                context.ProgrammesDBS.AddRange(
                    new ProgrammeModel
                    {
                        ProgrammeName = "Земеделска техника и технологии",
                        DepartmentId = 1,
                    },
                    new ProgrammeModel
                    {
                        ProgrammeName = "Материалознание и технологии",
                        DepartmentId = 2,
                    },
                    new ProgrammeModel
                    {
                        ProgrammeName = "Електронизация",
                        DepartmentId = 3
                    },
                    new ProgrammeModel
                    {
                        ProgrammeName = "Автомобилна техника",
                        DepartmentId = 4
                    },
                    new ProgrammeModel
                    {
                        ProgrammeName = "Политическа икономия",
                        DepartmentId = 5
                    },
                    new ProgrammeModel
                    {
                        ProgrammeName = "Софтуерно инженерство",
                        DepartmentId = 6
                    },
                    new ProgrammeModel
                    {
                        ProgrammeName = "Право",
                        DepartmentId = 7  
                    }, new ProgrammeModel
                    {
                        ProgrammeName = "Кинезитерапия",
                        DepartmentId = 8
                    });
                context.SaveChanges();
            }

            if (!context.DegreesDBS.Any())
            {

                context.DegreesDBS.AddRange(
                    
                    new DegreeModel
                    {
                        ProgrammeId = context.ProgrammesDBS.ToList().ElementAt(0).Id,
                        Degree = DegreeEnum.Bachelor
                    },
                    new DegreeModel
                    {
                        ProgrammeId = context.ProgrammesDBS.ToList().ElementAt(1).Id,
                        Degree = DegreeEnum.Master
                    },
                    new DegreeModel
                    {   
                        ProgrammeId = context.ProgrammesDBS.ToList().ElementAt(2).Id,
                        Degree = DegreeEnum.Master
                    },
                    new DegreeModel
                    {
                        ProgrammeId = context.ProgrammesDBS.ToList().ElementAt(3).Id,
                        Degree = DegreeEnum.Master
                    },
                    new DegreeModel
                    {
                        ProgrammeId = context.ProgrammesDBS.ToList().ElementAt(4).Id,
                        Degree = DegreeEnum.Bachelor
                    }, 
                    new DegreeModel
                    {
                        ProgrammeId = context.ProgrammesDBS.ToList().ElementAt(5).Id,  
                        Degree = DegreeEnum.Master
                    },
                    new DegreeModel
                    {
                        ProgrammeId = context.ProgrammesDBS.ToList().ElementAt(6).Id,
                        Degree = DegreeEnum.Master
                    },
                    new DegreeModel
                    {
                        ProgrammeId = context.ProgrammesDBS.ToList().ElementAt(7).Id,
                        Degree = DegreeEnum.Bachelor
                    }
                );
                context.SaveChanges();
            }

            if (!context.ThesisDBS.Any())
            {

                context.ThesisDBS.AddRange(
                    new ThesisModel
                    {
                        Title = "DCDN: Distributed content delivery for the modern web",
                        Description = "This thesis explores the state of the art in browser-based P2P content delivery for the web and seeks to answer whether such systems can be used to efficiently and invisibly deliver content.",
                        AssignDate = DateTime.Now,
                        DefendDate = DateTime.Parse("5-6-2014"),
                        Grade = 7.99M,
                        Tags = "p2p,web,dcdn,content delivery",
                        DegreeId = context.DegreesDBS.AsNoTracking().ToList().ElementAt(6).Id,
                        Status = StatusEnum.Archived
                    },
                    new ThesisModel
                    {
                        Title = "Diploma 2",
                        Description = "desc2",
                        DegreeId = context.DegreesDBS.AsNoTracking().ToList().ElementAt(2).Id,
                        Status = StatusEnum.InAppraisal,
                        AssignDate = DateTime.Now.AddDays(-30),
                        DefendDate = DateTime.Parse("20-12-2024")
                    },
                    new ThesisModel
                    {
                        Title = "Diploma 3",
                        Description = "desc3",
                        Status = StatusEnum.WIP,
                        DegreeId = context.DegreesDBS.AsNoTracking().ToList().ElementAt(1).Id,
                        Tags = "tag303,tag3,tag33"
                    },
                    new ThesisModel
                    {
                        Title = "Diploma 4",
                        Description = "desc4",
                        DegreeId = context.DegreesDBS.AsNoTracking().ToList().ElementAt(7).Id,
                        Status = StatusEnum.Posted
                    });
                context.SaveChanges();
            }

            if(!context.AssignedThesesDBS.Any())
            {
                context.AssignedThesesDBS.AddRange(
                new AssignedThesisModel
                {
                    ThesisID = context.ThesisDBS.AsNoTracking().ToList().ElementAt(0).ThesisID,
                    TeacherID = context.TeachersDBS.AsNoTracking().ToList().ElementAt(0).Id,
                    StudentID = context.StudentsDBS.AsNoTracking().ToList().ElementAt(0).Id
                },
                new AssignedThesisModel
                {
                    ThesisID = context.ThesisDBS.AsNoTracking().ToList().ElementAt(1).ThesisID,
                    TeacherID = context.TeachersDBS.AsNoTracking().ToList().ElementAt(0).Id,
                    StudentID = null
                }, new AssignedThesisModel
                {
                    ThesisID = context.ThesisDBS.AsNoTracking().ToList().ElementAt(2).ThesisID,
                    TeacherID = null,
                    StudentID = context.StudentsDBS.AsNoTracking().ToList().ElementAt(0).Id
                }, new AssignedThesisModel
                {
                    ThesisID = context.ThesisDBS.AsNoTracking().ToList().ElementAt(3).ThesisID,
                    TeacherID = null,
                    StudentID = null
                }
                );
                context.SaveChanges();
            }

        }
    }

    public static async Task SeedRolesAsync(RoleManager<IdentityRole<Guid>> roleManager)
    {
        //Seed Roles
        await roleManager.CreateAsync(new IdentityRole<Guid>(MyRolesEnum.Student.ToString()));
        await roleManager.CreateAsync(new IdentityRole<Guid>(MyRolesEnum.Teacher.ToString()));
        await roleManager.CreateAsync(new IdentityRole<Guid>(MyRolesEnum.Admin.ToString()));
    }


    public static async Task AssignRolesAsync(DiplomaSite3Context context, UserManager<UserModel> userManager)
    {
        var admins = await context.AdminsDBS.ToListAsync();
        foreach (var admin in admins)
        {
            await userManager.AddToRoleAsync(admin, MyRolesEnum.Admin.ToString());
            Console.Out.WriteLine("admin role:"+ admin.FullName);
        }
        
        var teachers = await context.TeachersDBS.ToListAsync();
        foreach (var teacher in teachers)
        {
            await userManager.AddToRoleAsync(teacher, MyRolesEnum.Teacher.ToString());
            Console.Out.WriteLine("teacher role:" + teacher.FullName);
        }
        
        var students = await context.StudentsDBS.ToListAsync();
        foreach (var student in students)
        {
            await userManager.AddToRoleAsync(student, MyRolesEnum.Student.ToString());
            Console.Out.WriteLine("student role:" + student.FullName);
        }
    }
}