using DiplomaSite2.Models;

namespace DiplomaSite3.Models
{
    public class TeacherModel:UserModel
    {
        public bool Approved { get; set; }

        public ICollection<DiplomaModel>? PostedDiplomas { get; set; }
    }
}
