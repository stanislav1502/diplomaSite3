
using DiplomaSite3.Models;
using DiplomaSite3.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Security.Cryptography;
using System.Text;
using NuGet.DependencyResolver;
using Microsoft.EntityFrameworkCore.Storage;
using System.Numerics;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NuGet.Protocol.Plugins;
using DiplomaSite3.Areas.Identity.Pages.Account;

namespace DiplomaSite3.Data;

public static class SeedDB
{
    public static async void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new DiplomaSite3Context(
            serviceProvider.GetRequiredService<
                DbContextOptions<DiplomaSite3Context>>()))
        {

            if (!context.UsersDBS.Any())
            {
                UserModel user = Activator.CreateInstance<AdminModel>();
                string username = "Admin";
                string useremail= "admin@admin.haha";
                string userpass = "admin1-Admin";
                string userfirstname = "admin";
                string userlastname = "adminov";
                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
                await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Admin;
                await context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);


                user = Activator.CreateInstance<TeacherModel>();
                username = "Teacher";
                useremail = "teach@teach.haha";
                userpass = ",a'%4n,k9Yp&#X\"";
                userfirstname = "Teacher";
                userlastname = "Teacherov";
                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
                await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Teacher;
                await context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);

                user = Activator.CreateInstance<AdminModel>();
                username = "Student";
                useremail= "student@stud.haha";
                userpass = ",9BkiGkf2s:kSn7";
                userfirstname = "student";
                userlastname = "Studentski";
                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
                await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Student;
                await context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);


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
                        TeacherID = (await context.TeachersDBS.FirstAsync()).Id,
                        StudentID = (await context.StudentsDBS.FirstAsync()).Id
                    },
                    new DiplomaModel
                    {
                        Title = "Diploma 2",
                        Description = "desc2",
                        Status = StatusEnum.InAppraisal,

                        DefendDate = DateTime.Parse("20-12-2024"),
                        StudentID = null
                    },
                    new DiplomaModel
                    {
                        Title = "Diploma 3",
                        Description = "desc3",
                        Status = StatusEnum.WIP,

                        Tags = "tag303,tag3,tag33",
                        TeacherID = null,
                        StudentID = null
                    },
                    new DiplomaModel
                    {
                        Title = "Diploma 4",
                        Description = "desc4",
                        Status = StatusEnum.Posted,

                        TeacherID = null
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