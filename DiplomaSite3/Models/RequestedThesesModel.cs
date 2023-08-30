using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomaSite3.Models
{
    public class RequestedThesesModel
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestId { get; set; }

        [Required]
        public Guid ThesisID { get; set; }
        public ThesisModel Thesis { get; set; }

        [Required]
        public Guid StudentID { get; set; }
        public StudentModel Student { get; set; }
    }
}
