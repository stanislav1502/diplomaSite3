using DiplomaSite3.Data;
using DiplomaSite3.Enums;
using DiplomaSite3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DiplomaSite3.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class TeacherDiplomaController : Controller
    {
        private readonly DiplomaSite3Context _context;

        public TeacherDiplomaController(DiplomaSite3Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            var diplomas = _context.DiplomasDBS;
            if (diplomas == null)
            {
                return Problem("Entity set 'DiplomaSite3Context.Diplomas'  is null.");
            }

            var diplomasQuerry = from d in diplomas
                                 select d;

            if (!string.IsNullOrEmpty(searchString))
            {
                diplomasQuerry = diplomasQuerry.Where(s => s.Title!.Contains(searchString));
            }

            if (User != null)
            {
                var userID = new Guid(User.Claims.First().Value);
                diplomasQuerry = diplomasQuerry.Where(d => d.TeacherID!.Equals(userID));
            }

            var viewModel = new TeacherDiplomasVM
            {
                Diplomas = await diplomasQuerry.ToListAsync()
            };

            foreach (var diploma in viewModel.Diplomas)
            {
                var student = _context.StudentsDBS.FromSqlRaw("SELECT * FROM Users WHERE Id = {0}", diploma.StudentID).AsNoTracking();
                diploma.Student = student.Any() ? student.First() : null;
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetStatus(StatusEnum status)
        {
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetDefense()
        {

        }


    }
}
