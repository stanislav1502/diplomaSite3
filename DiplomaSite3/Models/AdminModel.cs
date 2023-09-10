
using System.ComponentModel.DataAnnotations;

namespace DiplomaSite3.Models
{
    public class AdminModel : UserModel 
    {
        [Required]
        public bool Verified { get; set; } = false;

    }
}
