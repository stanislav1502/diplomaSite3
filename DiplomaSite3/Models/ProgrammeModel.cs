using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomaSite3.Models
{
    public class ProgrammeModel
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string ProgrammeName { get; set; }

        public DepartmentModel ?Department { get; set; }
        public int ?DepartmentId { get; set; }

        public ICollection<DegreeModel> ?Degrees { get; set; }

        public override string ToString()
        {
            var str = "";
            str += Department!.ToString() +" "+ ProgrammeName;
            return str;
        }
    }
}
