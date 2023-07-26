using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DiplomaSite3.Enums;

namespace DiplomaSite3.Models
{
    public class DegreeModel
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(100)]
        public ProgrammeModel? Programme { get; set; }
        public int? ProgrammeId { get; set; }

        public DegreeEnum Degree { get; set; }


        public ICollection<ThesisModel>? Thesis { get; set; }

        public override string ToString()
        {
            var str = "";
            str += Programme!.ToString();
            return str;
        }

    }
}
