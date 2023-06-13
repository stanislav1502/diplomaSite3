
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DiplomaSite3.Enums;

namespace DiplomaSite3.Models
{
    public class AdminDiplomaVM
    {
        [Key]
        public Guid DiplomaID { get; }

        [Required]
        [StringLength(100)]
        public string Title { get;  }

        [Required]
        [StringLength(200)]
        public string Description { get; }

        [Display(Name = "Defense")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", NullDisplayText = "No defense ")]
        public DateTime? DefendDate { get; }

        [Precision(5, 3)]
        [DisplayFormat(NullDisplayText = "No grade")]
        public decimal? Grade { get; }

        [StringLength(50)]
        [DisplayFormat(NullDisplayText = "No tags")]
        public string? Tags { get; }

        [Required]
        public StatusEnum Status { get; }

        // 1 diploma - by 1 assigner
        [Display(Name = "Teacher")]
        [DisplayFormat(NullDisplayText = "Missing teacher")]
        public string? Teacher { get; }
        
        // 1 diploma - to 1 assignee
        [Display(Name = "Student")]
        [DisplayFormat(NullDisplayText = "Not assigned")]
        public string? Student { get; }

        public AdminDiplomaVM(DiplomaModel dm,string teacher,string student)
        {
            DiplomaID = dm.DiplomaID;
            Title = dm.Title;
            Description = dm.Description;
            DefendDate = dm.DefendDate;
            Grade = dm.Grade;
            Tags = dm.Tags;
            Status = dm.Status;
            Teacher = teacher;
            Student = student;
        }
    }
}
