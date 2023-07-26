
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DiplomaSite3.Enums;
using System.ComponentModel;

namespace DiplomaSite3.Models
{
    public class AdminThesisVM
    {

        public List<AssignedThesisModel>? ThesesList { get; set;}
        
        public AssignedThesisModel? ThesisModel { get; set;}
        
        public string? SearchString { get; set;}

        public bool OnlyPosted { get; set;} = false;
    }
}
