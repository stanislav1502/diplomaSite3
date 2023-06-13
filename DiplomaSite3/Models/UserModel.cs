
#nullable disable

using DiplomaSite3.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomaSite3.Models
{
    
    public abstract class UserModel : IdentityUser<Guid>
    {

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string LastName { get; set; }

        [Required]
        public MyRolesEnum UserType { get; set; }

        public byte[] PasswordSalt { get; set; }

        [NotMapped]
        public string FullName { get { return $"{FirstName} {LastName}"; } }


    }
}
