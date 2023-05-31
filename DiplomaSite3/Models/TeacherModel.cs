
using System.ComponentModel.DataAnnotations;

namespace DiplomaSite3.Models
{
    public class TeacherModel:UserModel
    {
        [Required]
        public bool? Approved { get; set; } = false;

        public ICollection<DiplomaModel>? PostedDiplomas { get; set; }
    }
}
