
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DiplomaSite3.Enums;

namespace DiplomaSite3.Models
{

    public class ThesisModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ThesisID { get; set; }

        [Required]
        [StringLength(200)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Title { get; set; }

        [Required]
        [StringLength(2000)]
        public string Description { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


        [Display(Name = "Assigned on")]
        [DataType(DataType.Date)]
        [DisplayFormat(NullDisplayText = "---")]
        public DateTime? AssignDate { get; set; }

        [Display(Name = "Defense on")]
        [DataType(DataType.Date)]
        [DisplayFormat(NullDisplayText = "---")]
        public DateTime? DefendDate { get; set; }

        [Precision(5, 3)]
        [DisplayFormat(NullDisplayText = "---")]
        public decimal? Grade { get; set; }

       [Required]
        public StatusEnum Status { get; set; }

        [Required]
        [Display(Name = "Degree")]
        public int DegreeId { get; set; } = 0;
        public DegreeModel Degree { get; set; }
        
        public AssignedThesisModel? Assigned { get; set; }

        public ICollection<RequestedThesesModel>? StudentRequests { get; set; }
    }
}
