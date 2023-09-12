// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using DiplomaSite3.Enums;
using DiplomaSite3.Models;
using DiplomaSite3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;

namespace DiplomaSite3.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<UserModel> _signInManager;
        private readonly UserManager<UserModel> _userManager;
        private readonly IUserStore<UserModel> _userStore;
        private readonly IUserEmailStore<UserModel> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailService _emailSender;

        public RegisterModel(
            UserManager<UserModel> userManager,
            IUserStore<UserModel> userStore,
            SignInManager<UserModel> signInManager,
            ILogger<RegisterModel> logger,
            IEmailService emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }
        
        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [StringLength(100, ErrorMessage ="Username must be at least {2} characters long.",MinimumLength = 4)]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(20, MinimumLength = 1)]
            public string FirstName { get; set; }

            [Required]
            [StringLength(20, MinimumLength = 1)]
            public string LastName { get; set; }

            public string? FacultyNumber { get; set; } = null;

            [Required]
            [Display(Name = "Acount Type")]
            public MyRolesEnum UserType { get; set; } = MyRolesEnum.Student;

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

 }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Username, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                user.FirstName = Input.FirstName;
                user.LastName = Input.LastName;
                user.UserType = Input.UserType;
                if (Input.FacultyNumber == null)
                    throw new InvalidOperationException($"Can't create an instance of '{nameof(StudentModel)} - empty Faculty number'. ");

                var result = await _userManager.CreateAsync(user, Input.Password);


                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    await _userManager.AddToRoleAsync(user, user.UserType.ToString());

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    MailData mailData = new MailData() {
                        EmailToId = _userManager.GetEmailAsync(user).Result.ToString(),
                        EmailToName = _userManager.GetUserNameAsync(user).Result.ToString(),
                        EmailSubject = "Confirm your email",
                        EmailBody = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>."
                    };

                    await _emailSender.SendMailAsync(mailData, new CancellationToken());

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private UserModel CreateUser()
        {
            try
            {
                switch (Input.UserType)
                {
                    case MyRolesEnum.Student:
                    var stresult = Activator.CreateInstance<StudentModel>();
                       
                        stresult.FacultyNumber = Input.FacultyNumber;
                        return stresult;
                    case MyRolesEnum.Teacher:
                        var tresult = Activator.CreateInstance<TeacherModel>();
                        tresult.Verified = false;
                        return tresult;
                    //case MyRolesEnum.Admin: return Activator.CreateInstance<AdminModel>();
                    default: return null;
                }
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(UserModel)}'. " +
                    $"Ensure that '{nameof(UserModel)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<UserModel> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<UserModel>) _userStore;
        }
    }
}
