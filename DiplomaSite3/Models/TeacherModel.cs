
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomaSite3.Models
{
    public class TeacherModel:UserModel
    {
        [Required]
        public bool? Verified { get; set; } = false;

        public ICollection<AssignedThesisModel>? PostedTheses { get; set; }
    }
}
