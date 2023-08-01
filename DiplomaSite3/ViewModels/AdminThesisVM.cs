
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DiplomaSite3.Enums;
using System.ComponentModel;

namespace DiplomaSite3.Models
{
    public class AdminThesisVM
    {

        public List<AssignedThesisModel>? ThesesList { get; set;} = new List<AssignedThesisModel>();
        
        public AssignedThesisModel? ThesisModel { get; set;} = new AssignedThesisModel();
        
        public string? SearchString { get; set;} = null;

        public bool OnlyPosted { get; set;} = false;
    }
}
