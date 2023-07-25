#nullable disable

using DiplomaSite3.Data;
using DiplomaSite3.Enums;
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
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index(string searchString, bool onlyposted)
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
                diplomasQuerry = diplomasQuerry.Where(d => d.Title!.Contains(searchString));
            }

            if(onlyposted)
            {
                diplomasQuerry = diplomasQuerry.Where(d => d.Status!.Equals(StatusEnum.Posted));
            }

            var viewModel = new AdminDiplomaVM
            {
                Diplomas = await diplomasQuerry.OrderByDescending(d => d.Title).ToListAsync()
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
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.DiplomasDBS == null)
            {
                return NotFound();
            }
            var viewModel = new AdminDiplomaVM();

            var diplomaModel = await _context.DiplomasDBS
                .FirstOrDefaultAsync(m => m.DiplomaID == id);
            if (diplomaModel == null)
            {
                return NotFound();
            }
            viewModel.DiplomaModel = diplomaModel;

            return View(viewModel);
        }


		[HttpGet]
		[Authorize(Roles = "Student")]
        public IActionResult MyDiploma()
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
            PopulateProgrammesDropDownList();
            return View();
        }

        // POST: Diplomas/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DiplomaModel diplomaModel)
        {
            if (ModelState.IsValid)
            {
                diplomaModel.DiplomaID = Guid.NewGuid();
                _context.Add(diplomaModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Details),nameof(DiplomasController),diplomaModel.DiplomaID);
        }

        // GET: Diplomas/Edit/5
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            var viewModel = new AdminDiplomaVM();

            if (id == null || _context.DiplomasDBS == null)
            {
                return NotFound();
            }

            var diplomaModel = await _context.DiplomasDBS.FindAsync(id);
            if (diplomaModel == null)
            {
                return NotFound();
            }

            viewModel.DiplomaModel = diplomaModel;

            return View(viewModel);
        }

        // POST: Diplomas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Teacher")]
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
        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Student")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestDiploma(string diploma, string student)
        {
            
            var stud = new Guid(student);
            if (_context.DiplomasDBS == null)
            {
                return Problem("Entity set 'DiplomaSite3Context.Diplomas'  is null.");
            }
            var diplomaModel = await _context.DiplomasDBS.FromSqlRaw("SELECT * FROM Diploma WHERE DiplomaID = {0}", diploma).FirstAsync();

            if (ModelState.IsValid && diplomaModel.Status == StatusEnum.Posted)
            {
                try
                {

                    diplomaModel.Student = (StudentModel)(await _context.UsersDBS.FindAsync(stud));
                    diplomaModel.StudentID = diplomaModel.Student.Id;
                    diplomaModel.Status = StatusEnum.WIP;

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

            return RedirectToAction(nameof(Index),nameof(DiplomasController));
        }

        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Student")]
        [HttpPost]
        public async Task<IActionResult> MarkDone(IFormCollection collection)
        {
            string diploma = collection["diplomaid"];
            Guid id = new Guid(diploma);
            if (_context.DiplomasDBS == null)
            {
                return Problem("Entity set 'DiplomaSite3Context.Diplomas'  is null.");
            }
            var diplomaModel = await _context.DiplomasDBS.FindAsync(id);
            if (diplomaModel != null)
            {
                if (User.Claims.FirstOrDefault().Value.Equals(diplomaModel.StudentID.ToString()))
                {
                    diplomaModel.Status = StatusEnum.Done;
                    _context.DiplomasDBS.Update(diplomaModel);
                } 
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("MyDiploma");
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
            ViewBag.TeacherList = new SelectList(teachersQuery.AsNoTracking(), "UserID", "FullName", selectedTeacher);
        }

        private void PopulateProgrammesDropDownList(object selectedDegree = null)
        {
            var degreesQuerry = from t in _context.DegreesDBS
                                select t;
            ViewBag.DegreeList = new SelectList(degreesQuerry.AsNoTracking(), "Id", "DegreeName", selectedDegree);
        }
    }
}
