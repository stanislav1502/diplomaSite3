
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DiplomaSite3.Models
{
    public class AdminModel : UserModel 
    {
        [Required]
        [PasswordPropertyText]
        [StringLength(50, MinimumLength = 10)]
        public string? AdminPass { get; set; }

    }
}
