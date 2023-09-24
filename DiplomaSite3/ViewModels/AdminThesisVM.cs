
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

        public string? Search { get; set;} = null;
        public StatusEnum? SearchStatus { get; set; }

        public bool OnlyPosted { get; set;} = false;

        public bool? hasThesis { get; set;} = null;

        

    }
}
