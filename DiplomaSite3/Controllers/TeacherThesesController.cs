
using DiplomaSite3.Data;
using DiplomaSite3.Enums;
using DiplomaSite3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DiplomaSite3.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class TeacherThesesController : Controller
    {
        private readonly DiplomaSite3Context _context;

        public TeacherThesesController(DiplomaSite3Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? searchString, string? searchStatus)
        {
            var theses = _context.AssignedThesesDBS;
            if (theses == null)
            {
                return Problem("Entity set 'DiplomaSite3Context.Thesis'  is null.");
            }

            var thesisQuerry = from t in theses
                                 select t ;

            if (User != null)
            {
                var userID = new Guid(User.Claims.First().Value);
                thesisQuerry = thesisQuerry.Where(d => d.TeacherID.Equals(userID));
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                thesisQuerry = thesisQuerry.Where(d => d.Thesis.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(searchStatus))
            {
                if (!searchStatus.Equals("Any"))

                {
                    var status = Enum.Parse<StatusEnum>(searchStatus, true);
                    thesisQuerry = thesisQuerry.Where(d => d.Thesis.Status.Equals(status));

                }
            }

            var viewModel = new TeacherThesesVM
            {
                ThesisList = await thesisQuerry.OrderByDescending(d =>d.Thesis.Title).ToListAsync()
            };

            foreach (var thesis in viewModel.ThesisList)
            {
                var student = _context.StudentsDBS.FromSqlRaw("SELECT * FROM Students WHERE Id = {0}", thesis.StudentID!).AsNoTracking();
                thesis.Student = student.Any() ? student.First() : null;
            }
            

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AssignedThesisModel model)
        {

            if (ModelState.IsValid)
            {
                model.Thesis.ThesisID = Guid.NewGuid();

                _context.ThesisDBS.Add(model.Thesis); 
                
                var userID = new Guid(User.Claims.First().Value);
                var teacher = _context.TeachersDBS.Where(d => d.Id.Equals(userID)).First();
                model.Teacher = teacher;

                _context.AssignedThesesDBS.Add(model);
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

    }
}
