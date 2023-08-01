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
    public class ThesisController : Controller
    {
        private readonly DiplomaSite3Context _context;

        public ThesisController(DiplomaSite3Context context)
        {
            _context = context;
        }

        // GET: Diplomas
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index(string searchString, bool onlyposted)
        {
            var theses = _context.AssignedThesesDBS;
            if (theses == null)
            {
                return Problem("Entity set 'DiplomaSite3Context.Thesis'  is null.");  
            }

            var thesesQuerry = from d in theses
                         select d;

            if (!string.IsNullOrEmpty(searchString))
            {
                thesesQuerry = thesesQuerry.Where(d => d.Thesis.Title.Contains(searchString));
            }

            if(onlyposted)
            {
                thesesQuerry = thesesQuerry.Where(d => d.Thesis.Status.Equals(StatusEnum.Posted));
            }

            var viewModel = new AdminThesisVM();
            viewModel.ThesesList = await thesesQuerry.ToListAsync();

            foreach (var item in viewModel.ThesesList)
            {
                var teacherData = _context.TeachersDBS.AsNoTracking().ToList() ;
                item.Teacher = item.TeacherID is not null ? teacherData.Find(t => t.Id == item.TeacherID) : null;
                var studentData = _context.StudentsDBS.AsNoTracking().ToList();
                item.Student = item.StudentID is not null ? studentData.Find(t => t.Id == item.StudentID) : null;
                var thesisData = _context.ThesisDBS.AsNoTracking().ToList();
                item.Thesis = item.ThesisID != Guid.Empty ? thesisData.Find(t => t.ThesisID == item.ThesisID) : null;

            }

            return View(viewModel);

        }

        // GET: Diplomas/Details/0000000000000000000000000000000000000
        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.ThesisDBS == null)
            {
                return NotFound();
            }
            var viewModel = new AdminThesisVM();

            var diplomaModel = await _context.ThesisDBS
                .FirstOrDefaultAsync(m => m.ThesisID == id);
            if (diplomaModel == null)
            {
                return NotFound();
            }
            viewModel.ThesisModel.Thesis = diplomaModel;

            return View(viewModel);
        }


		[HttpGet]
		[Authorize(Roles = "Student")]
        public IActionResult MyThesis()
		{
			var stringID = User.Claims.First().Value;
			if (stringID == null || _context.StudentsDBS == null)
			{
				return NotFound();
			}
			Guid userID = new Guid(stringID);

            var thesisQuerry = _context.AssignedThesesDBS.FromSqlRaw("SELECT * FROM AssignedTheses WHERE StudentID = {0}", userID).AsNoTracking();

            var thesisModel = new AssignedThesisModel();

            if ( ! thesisQuerry.Any() )
            {
                return NotFound();
            }
            else thesisModel = thesisQuerry.First();


            return View(thesisModel);
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
        public async Task<IActionResult> Create(ThesisModel thesisModel)
        {
            if (ModelState.IsValid)
            {
                thesisModel.ThesisID = Guid.NewGuid();
                _context.Add(thesisModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Details),nameof(ThesisController),thesisModel.ThesisID);
        }

        // GET: Diplomas/Edit/5
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            var viewModel = new AdminThesisVM();

            if (id == null || _context.ThesisDBS == null)
            {
                return NotFound();
            }

            var thesisModel = await _context.ThesisDBS.FindAsync(id);
            if (thesisModel == null)
            {
                return NotFound();
            }

            viewModel.ThesisModel.Thesis = thesisModel;

            return View(viewModel);
        }

        // POST: Diplomas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Teacher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ThesisID,Title,Description,DefendDate,Grade,Tags,Status,TeacherID,StudentID")] ThesisModel thesisModel)
        {
            if (id != thesisModel.ThesisID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thesisModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiplomaModelExists(thesisModel.ThesisID))
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
            return View(thesisModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        // GET: Diplomas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.ThesisDBS == null)
            {
                return NotFound();
            }

            var diplomaModel = await _context.ThesisDBS
                .FirstOrDefaultAsync(m => m.ThesisID == id);
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
            if (_context.ThesisDBS == null)
            {
                return Problem("Entity set 'DiplomaSite3Context.Thesis'  is null.");
            }
            var thesisModel = await _context.ThesisDBS.FindAsync(id);
            if (thesisModel != null)
            {
                _context.ThesisDBS.Remove(thesisModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Student")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestDiploma(string thesis, string student)
        {
            
            var stud = new Guid(student);
            if (_context.ThesisDBS == null)
            {
                return Problem("Entity set 'DiplomaSite3Context.Thesis'  is null.");
            }
            var thesisModel = await _context.ThesisDBS.FromSqlRaw("SELECT * FROM Thesis WHERE ThesisID = {0}", thesis).FirstAsync();

            var assignedThesis = new AssignedThesisModel();
            assignedThesis.Thesis = thesisModel;

            if (ModelState.IsValid && thesisModel.Status == StatusEnum.Posted)
            {
                try
                {
                    assignedThesis.Student = await _context.StudentsDBS.FindAsync(stud);
                    _context.AssignedThesesDBS.Add(assignedThesis);

                    thesisModel.Status = StatusEnum.WIP;
                    thesisModel.Assigned = assignedThesis;

                    _context.Update(thesisModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiplomaModelExists(thesisModel.ThesisID))
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

            return RedirectToAction(nameof(Index),nameof(ThesisController));
        }

        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Student")]
        [HttpPost]
        public async Task<IActionResult> MarkDone(IFormCollection collection)
        {
            string diploma = collection["ThesisID"];
            Guid id = new Guid(diploma);
            if (_context.ThesisDBS == null)
            {
                return Problem("Entity set 'DiplomaSite3Context.Thesis'  is null.");
            }

            var thesisModel = await _context.AssignedThesesDBS.FindAsync(id);
            if (thesisModel != null)
            {
                if (User.Claims.FirstOrDefault().Value.Equals(thesisModel.Student.Id.ToString()))
                {
                    thesisModel.Thesis.Status = StatusEnum.Done;
                    _context.AssignedThesesDBS.Update(thesisModel);
                    _context.ThesisDBS.Update(thesisModel.Thesis);
                } 
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("MyThesis");
        }

        private bool DiplomaModelExists(Guid id)
        {
          return (_context.ThesisDBS?.Any(e => e.ThesisID == id)).GetValueOrDefault();
        }

        private void PopulateTeachersDropDownList(object selectedTeacher = null)
        {
            var teachersQuery = from t in _context.TeachersDBS
                                   orderby t.FullName
                                   select t;
            ViewBag.TeacherList = new SelectList(teachersQuery.AsNoTracking(), "Id", "FullName", selectedTeacher);
        }

        private void PopulateProgrammesDropDownList(object selectedDegree = null)
        {
            var degreesQuerry = from t in _context.DegreesDBS
                                select t;
            ViewBag.DegreeList = new SelectList(degreesQuerry.AsNoTracking(), "Id", "DegreeName", selectedDegree);
        }
    }
}
