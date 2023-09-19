using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DiplomaSite3.Enums;
using Humanizer;

namespace DiplomaSite3.Models
{
    public class DegreeModel
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public FacultyModel? Faculty { get; set; }
        public int? FacultyId { get; set; }

        public DepartmentModel? Department { get; set; }
        public int? DepartmentId { get; set; }

        public ProgrammeModel? Programme { get; set; }
        public int? ProgrammeId { get; set; }


        public DegreeEnum Degree { get; set; }

        public ICollection<ThesisModel>? Thesis { get; set; }


    }
}
