using DiplomaSite2.Models;
using System.ComponentModel.DataAnnotations;

namespace DiplomaSite3.Models
{
    public class StudentModel : UserModel
    {
        [Required]
        public string FN { get; set; }

        [DisplayFormat(NullDisplayText = "No assigned diploma")]
        public DiplomaModel? AssignedDiploma { get; set; }
    }
}
