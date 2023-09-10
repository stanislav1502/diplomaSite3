using DiplomaSite3.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DiplomaSite3.Models
{
    public class TeacherThesesVM
    {
        public List<AssignedThesisModel>? ThesisList { get; set; }

        public string? SearchString { get; set; }
        public StatusEnum? SearchStatus { get; set; }

    }
}
