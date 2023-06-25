
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DiplomaSite3.Enums;

namespace DiplomaSite3.Models
{
    public class AdminDiplomaVM
    {
        public List<DiplomaModel>? Diplomas { get; set;}
        public DiplomaModel? DiplomaModel { get; set;}

        public string? SearchString { get; set;}

        public bool OnlyPosted { get; set;} = false;
    }
}
