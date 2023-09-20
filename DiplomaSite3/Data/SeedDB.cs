
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
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();
               

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
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();

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
                user.EmailConfirmed = true;
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();
               
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
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();

                user = Activator.CreateInstance<TeacherModel>();
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
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();
            
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
context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();
            
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
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();
                
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
                 context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();
                
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
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();
                
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
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();
                
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
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();

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
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();

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
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();

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
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();


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
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();

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
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();

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
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();

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
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();

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
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();

                user = Activator.CreateInstance<TeacherModel>();
                username = "VKozov";
                useremail = "vkozov@uni-ruse.bg";
                userpass = "parolavkozov";
                userfirstname = "Васил";
                userlastname = "Козов";
                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
                //  await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Teacher;
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();

                context.SaveChanges();
            }

            if (!context.StudentsDBS.Any())
            {
                
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
                user.EmailConfirmed = true;
               context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();


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
                user.EmailConfirmed = false;
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();

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
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();


                user = Activator.CreateInstance<StudentModel>();
                username = "SRedjeb";
                useremail = "s196201@stud.uni-ruse.bg";
                userpass = "parolasredjeb";
                userfirstname = "Салих";
                userlastname = "Реджеб";
                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
                //   await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Student;
                user.FacultyNumber = "196201";
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();


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
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();


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
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();

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
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();

                user = Activator.CreateInstance<StudentModel>();
                username = "DDiankov";
                useremail = "s196256@stud.uni-ruse.bg";
                userpass = "paroladdiankov";
                userfirstname = "Дилян";
                userlastname = "Дянков";
                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
                //   await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Student;
                user.FacultyNumber = "196256";
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();


                user = Activator.CreateInstance<StudentModel>();
                username = "AKovadjiev";
                useremail = "s196255@stud.uni-ruse.bg";
                userpass = "parolaakovadjiev";
                userfirstname = "Адриян";
                userlastname = "Коваджиев";
                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
                //   await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Student;
                user.FacultyNumber = "196255";
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();


                user = Activator.CreateInstance<StudentModel>();
                username = "KArsov";
                useremail = "s196274@stud.uni-ruse.bg";
                userpass = "parolakarsov";
                userfirstname = "Кристиян";
                userlastname = "Арсов";
                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
                //   await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Student;
                user.FacultyNumber = "196274";
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();


                user = Activator.CreateInstance<StudentModel>();
                username = "VZlatev";
                useremail = "s196257@stud.uni-ruse.bg";
                userpass = "parolavzlatev";
                userfirstname = "Венцеслав";
                userlastname = "Златев";
                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
                //   await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Student;
                user.FacultyNumber = "196257";
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();


                user = Activator.CreateInstance<StudentModel>();
                username = "KAndreev";
                useremail = "s196268@stud.uni-ruse.bg";
                userpass = "parolakandreev";
                userfirstname = "Камен";
                userlastname = "Андреев";
                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
                //   await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Student;
                user.FacultyNumber = "196268";
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();


                user = Activator.CreateInstance<StudentModel>();
                username = "SDimitrov";
                useremail = "s196269@stud.uni-ruse.bg";
                userpass = "parolasdimitrov";
                userfirstname = "Станислав";
                userlastname = "Димитров";
                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
                //   await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Student;
                user.FacultyNumber = "196269";
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();


                user = Activator.CreateInstance<StudentModel>();
                username = "NTodorov";
                useremail = "s196290@stud.uni-ruse.bg";
                userpass = "parolantodorov";
                userfirstname = "Николай";
                userlastname = "Тодоров";
                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
                //   await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Student;
                user.FacultyNumber = "196290";
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();

                user = Activator.CreateInstance<StudentModel>();
                username = "RKolev";
                useremail = "s196288@stud.uni-ruse.bg";
                userpass = "parolaRKolev";
                userfirstname = "Ростислав";
                userlastname = "Колев";
                await context.GetService<IUserStore<UserModel>>().SetUserNameAsync(user, username, CancellationToken.None);
                //   await context.GetService<IUserEmailStore<UserModel>>().SetEmailAsync(user, useremail, CancellationToken.None);
                user.Email = useremail;
                user.NormalizedEmail = useremail.Normalize();
                user.FirstName = userfirstname;
                user.LastName = userlastname;
                user.UserType = MyRolesEnum.Student;
                user.FacultyNumber = "196288";
                context.GetService<UserManager<UserModel>>().CreateAsync(user, userpass).GetAwaiter().GetResult();

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
             
                        DepartmentName = "Земеделска техника",
                        Faculty = await context.FacultiesDBS.FindAsync(1)
                    },
                    new DepartmentModel
                    {
                        DepartmentName = "Материалознание и технология на материалите",
                        Faculty = await context.FacultiesDBS.FindAsync(2)
                    },
                    new DepartmentModel
                    {
                        DepartmentName = "Електроника",
                        Faculty = await context.FacultiesDBS.FindAsync(3)
                    },
                    new DepartmentModel
                    {
                        DepartmentName = "Двигатели и транспортна техника",
                        Faculty = await context.FacultiesDBS.FindAsync(4)
                    },
                    new DepartmentModel
                    {
                            DepartmentName = "Икономика и международни отношения",
                        Faculty = await context.FacultiesDBS.FindAsync(5)
                    },
                    new DepartmentModel
                    {
                        DepartmentName = "Информатика и информационни технологии",
                        Faculty = await context.FacultiesDBS.FindAsync(6)
                    },
                    new DepartmentModel
                    {
                        DepartmentName = "Публичноправни науки",
                        Faculty = await context.FacultiesDBS.FindAsync(7)
                    },
                    new DepartmentModel
                    {
                        DepartmentName = "Обществено здраве",
                        Faculty = await context.FacultiesDBS.FindAsync(8)
                    },
                    new DepartmentModel
                    {
                        DepartmentName = "Педагогика, психология и история",
                        Faculty = await context.FacultiesDBS.FindAsync(6)
                    },
                    new DepartmentModel
                    {
                        DepartmentName = "Български език, литература и изкуство",
                        Faculty = await context.FacultiesDBS.FindAsync(6)
                    },
                    new DepartmentModel
                    {
                        DepartmentName = "Математика",
                        Faculty = await context.FacultiesDBS.FindAsync(6)
                    },
                   new DepartmentModel
                   {
                       DepartmentName = "Приложна математика и статистика",
                       Faculty = await context.FacultiesDBS.FindAsync(6)
                   });
                context.SaveChanges();
            }

            if (!context.ProgrammesDBS.Any())
            {
                context.ProgrammesDBS.AddRange(
                    new ProgrammeModel
                    { 
                        ProgrammeName = "Земеделска техника и технологии",
                        Department = await context.DepartmentsDBS.FindAsync(1),
                    },
                    new ProgrammeModel
                    {
                        ProgrammeName = "Материалознание и технологии",
                        Department = await context.DepartmentsDBS.FindAsync(2),
                    },
                    new ProgrammeModel
                    { ProgrammeName = "Електронизация",
                        Department = await context.DepartmentsDBS.FindAsync(3)
                    },
                    new ProgrammeModel
                    { ProgrammeName = "Автомобилна техника",
                        Department = await context.DepartmentsDBS.FindAsync(4)
                    },
                    new ProgrammeModel
                    { ProgrammeName = "Политическа икономия",
                        Department = await context.DepartmentsDBS.FindAsync(5)
                    },
                    new ProgrammeModel
                    { ProgrammeName = "Компютърни науки",
                        Department = await context.DepartmentsDBS.FindAsync(6)
                    },
                    new ProgrammeModel
                    { ProgrammeName = "Право",
                        Department = await context.DepartmentsDBS.FindAsync(7)
                    }, new ProgrammeModel
                    {
                        ProgrammeName = "Кинезитерапия",
                        Department = await context.DepartmentsDBS.FindAsync(8)
                    },
                     new ProgrammeModel
                     {
                         ProgrammeName = "Информатика и информационни технологии в бизнеса",
                         Department = await context.DepartmentsDBS.FindAsync(6)
                     },
                    new ProgrammeModel
                    {
                        ProgrammeName = "Софтуерно инженерство",
                        Department = await context.DepartmentsDBS.FindAsync(6)
                    }, new ProgrammeModel
                    {
                        ProgrammeName = "Информатика",
                        Department = await context.DepartmentsDBS.FindAsync(6)
                    },
                     new ProgrammeModel
                     { ProgrammeName = "Информатика и информационни технологии в образованието",
                         Department = await context.DepartmentsDBS.FindAsync(6)
                     });
                context.SaveChanges();
            }

            if (!context.DegreesDBS.Any())
            {
                context.DegreesDBS.AddRange(
                    new DegreeModel
                    {
                        Faculty = await context.FacultiesDBS.FindAsync(1),
                        Department = await context.DepartmentsDBS.FindAsync(1),
                        Programme = await context.ProgrammesDBS.FindAsync(1),
                        Degree = DegreeEnum.Bachelor
                    },
                    new DegreeModel
                    {
                        Faculty = await context.FacultiesDBS.FindAsync(2),
                        Department = await context.DepartmentsDBS.FindAsync(2),
                        Programme = await context.ProgrammesDBS.FindAsync(2),
                        Degree = DegreeEnum.Master
                    },
                    new DegreeModel
                    {
                        Faculty = await context.FacultiesDBS.FindAsync(3),
                        Department = await context.DepartmentsDBS.FindAsync(3),
                        Programme = await context.ProgrammesDBS.FindAsync(3),
                        Degree = DegreeEnum.Master
                    },
                    new DegreeModel
                    {
                        Faculty = await context.FacultiesDBS.FindAsync(4),
                        Department = await context.DepartmentsDBS.FindAsync(4),
                        Programme = await context.ProgrammesDBS.FindAsync(4),
                        Degree = DegreeEnum.Master
                    },
                    new DegreeModel
                    {
                        Faculty = await context.FacultiesDBS.FindAsync(5),
                        Department = await context.DepartmentsDBS.FindAsync(5),
                        Programme = await context.ProgrammesDBS.FindAsync(5),
                        Degree = DegreeEnum.Bachelor
                    },
                    new DegreeModel
                    {
                        Faculty = await context.FacultiesDBS.FindAsync(6),
                        Department = await context.DepartmentsDBS.FindAsync(6),
                        Programme = await context.ProgrammesDBS.FindAsync(6),
                        Degree = DegreeEnum.Bachelor
                    },
                    new DegreeModel
                    {
                        Faculty = await context.FacultiesDBS.FindAsync(7),
                        Department = await context.DepartmentsDBS.FindAsync(7),
                        Programme = await context.ProgrammesDBS.FindAsync(7),
                        Degree = DegreeEnum.Master
                    },
                    new DegreeModel
                    {
                        Faculty = await context.FacultiesDBS.FindAsync(8),
                        Department = await context.DepartmentsDBS.FindAsync(8),
                        Programme = await context.ProgrammesDBS.FindAsync(8),
                        Degree = DegreeEnum.Bachelor
                    },
                    new DegreeModel
                    {
                        Faculty = await context.FacultiesDBS.FindAsync(6),
                        Department = await context.DepartmentsDBS.FindAsync(6),
                        Programme = await context.ProgrammesDBS.FindAsync(9),
                        Degree = DegreeEnum.Bachelor
                    },
                    new DegreeModel
                    {
                        Faculty = await context.FacultiesDBS.FindAsync(6),
                        Department = await context.DepartmentsDBS.FindAsync(6),
                        Programme = await context.ProgrammesDBS.FindAsync(10),
                        Degree = DegreeEnum.Bachelor
                    },
                    new DegreeModel
                    {
                        Faculty = await context.FacultiesDBS.FindAsync(6),
                        Department = await context.DepartmentsDBS.FindAsync(6),
                        Programme = await context.ProgrammesDBS.FindAsync(10),
                        Degree = DegreeEnum.Master
                    },
                    new DegreeModel
                    {
                        Faculty = await context.FacultiesDBS.FindAsync(6),
                        Department = await context.DepartmentsDBS.FindAsync(6),
                        Programme = await context.ProgrammesDBS.FindAsync(11),
                        Degree = DegreeEnum.Master
                    },
                    new DegreeModel
                    {
                        Faculty = await context.FacultiesDBS.FindAsync(6),
                        Department = await context.DepartmentsDBS.FindAsync(6),
                        Programme = await context.ProgrammesDBS.FindAsync(12),
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
                        Title = "Информацонна система за управление процеса по дипломиране",
                        Description = "Информационна система, която осигурява поддържането да разнородни документи, свързани с организация на процеса на дипломиране на студентите и техен архив. Системата подпомага определянето на етапи и срокове",
                        AssignDate = DateTime.Parse("2-2-2023"),
                        DefendDate = DateTime.Parse("25-9-2023"),
                        Grade = null,
                        Degree = await context.DegreesDBS.FindAsync(10),
                        Status = StatusEnum.InAppraisal
                    },
                     new ThesisModel
                     {
                         Title = "Уеб базирано приложение за чат в реално време",
                         Description = "Уеб базирано приложение за чат в реално време, предназначена за масово ползване (подобно на вече съществуващи такива алтернативи като Messenger, Skype, Signal). Потребителите му ще имат възможността да се Вписват / Регистрират или да влязат в стая като гост, като стаите ще бъдат идентифицирани на базата на лесно разпостранимо ид. Индивидуалните стаи ще могат да се създават, активират / деактивират или изтриват от потребителя, който ги е създал. Ще бъде предоставена възможността стаята да бъде достъпна само срещу въведена парола. Уеб сокети ще бъдат използвани за осъществяване на пряката комуникация. Front-end часта на приложението ще бъде изградена чрез framework за Single Page Application. Тя ще комуникира с back-end система чрез JSON рекуести.",
                         AssignDate = DateTime.Parse("2-2-2023"),
                         DefendDate = DateTime.Parse("25-9-2023"),
                         Grade = 6.00M,
                         Degree = await context.DegreesDBS.FindAsync(6),
                         Status = StatusEnum.Archived
                     }
                     ,
                     new ThesisModel
                     {
                         Title = "Интелигентна система за интерактивно извеждане и управление на съдържание",
                         Description = "Система за мониторинг на музикални изпълнения (на сцена от музиканти, показва им се какво и кога трябва да свирят и пеят). Има един главен (водещ) музикант, който управлява списък от предварително зададени песни. Може да се добавят песни и в движение с мини клавиатура до монитора. През администраторски панел се добавят нови песни с техните акорди. Има две нива на администратор главен и модератор, като те имат различни права. Допълнително: Проектът ще има и хардуерна реализация и ще бъде тестван в реална среда, благодарение на доц. Димитър Грозев. Ситемата към момента ще работи изцяло в лакална мрежа, така че административния панел, да може да се достъпва от всяка машина в локалната мрежа, а часТта с визуализацията на въведеното съдържание да се достъпва само от машината, позиционирана на сцената.",
                         AssignDate = DateTime.Parse("2-2-2023"),
                         DefendDate = DateTime.Parse("25-9-2023"),
                         Grade = 6.0M,
                         Degree = await context.DegreesDBS.FindAsync(10),
                         Status = StatusEnum.Archived
                     },
                     new ThesisModel
                     {
                         Title = "Web-базирана мултимедийна информационна система \"Киоск-терминал\"",
                         Description = "Целта на проекта е да се създаде уеб базирана мултимедийна информационна система за използване от устройства тип киоск-терминали. Проектът ще съдържа клиентска и администраторска част. Клиентската част ще представлява общодостъпен touchfriendly уеб интефейс, за достъп до предлаганите услуги от крайните потребители. Администраторската част ще бъде представлявана от разработен CMS (Content Management System), който ще служи за промяна на видимото съдържание за крайния потребител в реално време. Това ще наложи наличието на два или повече потребителски типа - администратор и редактор, със съответен достъп до нужните йм функционалности от CMS . Проектът ще включва описание на хардуерна и мрежова инфраструктура, върху която успешно може да работи подобен тип информационна система. ",
                         AssignDate = DateTime.Parse("2-2-2023"),
                         DefendDate = DateTime.Parse("25-9-2023"),
                         Grade = 6.0M,
                         Degree = await context.DegreesDBS.FindAsync(10),
                         Status = StatusEnum.Archived
                     },
                     new ThesisModel
                     {
                         Title = "Разработване на софтуерна система за графично представяне и анализ на данни от измервания на геометрични отклонения на технологични проби.",
                         Description = "Системата ще обработва данните от модула за измерване и анализ на геометричните отклонения на технологични проби и ще ги представя в 3D графичен вид съпоставяйки ги с номиналната технологична проба. При откриване на необосновано големи отклонения в отделни участъци, дължащи се на случайни грешки от измерването, системата ще предлага автоматично или ръчно интерполиране на съответните координати. Ще бъде разработен специализиран модул за допълнителна статистическа оценка на точността на технологичната проба в специфични нейни области и направления задавани от потребителя.",
                         AssignDate = DateTime.Parse("2-2-2023"),
                         DefendDate = DateTime.Parse("25-9-2023"),
                         Grade = null,
                         Degree = await context.DegreesDBS.FindAsync(10),
                         Status = StatusEnum.InAppraisal
                     },
                     new ThesisModel
                     {
                         Title = "Уеб приложение за следене на фитнес резултати",
                         Description = "Уеб базираната система трябва да дава възможност за проследяването на фитнес резултати, като проследява физически показатели и активност, прети калории, меню за деня и др. Приложението трябва да поддържа няколко типа потребители - треньори, спортуващи и администратори.",
                         AssignDate = DateTime.Parse("2-2-2023"),
                         DefendDate = DateTime.Parse("25-9-2023"),
                         Grade = null,
                         Degree = await context.DegreesDBS.FindAsync(10),
                         Status = StatusEnum.InAppraisal
                     },
                     new ThesisModel
                     {
                         Title = "Софтуерна система за контрол при провеждане на онлайн практически занятия",
                         Description = "Софтуерна система за контрол при провеждане на онлайн практически занятия",
                         AssignDate = DateTime.Parse("2-2-2023"),
                         DefendDate = null,
                         Grade = null,
                         Degree = await context.DegreesDBS.FindAsync(10),
                         Status = StatusEnum.WIP
                     },
                     new ThesisModel
                     {
                         Title = "Discord бот - Studybuddy",
                         Description = "Дискорд бот, който помага на учениците в първи клас да учат. Благодарение на програмираните команди, децата ще могат да гледат клипчета, които ще им помагат по-лесно да усвояват материала по различните дисциплини.",
                         AssignDate = DateTime.Parse("2-2-2023"),
                         DefendDate = DateTime.Parse("25-9-2023"),
                         Grade = 6.0M,
                         Degree = await context.DegreesDBS.FindAsync(10),
                         Status = StatusEnum.Archived
                     },
                     new ThesisModel
                     {
                         Title = "Уеб-базирана система за обслужване на малка фирма",
                         Description = "Трябва да се проектира релационна БД с таблици: поръчки, клиенти, доставчици и т.н. с уеб интерфейс за въвеждане на данни, справки, актуализация. Два вида потребители: админ и обикновен. Студентът може да си избере технология от страна на сървъра и БД.",
                         AssignDate = DateTime.Parse("2-2-2023"),
                         DefendDate = null,
                         Grade = null,
                         Degree = await context.DegreesDBS.FindAsync(10),
                         Status = StatusEnum.WIP
                     },
                     new ThesisModel
                     {
                         Title = "Подсистема за генериране на 3D терен",
                         Description = "По зададени опорни точки в пространството да се генерира 3D терен. Опорните точки могат да се задават параметрично (като координати в пространството), или чрез мишката (т.е. - предварителна карта, като се отбелязват изохорните линии и възловитре точки). Поставят се възлови обекти (реки, езера и др.). Системата генерира път между 2 точки. Целта на генерирания терен по-нататъшното му използване в игри и симулации.",
                         AssignDate = DateTime.Parse("2-2-2023"),
                         DefendDate = DateTime.Parse("25-9-2023"),
                         Grade = 5.5M,
                         Degree = await context.DegreesDBS.FindAsync(6),
                         Status = StatusEnum.Archived
                     },
                     new ThesisModel
                     {
                         Title = "Електронна библиотека",
                         Description = "Акаунт за библиотекар и за читател. Възможност за създаване на категории (Наука, Технологии и др). Възможност за добавяне (изтриване) на книга в съответната категория. Всяка книга да има уникален код, пълно библиографско описание, изображение на корицата, колко броя са налични в библиотеката). Възможност за отбелязване дали книгата е свободна или заета при наличност от един брой и дата, до която книгата е ангажирана. Възможност за резервиране на книга от регистриран потребител-читател. Възможност за продължаване на срока на заемане на книгата. Възможност за изпращане на съобщения към читателите. Да има категория електронни ресурси, в която да може всеки регистриран потребител да добавя материали със свободен достъп. Възможност за оценяване на ресурса и публикуване на коментари.",
                         AssignDate = DateTime.Parse("2-2-2023"),
                         DefendDate = null,
                         Grade = null,
                         Degree = await context.DegreesDBS.FindAsync(10),
                         Status = StatusEnum.WIP
                     },
                     new ThesisModel
                     {
                         Title = "Програмна система за симулация на електропроизводството от фотоволтаична система",
                         Description = "Разработване на модул към система за моделиране и симулация на електропроизводството от фотоволтаична система",
                         AssignDate = null,
                         DefendDate = null,
                         Grade = null,
                         Degree = await context.DegreesDBS.FindAsync(10),
                         Status = StatusEnum.Posted
                     },
                     new ThesisModel
                     {
                         Title = "Създаване на приложение, използващо изкуствен интелект чрез облачните услуги на Azure",
                         Description = "Приложение, използващо предлаганите от Azure услуги за изкуствен интелект за създаване на приложение - например  приложение за анализиране на данни/снимки с обучителен алгоритъм или приложение свързано с прилагане на изкуствен интелект в работата с роботи.",
                         AssignDate = null,
                         DefendDate = null,
                         Grade = null,
                         Degree = await context.DegreesDBS.FindAsync(10),
                         Status = StatusEnum.Posted
                     },
                     new ThesisModel
                     {
                         Title = "Уеб-базирана система за нуждите на спортен клуб",
                         Description = "Системата поддържа актуална информация, касаеща състезателите. Специализирани функции: отчитане на посещения спрямо зададен диапазон от време; проведени медицински прегледи; кой състезател в коя категория попада. На базата на гореспоменатите дейност, приложението поддържа и детайлна справочна дейност. Възможност за следене на датите за състезания",
                         AssignDate = DateTime.Parse("2-2-2023"),
                         DefendDate = DateTime.Parse("25-9-2023"),
                         Grade = null,
                         Degree = await context.DegreesDBS.FindAsync(10),
                         Status = StatusEnum.InAppraisal
                     },
                     new ThesisModel
                     {
                         Title = "Web базирана система БРИКС",
                         Description = "Система за поддръжка на артикулна информация с възможност за генериране, визуализиране и калкулиране на цени за  цялостни проекти. Системата ще обслужва ателие за различни профили на рамки за картини и репродукции, гоблени, пана с приложни материали, огледала, икони. Калкулатор на цени според избран от потребителя вид артикул, размери, материал и т.н.",
                         AssignDate = DateTime.Parse("2-2-2023"),
                         DefendDate = DateTime.Parse("25-9-2023"),
                         Grade = null,
                         Degree = await context.DegreesDBS.FindAsync(10),
                         Status = StatusEnum.Done
                     }
                    );
            }
            context.SaveChanges();

            if (!context.AssignedThesesDBS.Any())
            {
                List<AssignedThesisModel> theses = new List<AssignedThesisModel>();
                Thread.Sleep(2000);

                var thesis = new AssignedThesisModel();
                thesis.ThesisID = (await context.ThesisDBS.Where(d => d.Title.Contains("Информацонна система за управление процеса по дипломиране")).FirstAsync()).ThesisID;
                thesis.StudentID = (await context.StudentsDBS.Where(s => s.FacultyNumber == "196286").FirstAsync()).Id;
                thesis.TeacherID = (await context.UsersDBS.Where(t => t.UserName == "DBaeva").FirstAsync()).Id;
                theses.Add(thesis);

                thesis = new AssignedThesisModel();
                thesis.ThesisID = (await context.ThesisDBS.Where(d => d.Title.Contains("Интелигентна система за интерактивно извеждане и управление на съдържание")).FirstAsync()).ThesisID;
                thesis.StudentID = (await context.StudentsDBS.Where(s => s.FacultyNumber == "196262").FirstAsync()).Id;
                thesis.TeacherID = (await context.UsersDBS.Where(t => t.UserName == "GAtanasova").FirstAsync()).Id;
                theses.Add(thesis);

                thesis = new AssignedThesisModel();
                thesis.ThesisID = (await context.ThesisDBS.Where(d => d.Title.Contains("Киоск-терминал")).FirstAsync()).ThesisID;
                thesis.StudentID = (await context.StudentsDBS.Where(s => s.FacultyNumber == "196250").FirstAsync()).Id;
                thesis.TeacherID = (await context.UsersDBS.Where(t => t.UserName == "GAtanasova").FirstAsync()).Id;
                theses.Add(thesis);

                thesis = new AssignedThesisModel();
                thesis.ThesisID = (await context.ThesisDBS.Where(d => d.Title.Contains("Уеб базирано приложение за чат в реално време")).FirstAsync()).ThesisID;
                thesis.StudentID = (await context.StudentsDBS.Where(s => s.FacultyNumber == "206215").FirstAsync()).Id;
                thesis.TeacherID = (await context.UsersDBS.Where(t => t.UserName == "GAtanasova").FirstAsync()).Id;
                theses.Add(thesis);

                thesis = new AssignedThesisModel();
                thesis.ThesisID = (await context.ThesisDBS.Where(d => d.Title.Contains("софтуерна система за графично представяне и анализ на данни от измервания на геометрични")).FirstAsync()).ThesisID;
                thesis.StudentID = (await context.StudentsDBS.Where(s => s.FacultyNumber == "196255").FirstAsync()).Id;
                thesis.TeacherID = (await context.UsersDBS.Where(t => t.UserName == "EMinev").FirstAsync()).Id;
                theses.Add(thesis);
                
                thesis = new AssignedThesisModel();
                thesis.ThesisID = (await context.ThesisDBS.Where(d => d.Title.Contains("приложение за следене на фитнес резултати")).FirstAsync()).ThesisID;
                thesis.StudentID = (await context.StudentsDBS.Where(s => s.FacultyNumber == "196265").FirstAsync()).Id;
                thesis.TeacherID = (await context.UsersDBS.Where(t=>t.UserType==MyRolesEnum.Teacher).Where(t => t.UserName == "MDimitrov").FirstAsync()).Id;
                theses.Add(thesis);

                thesis = new AssignedThesisModel();
                thesis.ThesisID = (await context.ThesisDBS.Where(d => d.Title.Contains("система за контрол при провеждане на онлайн практически занятия")).FirstAsync()).ThesisID;
                thesis.StudentID = (await context.StudentsDBS.Where(s => s.FacultyNumber == "196269").FirstAsync()).Id;
                thesis.TeacherID = (await context.UsersDBS.Where(t => t.UserName == "RRusev").FirstAsync()).Id;
                theses.Add(thesis);

                thesis = new AssignedThesisModel();
                thesis.ThesisID = (await context.ThesisDBS.Where(d => d.Title.Contains("Studybuddy")).FirstAsync()).ThesisID;
                thesis.StudentID = (await context.StudentsDBS.Where(s => s.FacultyNumber == "196256").FirstAsync()).Id;
                thesis.TeacherID = (await context.UsersDBS.Where(t => t.UserName == "BIvanova").FirstAsync()).Id;
                theses.Add(thesis);

                thesis = new AssignedThesisModel();
                thesis.ThesisID = (await context.ThesisDBS.Where(d => d.Title.Contains("Уеб-базирана система за обслужване на малка фирма")).FirstAsync()).ThesisID;
                thesis.StudentID = (await context.StudentsDBS.Where(s => s.FacultyNumber == "196257").FirstAsync()).Id;
                thesis.TeacherID = (await context.UsersDBS.Where(t => t.UserName == "CVasilev").FirstAsync()).Id;
                theses.Add(thesis);

                thesis = new AssignedThesisModel();
                thesis.ThesisID = (await context.ThesisDBS.Where(d => d.Title.Contains("генериране на 3D терен")).FirstAsync()).ThesisID;
                thesis.StudentID = (await context.StudentsDBS.Where(s => s.FacultyNumber == "196201").FirstAsync()).Id;
                thesis.TeacherID = (await context.UsersDBS.Where(t => t.UserName == "VVelikov").FirstAsync()).Id;
                theses.Add(thesis);

                thesis = new AssignedThesisModel();
                thesis.ThesisID = (await context.ThesisDBS.Where(d => d.Title.Contains("Електронна библиотека")).FirstAsync()).ThesisID;
                thesis.StudentID = (await context.StudentsDBS.Where(s => s.FacultyNumber == "196268").FirstAsync()).Id;
                thesis.TeacherID = (await context.UsersDBS.Where(t => t.UserName == "VVoinohovska").FirstAsync()).Id;
                theses.Add(thesis);

                thesis = new AssignedThesisModel();
                thesis.ThesisID = (await context.ThesisDBS.Where(d => d.Title.Contains("електропроизводството от фотоволтаична")).FirstAsync()).ThesisID;
                thesis.StudentID = null;
                thesis.TeacherID = (await context.UsersDBS.Where(t => t.UserName == "KGabrovska").FirstAsync()).Id;
                theses.Add(thesis);

                thesis = new AssignedThesisModel();
                thesis.ThesisID = (await context.ThesisDBS.Where(d => d.Title.Contains("изкуствен интелект чрез облачните услуги на Azure")).FirstAsync()).ThesisID;
                thesis.StudentID = null;
                thesis.TeacherID = (await context.UsersDBS.Where(t => t.UserName == "VKozov").FirstAsync()).Id;
                theses.Add(thesis);

                thesis = new AssignedThesisModel();
                thesis.ThesisID = (await context.ThesisDBS.Where(d => d.Title.Contains("Уеб-базирана система за нуждите на спортен клуб")).FirstAsync()).ThesisID;
                thesis.StudentID = (await context.StudentsDBS.Where(s => s.FacultyNumber == "196272").FirstAsync()).Id;
                thesis.TeacherID = (await context.UsersDBS.Where(t => t.UserName == "KShoylekova").FirstAsync()).Id;
                theses.Add(thesis);

                thesis = new AssignedThesisModel();
                thesis.ThesisID = (await context.ThesisDBS.Where(d => d.Title.Contains("Web базирана система БРИКС")).FirstAsync()).ThesisID;
                thesis.StudentID = (await context.StudentsDBS.Where(s => s.FacultyNumber == "196288").FirstAsync()).Id;
                thesis.TeacherID = (await context.UsersDBS.Where(t => t.UserName == "STsankov").FirstAsync()).Id;
                theses.Add(thesis);


                context.AssignedThesesDBS.AddRange(theses);
                
            }

            context.SaveChanges();
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