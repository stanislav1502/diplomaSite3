
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
                var admins = new List<AdminModel>();

                AdminModel user = Activator.CreateInstance<AdminModel>();
                string username = "Admin";
                string useremail = "admin-iit@uni-ruse.bg";
                string userpass = "admin1-Admin1";
                string userfirstname = "Администраторски";
                string userlastname = "Профил";

                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
               // await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Admin;
                user.EmailConfirmed = true;
                user.Verified = true;
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);
                admins.Add(user);

                user = Activator.CreateInstance<AdminModel>();
                username = "Admin2";
                useremail = "admin2-iit@uni-ruse.bg";
                userpass = "Аdmin2-аdmin2";
                userfirstname = "Админ";
                userlastname = "две";

                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
                // await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Admin;
                user.EmailConfirmed = true;
                user.Verified = true;
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);
                admins.Add(user);


                context.SaveChanges();
            }

            if (!context.TeachersDBS.Any())
            {
                var teachers = new List<TeacherModel>();

                TeacherModel user = Activator.CreateInstance<TeacherModel>();
                string username = "RRusev";
                string useremail = "rir@uni-ruse.bg";
                string userpass = "parolarrusev";
                string userfirstname = "Румен";
                string userlastname = "Русев";

                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
                //  await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Teacher;
                user.Verified = true;
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);
                user.EmailConfirmed=true;
                teachers.Add(user);


                user = Activator.CreateInstance<TeacherModel>();
                username = "CVasilev";
                useremail = "tvassilev@uni-ruse.bg";
                userpass = "parolacvasilev";
                userfirstname = "Цветомир";
                userlastname = "Василев";

                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
             //   await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Teacher;
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);
                teachers.Add(user);

                

                username = "VVoinohovska";
                useremail = "vvoinohovska@uni-ruse.bg";
                userpass = "parolavvoinohovska";
                userfirstname = "Валентина";
                userlastname = "Войноховска";

                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
              //  await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Teacher;
                 context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);
                teachers.Add(user);


                user = Activator.CreateInstance<TeacherModel>();
                username = "GAtanasova";
                useremail = "gatanasova@uni-ruse.bg";
                userpass = "parolagatanasova";
                userfirstname = "Галина";
                userlastname = "Атанасова";

                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
              //  await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Teacher;
                 context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);
                teachers.Add(user);


                user = Activator.CreateInstance<TeacherModel>();
                username = "DAtanasova";
                useremail = "datanasova@uni-ruse.bg";
                userpass = "paroladatanasova";
                userfirstname = "Десислава";
                userlastname = "Атанасова";

                 await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
              // await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Teacher;
                 context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);
                teachers.Add(user);


                user = Activator.CreateInstance<TeacherModel>();
                username = "DBaeva";
                useremail = "dbaeva@uni-ruse.bg";
                userpass = "paroladbaeva";
                userfirstname = "Десислава";
                userlastname = "Баева";

                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
             //  await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Teacher;
                 context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);
                teachers.Add(user);


                user = Activator.CreateInstance<TeacherModel>();
                username = "STsankov";
                useremail = "stsankov@uni-ruse.bg";
                userpass = "parolastsankov";
                userfirstname = "Светлозар";
                userlastname = "Цанков";

                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
              //  await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Teacher;
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);
                teachers.Add(user);


                user = Activator.CreateInstance<TeacherModel>();
                username = "VVelikov";
                useremail = "vvelikov@uni-ruse.bg";
                userpass = "parolavvelikov";
                userfirstname = "Валентин";
                userlastname = "Великов";

                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
              //  await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Teacher;
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);
                teachers.Add(user);


                user = Activator.CreateInstance<TeacherModel>();
                username = "KShoylekova";
                useremail = "kshoylekova@uni-ruse.bg";
                userpass = "parolakshoylekova";
                userfirstname = "Камелия";
                userlastname = "Шойлекова";

                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
             //   await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Teacher;
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);
                teachers.Add(user);


                user = Activator.CreateInstance<TeacherModel>();
                username = "MAndreeva";
                useremail = "mhandreeva@uni-ruse.bg";
                userpass = "parolamandreeva";
                userfirstname = "Магдалена";
                userlastname = "Андреева";

                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
              //  await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Teacher;
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);
                teachers.Add(user);


                user = Activator.CreateInstance<TeacherModel>();
                username = "SAntonov";
                useremail = "santonov@uni-ruse.bg";
                userpass = "parolasantonov";
                userfirstname = "Сергей";
                userlastname = "Антонов";

                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
            //    await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Teacher;
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);
                teachers.Add(user);


                user = Activator.CreateInstance<TeacherModel>();
                username = "BIvanova";
                useremail = "bivanova@uni-ruse.bg";
                userpass = "parolabivanova";
                userfirstname = "Бояна";
                userlastname = "Иванова";

                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
              //  await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Teacher;
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);
                teachers.Add(user);


                user = Activator.CreateInstance<TeacherModel>();
                username = "IKamenarov";
                useremail = "ikamenarov@uni-ruse.bg";
                userpass = "parolaikamenarov";
                userfirstname = "Ивайло";
                userlastname = "Каменаров";

                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
             //   await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Teacher;
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);
                teachers.Add(user);


                user = Activator.CreateInstance<TeacherModel>();
                username = "SKostadinov";
                useremail = "sdkostadinov@uni-ruse.bg";
                userpass = "parolaskostadinov";
                userfirstname = "Станислав";
                userlastname = "Костадинов";

                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
              //  await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Teacher;
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);
                teachers.Add(user);


                user = Activator.CreateInstance<TeacherModel>();
                username = "MDimitrov";
                useremail = "mdimitrov@uni-ruse.bg";
                userpass = "parolamdimitrov";
                userfirstname = "Методи";
                userlastname = "Димитров";

                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
               // await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Teacher;
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);
                teachers.Add(user);


                user = Activator.CreateInstance<TeacherModel>();
                username = "KGabrovska";
                useremail = "kgabrovska@uni-ruse.bg";
                userpass = "parolakgabrovska";
                userfirstname = "Катерина";
                userlastname = "Габровска";

                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
             //   await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Teacher;
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);
                teachers.Add(user);


                user = Activator.CreateInstance<TeacherModel>();
                username = "EMinev";
                useremail = "eminev@uni-ruse.bg";
                userpass = "parolaeminev";
                userfirstname = "Екатерин";
                userlastname = "Минев";

                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
              //  await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Teacher;
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);
                teachers.Add(user);

        context.SaveChanges();

            }

            if (!context.StudentsDBS.Any())
            {
                var students = new List<StudentModel>();

                StudentModel user = Activator.CreateInstance<StudentModel>();
                string username = "SIliev";
                string useremail = "s196286@stud.uni-ruse.bg";
                string userpass = "123456789+0";
                string userfirstname = "Станислав";
                string userlastname = "Илиев";

                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
                //await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Student;
                user.FacultyNumber = "196286";
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);
                user.EmailConfirmed = true;
                students.Add(user);


                user = Activator.CreateInstance<StudentModel>();
                username = "MDjurov";
                useremail = "s196250@stud.uni-ruse.bg";
                userpass = "parolamdjurov";
                userfirstname = "Мартин";
                userlastname = "Джуров";

                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
                //   await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Student;
                user.FacultyNumber = "196250";
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);
                students.Add(user);


                user = Activator.CreateInstance<StudentModel>();
                username = "JGanev";
                useremail = "s196272@stud.uni-ruse.bg";
                userpass = "parolajganev";
                userfirstname = "Йордан";
                userlastname = "Ганев";

                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
                //   await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Student;
                user.FacultyNumber = "196272";
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);
                students.Add(user);


                user = Activator.CreateInstance<StudentModel>();
                username = "RPetkova";
                useremail = "s196259@stud.uni-ruse.bg";
                userpass = "parolarpetkova";
                userfirstname = "Радина";
                userlastname = "Петкова";

                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
                //   await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Student;
                user.FacultyNumber = "196259";
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);

                students.Add(user);


                user = Activator.CreateInstance<StudentModel>();
                username = "KSpasov";
                useremail = "s196262@stud.uni-ruse.bg";
                userpass = "parolakspasov";
                userfirstname = "Кристиан";
                userlastname = "Спасов";

                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
                //   await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Student;
                user.FacultyNumber = "196262";
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);
                students.Add(user);


                user = Activator.CreateInstance<StudentModel>();
                username = "SSahmi";
                useremail = "s206215@stud.uni-ruse.bg";
                userpass = "parolassahmi";
                userfirstname = "Сезен";
                userlastname = "Сахми";

                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
                //   await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Student;
                user.FacultyNumber = "206215";
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);
                students.Add(user);


                user = Activator.CreateInstance<StudentModel>();
                username = "VNikolov";
                useremail = "s196265@stud.uni-ruse.bg";
                userpass = "parolavnikolov";
                userfirstname = "Венцислав";
                userlastname = "Николов";

                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
                //   await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Student;
                user.FacultyNumber = "196265";
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass);
                students.Add(user);


             
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
                FacultyModel[] list = new FacultyModel[]{
                    new FacultyModel
                    {
                        Id=1,
                        FacultyName = "Аграрно-индустриален"
                    },
                    new FacultyModel
                    {
                        Id=2,
                        FacultyName = "Машинно-технологичен"
                    },
                    new FacultyModel
                    {
                        Id=3,
                        FacultyName = "Електротехника, електроника и автоматика"
                    },
                    new FacultyModel
                    {
                        Id=4,
                        FacultyName = "Транспортен"
                    },
                    new FacultyModel
                    {
                            Id=5,
                        FacultyName = "Бизнес и мениджмънт"
                    },
                    new FacultyModel
                    {
                        Id=6,
                        FacultyName = "Природни науки и образование"
                    },
                    new FacultyModel
                    {
                        Id=7,
                        FacultyName = "Юридически"
                    },
                    new FacultyModel
                    {
                        Id=8,
                        FacultyName = "Обществено здраве и здравни грижи​​"
                    } 
                };
                
              context.FacultiesDBS.AddRange(list);
                context.SaveChanges();
            }

            if (!context.DepartmentsDBS.Any())
            {
                context.DepartmentsDBS.AddRange(
                    new DepartmentModel
                    {
                        Id = 1,
                        DepartmentName = "Земеделска техника",
                        FacultyId = 1,
                    },
                    new DepartmentModel
                    {
                        Id = 2,
                        DepartmentName = "Материалознание и технология на материалите",
                        FacultyId = 2,
                    },
                    new DepartmentModel
                    {
                        Id = 3,
                        DepartmentName = "Електроника",
                        FacultyId = 3,
                    },
                    new DepartmentModel
                    {
                        Id = 4,
                        DepartmentName = "Двигатели и транспортна техника",
                        FacultyId = 4,
                    },
                    new DepartmentModel
                    {
                        Id = 5,
                        DepartmentName = "Икономика и международни отношения",
                        FacultyId = 5,
                    },
                    new DepartmentModel
                    {
                        Id = 6,
                        DepartmentName = "Информатика и информационни технологии",
                        FacultyId = 6,
                    },
                    new DepartmentModel
                    {
                        Id = 7,
                        DepartmentName = "Публичноправни науки",
                        FacultyId = 7,
                    },
                    new DepartmentModel
                    {
                        Id = 8,
                        DepartmentName = "Обществено здраве",
                        FacultyId = 8,
                    },
                    new DepartmentModel
                    {
                        Id = 9,
                        DepartmentName = "Педагогика, психология и история",
                        FacultyId = 6,
                    },
                    new DepartmentModel
                    {
                        Id = 10,
                        DepartmentName = "Български език, литература и изкуство",
                        FacultyId = 6,
                    },
                    new DepartmentModel
                    {
                        Id = 11,
                        DepartmentName = "Математика",
                        FacultyId = 6,
                    },
                   new DepartmentModel
                   {
                       Id = 12,
                       DepartmentName = "Приложна математика и статистика",
                       FacultyId = 6,
                   });
                context.SaveChanges();
            }

            if (!context.ProgrammesDBS.Any())
            {
                context.ProgrammesDBS.AddRange(
                    new ProgrammeModel
                    {Id=1,
                        ProgrammeName = "Земеделска техника и технологии",
                        Department = await context.DepartmentsDBS.FindAsync(0),
                    },
                    new ProgrammeModel
                    {
                        Id = 2,
                        ProgrammeName = "Материалознание и технологии",
                        Department = await context.DepartmentsDBS.FindAsync(1),
                    },
                    new ProgrammeModel
                    {Id = 3,
                        ProgrammeName = "Електронизация",
                        Department = await context.DepartmentsDBS.FindAsync(2)
                    },
                    new ProgrammeModel
                    {Id = 4,
                        ProgrammeName = "Автомобилна техника",
                        Department = await context.DepartmentsDBS.FindAsync(3)
                    },
                    new ProgrammeModel
                    {Id = 5,
                        ProgrammeName = "Политическа икономия",
                        Department = await context.DepartmentsDBS.FindAsync(4)
                    },
                    new ProgrammeModel
                    {Id = 6,
                        ProgrammeName = "Компютърни науки",
                        Department = await context.DepartmentsDBS.FindAsync(5)
                    },
                    new ProgrammeModel
                    {Id = 7,
                        ProgrammeName = "Право",
                        Department = await context.DepartmentsDBS.FindAsync(6)
                    }, new ProgrammeModel
                    {
                        Id = 8,
                        ProgrammeName = "Кинезитерапия",
                        Department = await context.DepartmentsDBS.FindAsync(7)
                    },
                     new ProgrammeModel
                     {
                         Id = 9,
                         ProgrammeName = "Информатика и информационни технологии в бизнеса",
                         Department = await context.DepartmentsDBS.FindAsync(5)
                     },
                    new ProgrammeModel
                    {
                        Id = 10,
                        ProgrammeName = "Софтуерно инженерство",
                        Department = await context.DepartmentsDBS.FindAsync(5)
                    }, new ProgrammeModel
                    {
                        Id = 11,
                        ProgrammeName = "Информатика",
                        Department = await context.DepartmentsDBS.FindAsync(5)
                    },
                     new ProgrammeModel
                     {Id = 12,
                         ProgrammeName = "Информатика и информационни технологии в образованието",
                         Department = await context.DepartmentsDBS.FindAsync(5)
                     });
                context.SaveChanges();
            }

            if (!context.DegreesDBS.Any())
            {
                context.DegreesDBS.AddRange(
                    new DegreeModel
                    {
                        Id=1,
                        FacultyId= 1,
                        DepartmentId=1,
                        ProgrammeId = 1,
                        Degree = DegreeEnum.Bachelor
                    },
                    new DegreeModel
                    {
                        Id = 2,
                        FacultyId = 2,
                        DepartmentId = 2,
                        ProgrammeId = 2,
                        Degree = DegreeEnum.Master
                    },
                    new DegreeModel
                    {
                        Id = 3,
                        FacultyId = 3,
                        DepartmentId =3,
                        ProgrammeId = 3,
                        Degree = DegreeEnum.Master
                    },
                    new DegreeModel
                    {
                        Id = 4,
                        FacultyId = 4,
                        DepartmentId =4,
                        ProgrammeId =4,
                        Degree = DegreeEnum.Master
                    },
                    new DegreeModel
                    {
                        Id = 5,
                        FacultyId = 5,
                        DepartmentId = 5,
                        ProgrammeId =5,
                        Degree = DegreeEnum.Bachelor
                    }, 
                    new DegreeModel
                    {
                        Id = 6,
                        FacultyId =6,
                        DepartmentId =6,
                        ProgrammeId = 6,  
                        Degree = DegreeEnum.Bachelor
                    },
                    new DegreeModel
                    {
                        Id = 7,
                        FacultyId = 7,
                        DepartmentId = 7,
                        ProgrammeId = 7,
                        Degree = DegreeEnum.Master
                    },
                    new DegreeModel
                    {
                        Id = 8,
                        FacultyId = 8,
                        DepartmentId = 8,
                        ProgrammeId = 8,
                        Degree = DegreeEnum.Bachelor
                    },
                    new DegreeModel
                    {
                        Id = 9,
                        FacultyId = 6,
                        DepartmentId = 6,
                        ProgrammeId = 9,
                        Degree = DegreeEnum.Bachelor
                    },
                    new DegreeModel
                    {
                        Id = 10,
                        FacultyId = 6,
                        DepartmentId = 6,
                        ProgrammeId = 10,
                        Degree = DegreeEnum.Bachelor
                    },
                    new DegreeModel
                    {
                        Id = 11,
                        FacultyId = 6,
                        DepartmentId = 6,
                        ProgrammeId = 10,
                        Degree = DegreeEnum.Master
                    },
                    new DegreeModel
                    {
                        Id = 12,
                        FacultyId = 6,
                        DepartmentId = 6,
                        ProgrammeId = 11,
                        Degree = DegreeEnum.Master
                    }, 
                    new DegreeModel
                    {
                        Id = 13,
                        FacultyId = 6,
                        DepartmentId = 6,
                        ProgrammeId = 12,
                        Degree = DegreeEnum.Master
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
                        DegreeId = 7,
                        Status = StatusEnum.Archived
                    }
                    );
                context.SaveChanges();
            }

            if(!context.AssignedThesesDBS.Any())
            {

                var assigned = new List<AssignedThesisModel>();


                context.SaveChanges();
            }

            context.Dispose();
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