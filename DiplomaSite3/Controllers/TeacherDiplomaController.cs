
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
    public class TeacherDiplomaController : Controller
    {
        private readonly DiplomaSite3Context _context;

        public TeacherDiplomaController(DiplomaSite3Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? searchString, string? searchStatus)
        {
            var diplomas = _context.DiplomasDBS;
            if (diplomas == null)
            {
                return Problem("Entity set 'DiplomaSite3Context.Diplomas'  is null.");
            }

            var diplomasQuerry = from d in diplomas
                                 select d ;

            if (User != null)
            {
                var userID = new Guid(User.Claims.First().Value);
                diplomasQuerry = diplomasQuerry.Where(d => d.TeacherID!.Equals(userID));
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                diplomasQuerry = diplomasQuerry.Where(d => d.Title!.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(searchStatus))
            {
                if (!searchStatus.Equals("Any"))

                {
                    var status = Enum.Parse<StatusEnum>(searchStatus, true);
                    diplomasQuerry = diplomasQuerry.Where(d => d.Status!.Equals(status));

                }
            }

            var viewModel = new TeacherDiplomasVM
            {
                Diplomas = await diplomasQuerry.OrderByDescending(d =>d.Title).ToListAsync()
            };

            foreach (var diploma in viewModel.Diplomas)
            {
                var student = _context.StudentsDBS.FromSqlRaw("SELECT * FROM Users WHERE Id = {0}", diploma.StudentID!).AsNoTracking();
                diploma.Student = student.Any() ? student.First() : null;
            }
            

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            StudentTeacherDiplomaVM model = new StudentTeacherDiplomaVM();
            Guid teacherID = Guid.Empty;
            if (User != null)
                teacherID = new Guid(User.Claims.First().Value);
            else return Problem("Teacher is not logged in.");

            if (!teacherID.Equals(Guid.Empty))
#pragma warning disable CS8601 // Possible null reference assignment.
                model.TeacherTC = await _context.TeachersDBS.FindAsync(teacherID);
#pragma warning restore CS8601 // Possible null reference assignment.


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentTeacherDiplomaVM model)
        {

            if (ModelState.IsValid)
            {
                model.DiplomaTC.DiplomaID = Guid.NewGuid();

                _context.DiplomasDBS.Add(model.DiplomaTC);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.DiplomasDBS == null)
            {
                return NotFound();
            }
            var viewModel = new TeacherDiplomasVM();

            var diplomaModel = await _context.DiplomasDBS
                .FirstOrDefaultAsync(m => m.DiplomaID == id);
            if (diplomaModel == null)
            {
                return NotFound();
            }
            viewModel.Diplomas!.Add(diplomaModel);

            return View(viewModel);
        }

    }
}
