
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomaSite3.Models
{
    public class TeacherModel:UserModel
    {
        public bool? Approved { get; set; } = false;

        public ICollection<DiplomaModel>? PostedDiplomas { get; set; }
    }
}
