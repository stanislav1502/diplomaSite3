using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomaSite3.Models
{
    public class DepartmentModel
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string DepartmentName { get; set; }

        public FacultyModel? Faculty { get; set; }
        public int? FacultyId { get; set; }

        public ICollection<ProgrammeModel>? Programmes { get; set; }


    }
}
