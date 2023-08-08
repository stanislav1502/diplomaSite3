using DiplomaSite3.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DiplomaSite3.Models
{
    public class RequestedThesesVM
    {
        public List<StudentModel>? StudentsList { get; set; }
        public string? SearchString { get; set; }
        public string? SearchStudent { get; set; }
        public Guid? GivenStudentId { get; set; }
        public Guid? GivingTeacherId { get; set; }

        public ThesisModel? RequestedThesis { get; set; }
    }
}
