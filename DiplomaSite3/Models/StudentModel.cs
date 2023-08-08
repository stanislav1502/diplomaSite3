﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomaSite3.Models
{
    public class StudentModel : UserModel
    {
        [Required]
        [StringLength(6)]
        public string FacultyNumber { get; set; }

        //public Guid? AssignedDiplomaID { get; set; }
        public AssignedThesisModel? AssignedThesis { get; set; }

        public ICollection<RequestedThesisModel> RequestedTheses { get; set;}
    }
}
