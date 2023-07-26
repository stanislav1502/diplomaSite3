using DiplomaSite3.Models;

namespace DiplomaSite3.Models
{
    public class AdminVM
    {
        public List<UserModel>? UsersList { get; set; }
        public List<AssignedThesisModel>? ThesisList { get; set;}
        public string? SearchString { get; set; }
    }
}
