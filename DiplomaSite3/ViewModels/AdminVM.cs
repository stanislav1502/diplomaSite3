using DiplomaSite3.Models;

namespace DiplomaSite3.Models
{
    public class AdminVM
    {
        public List<UserModel>? Users { get; set; }
        public List<DiplomaModel>? Diplomas { get; set;}
        public string? SearchString { get; set; }
    }
}
