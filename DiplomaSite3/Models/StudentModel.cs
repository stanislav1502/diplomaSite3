
using System.ComponentModel.DataAnnotations;

namespace DiplomaSite3.Models
{
    public class StudentModel : UserModel
    {
        [Required]
        [StringLength(6)]
        public string? FacultyNumber { get; set; }

        [DisplayFormat(NullDisplayText = "No assigned diploma")]
        public DiplomaModel? AssignedDiploma { get; set; }
    }
}
