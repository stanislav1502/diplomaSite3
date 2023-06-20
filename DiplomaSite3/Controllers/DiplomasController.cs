#nullable disable

using DiplomaSite3.Data;
using DiplomaSite3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using System.Collections.Generic;

namespace DiplomaSite3.Controllers
{
    [Authorize]
    public class DiplomasController : Controller
    {
        private readonly DiplomaSite3Context _context;

        public DiplomasController(DiplomaSite3Context context)
        {
            _context = context;
        }

        // GET: Diplomas
        [HttpGet]
        [AllowAnonymous]
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

            var viewModel = new AdminDiplomaVM
            {
                Diplomas = await diplomasQuerry.ToListAsync()
            };

            foreach (var diploma in viewModel.Diplomas)
            {
                var teacher = _context.TeachersDBS.FromSqlRaw("SELECT * FROM Users WHERE Id = {0}", diploma.TeacherID).AsNoTracking();
                diploma.Teacher = teacher.Any() ? teacher.First() : null;
                var student = _context.StudentsDBS.FromSqlRaw("SELECT * FROM Users WHERE Id = {0}", diploma.StudentID).AsNoTracking();
                diploma.Student = student.Any() ? student.First() : null;
            }

            return View(viewModel);

        }

        // GET: Diplomas/Details/0000000000000000000000000000000000000
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.DiplomasDBS == null)
            {
                return NotFound();
            }

            var diplomaModel = await _context.DiplomasDBS
                .FirstOrDefaultAsync(m => m.DiplomaID == id);
            if (diplomaModel == null)
            {
                return NotFound();
            }

            return View(diplomaModel);
        }

        
        [HttpGet]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> MyDiploma()
        {
           
            var stringID = User.Claims.First().Value; 
            if (stringID == null || _context.StudentsDBS == null)
            {
                return NotFound();
            }
            Guid userID = new Guid(stringID);

            var diplomaModel = _context.DiplomasDBS.FromSqlRaw("SELECT * FROM Diploma WHERE StudentID = {0}", userID).AsNoTracking().First();
            if (diplomaModel == null)
            {
                return NotFound();
            }

            return View(diplomaModel);
        }

        // GET: Diplomas/Create
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            PopulateTeachersDropDownList();
            return View();
        }

        // POST: Diplomas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiplomaID,Title,Description,DefendDate,Grade,Tags,Status,TeacherID,StudentID")] DiplomaModel diplomaModel)
        {
            if (ModelState.IsValid)
            {
                diplomaModel.DiplomaID = Guid.NewGuid();
                _context.Add(diplomaModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(diplomaModel);
        }

        // GET: Diplomas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.DiplomasDBS == null)
            {
                return NotFound();
            }

            var diplomaModel = await _context.DiplomasDBS.FindAsync(id);
            if (diplomaModel == null)
            {
                return NotFound();
            }
            return View(diplomaModel);
        }

        // POST: Diplomas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DiplomaID,Title,Description,DefendDate,Grade,Tags,Status,TeacherID,StudentID")] DiplomaModel diplomaModel)
        {
            if (id != diplomaModel.DiplomaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diplomaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiplomaModelExists(diplomaModel.DiplomaID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(diplomaModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        // GET: Diplomas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.DiplomasDBS == null)
            {
                return NotFound();
            }

            var diplomaModel = await _context.DiplomasDBS
                .FirstOrDefaultAsync(m => m.DiplomaID == id);
            if (diplomaModel == null)
            {
                return NotFound();
            }

            return View(diplomaModel);
        }

        // POST: Diplomas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.DiplomasDBS == null)
            {
                return Problem("Entity set 'DiplomaSite3Context.Diplomas'  is null.");
            }
            var diplomaModel = await _context.DiplomasDBS.FindAsync(id);
            if (diplomaModel != null)
            {
                _context.DiplomasDBS.Remove(diplomaModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiplomaModelExists(Guid id)
        {
          return (_context.DiplomasDBS?.Any(e => e.DiplomaID == id)).GetValueOrDefault();
        }

        private void PopulateTeachersDropDownList(object selectedTeacher = null)
        {
            var teachersQuery = from t in _context.TeachersDBS
                                   orderby t.FullName
                                   select t;
            ViewBag.DepartmentID = new SelectList(teachersQuery.AsNoTracking(), "UserID", "FirstName", selectedTeacher);
        }
    }
}
