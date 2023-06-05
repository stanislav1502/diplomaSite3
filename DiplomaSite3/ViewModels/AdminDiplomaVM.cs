
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DiplomaSite3.Models
{

    

    public class AdminDiplomaVM
    {
        [Key]
        public Guid DiplomaID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Display(Name = "Defense")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", NullDisplayText = "No defense ")]
        public DateTime? DefendDate { get; set; }

        [Precision(5, 3)]
        [DisplayFormat(NullDisplayText = "No grade")]
        public decimal? Grade { get; set; }

        [StringLength(50)]
        [DisplayFormat(NullDisplayText = "No tags")]
        public string? Tags { get; set; }

        [Required]
        public StatusEnum Status { get; set; }

        // 1 diploma - by 1 assigner
        [Display(Name = "Teacher")]
        [DisplayFormat(NullDisplayText = "Missing teacher")]
        public Guid? TeacherID { get; set; }
        
        // 1 diploma - to 1 assignee
        [Display(Name = "Student")]
        [DisplayFormat(NullDisplayText = "No assigned student")]
        public Guid? StudentID { get; set; }

        public AdminDiplomaVM(DiplomaModel dm)
        {
            DiplomaID = dm.DiplomaID;
            Title = dm.Title;
            Description = dm.Description;
            DefendDate = dm.DefendDate;
            Grade = dm.Grade;
            Tags = dm.Tags;
            Status = dm.Status;
            TeacherID = dm.TeacherID;
            StudentID = dm.StudentID;
        }
    }
}
