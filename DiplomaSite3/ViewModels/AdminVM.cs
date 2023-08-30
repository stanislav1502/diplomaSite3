using DiplomaSite3.Models;

namespace DiplomaSite3.Models
{
    public class AdminVM
    {
        public List<UserModel>? AdminsList { get; set; }
        public List<UserModel>? TeachersList { get; set; }
        public List<UserModel>? StudentsList { get; set; }

        public List<AssignedThesisModel>? ThesisList { get; set;}
        public string? SearchString { get; set; }
    }
}
