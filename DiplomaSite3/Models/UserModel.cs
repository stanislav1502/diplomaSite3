
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomaSite3.Models
{
    public enum UserType
    {
        Student, Teacher, Admin
    }
    public abstract class UserModel : IdentityUser<Guid>
    {

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string LastName { get; set; }

        [Required]
        public UserType UserType { get; set; }

        public byte[] PasswordSalt { get; set; }

        [NotMapped]
        public string FullName { get { return $"{FirstName} {LastName}"; } }


    }
}
