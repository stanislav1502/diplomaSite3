
using System.ComponentModel.DataAnnotations;

namespace DiplomaSite3.Models
{
    public class AdminModel : UserModel
    {
        [Required]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 10)]
        public string AdminPassword { get; set; }

    }
}
