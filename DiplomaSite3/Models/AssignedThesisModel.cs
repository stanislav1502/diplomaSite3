using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DiplomaSite3.Models
{
    public class AssignedThesisModel
    {
        [Key]
        [Required]
        public Guid ThesisID { get; set; }
        public ThesisModel Thesis { get; set; }

        // 1 diploma - by 1 assigner
        [Display(Name = "Teacher")]
        [DisplayFormat(NullDisplayText = "Missing teacher")]
        public Guid? TeacherID { get; set; }
        public TeacherModel? Teacher { get; set; }

        // 1 diploma - to 1 assignee
        [Display(Name = "Student")]
        [DisplayFormat(NullDisplayText = "No assigned student")]
        public Guid? StudentID { get; set; }
        public StudentModel? Student { get; set; }
    }
}
