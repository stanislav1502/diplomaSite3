
using System.ComponentModel.DataAnnotations;

namespace DiplomaSite3.Models
{

    public enum StatusEnum { Posted, WIP, Done, InAppraisal, Archived }

//    [Table("Diploma")]
    public class DiplomaModel
    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DiplomaID { get; set; }

//      [Required]
        public string Title { get; set; }

//        [Required]
        public string Description { get; set; }

        [DataType(DataType.Date)]
//        [DisplayFormat(NullDisplayText = "No defense ")]
        public DateTime? DefendDate { get; set; }

//        [Precision(5, 3)]
        [DisplayFormat(NullDisplayText = "No grade")]
        public decimal? Grade { get; set; }

//        [DisplayFormat(NullDisplayText = "No tags")]
        public string? Tags { get; set; }

        public StatusEnum Status { get; set; }

        // 1 diploma - by 1 assigner
//        [Required]
        public Guid TeacherID { get; set; }

        public TeacherModel Teacher { get; set; }

        // 1 diploma - to 1 assignee

        public Guid? StudentID { get; set; }

//        [DisplayFormat(NullDisplayText = "No assigned student")]
        public StudentModel? Student { get; set; }
    }
}
