using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomaSite3.Models
{
    public class RequestedThesisModel
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Guid ThesisID { get; set; }
        public ICollection< ThesisModel > Thesis { get; set; }

        [Required]
        public Guid StudentID { get; set; }
        public ICollection<StudentModel> Students { get; set; }
    }
}
