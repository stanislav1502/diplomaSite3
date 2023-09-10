using DiplomaSite3.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DiplomaSite3.Models
{
    public class DefenseThesisVM
    {
        public AssignedThesisModel? AssignedThesis { get; set; }


    }
}
