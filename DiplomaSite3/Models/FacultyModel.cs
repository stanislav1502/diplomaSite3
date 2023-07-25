using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomaSite3.Models
{
    public class FacultyModel
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string FacultyName { get; set; }

        public ICollection<DepartmentModel> ?Departments { get; set; }

        public override string ToString()
        {
            return FacultyName;
        }
    }
}
