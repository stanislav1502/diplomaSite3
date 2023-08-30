#nullable disable

using DiplomaSite3.Data;
using DiplomaSite3.Enums;
using DiplomaSite3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        // GET: Thesis
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

            
            // if anonymous show only not assigned theses
            if (!User.Claims.ToList().Any()) onlyposted = true;

            // filter based on form values
            if (!string.IsNullOrEmpty(searchString))
            {
                thesesQuerry = thesesQuerry.Where(d => d.Thesis.Title.Contains(searchString));
            }
            if (onlyposted)
            {
                thesesQuerry = thesesQuerry.Where(d => d.Thesis.Status.Equals(StatusEnum.Posted));
            }

            var viewModel = new AdminThesisVM();
            viewModel.ThesesList = await thesesQuerry.ToListAsync();

            foreach (var item in viewModel.ThesesList)
            {
                LinkAssignedThesisData(item);
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

            var thesisModel = await _context.AssignedThesesDBS
                .FirstOrDefaultAsync(m => m.ThesisID == id);
            if (thesisModel == null)
            {
                return NotFound();
            }
            viewModel.ThesisModel = thesisModel;

            if (thesisModel.ThesisID != Guid.Empty)
                thesisModel.Thesis = _context.ThesisDBS.FindAsync(thesisModel.ThesisID).Result;
            if (thesisModel.StudentID != Guid.Empty)
                thesisModel.Student = _context.StudentsDBS.FindAsync(thesisModel.StudentID).Result;
            if (thesisModel.TeacherID != Guid.Empty)
                thesisModel.Teacher = _context.TeachersDBS.FindAsync(thesisModel.TeacherID).Result;

            // does current user have a thesis
            var stringID = User.Claims.First().Value;
            if (stringID == null || _context.StudentsDBS == null)
            {
                return NotFound();
            }
            Guid userID = new Guid(stringID);
            var studentQuerry = _context.AssignedThesesDBS.FromSqlRaw("SELECT * FROM AssignedTheses WHERE StudentID = {0}", userID).AsNoTracking();

            viewModel.hasThesis = studentQuerry.Any();

            return View(viewModel);
        }


        [HttpGet]
        [Authorize(Roles = "Student")]
        public IActionResult MyThesis()
        {
            // checking if current user has a thesis
            var stringID = User.Claims.First().Value;
            if (stringID == null || _context.StudentsDBS == null)
            {
                return NotFound();
            }
            Guid userID = new Guid(stringID);

            var thesisQuerry = _context.AssignedThesesDBS.FromSqlRaw("SELECT * FROM AssignedTheses WHERE StudentID = {0}", userID).AsNoTracking();

            var thesisModel = new AssignedThesisModel();
            if (!thesisQuerry.Any())
            {
                return NotFound();
            }
            else thesisModel = thesisQuerry.First();

            // linking the view models with data from db
            thesisModel = LinkAssignedThesisData(thesisModel);

            return View(thesisModel);
        }

        // GET: Diplomas/Create
        [HttpGet]
        [Authorize(Roles = "Admin, Teacher")]
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
                _context.ThesisDBS.Add(thesisModel);
                AssignedThesisModel assigned = new AssignedThesisModel {
                    ThesisID = thesisModel.ThesisID,
                    StudentID = null,
                    TeacherID = null,
                };

                assigned = LinkAssignedThesisData(assigned);

                _context.AssignedThesesDBS.Add(assigned);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Details), nameof(ThesisController), thesisModel.ThesisID);
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
                    if (!ThesisModelExists(thesisModel.ThesisID))
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

            var thesisModel = await _context.AssignedThesesDBS
                .FirstOrDefaultAsync(m => m.ThesisID == id);
            if (thesisModel == null)
            {
                return NotFound();
            }

            thesisModel = LinkAssignedThesisData(thesisModel);

            return View(thesisModel);
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
            var assignedModel = await _context.AssignedThesesDBS.FindAsync(id);

            if (thesisModel != null)
                _context.ThesisDBS.Remove(thesisModel);
            if (assignedModel != null)
                _context.AssignedThesesDBS.Remove(assignedModel);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Student")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestThesis(IFormCollection collection)
        {

            if (_context.ThesisDBS is null || _context.StudentsDBS is null || _context.RequestedThesesDBS is null)
                return Problem("Required Entity sets are null.");

            // check Post has data
            string thesisID = collection["ThesisID"];
            string studentID = collection["StudentID"];
            if (thesisID.IsNullOrEmpty() || studentID.IsNullOrEmpty())
                return Problem("Invalid request form.");

            var student = await _context.StudentsDBS.FindAsync(new Guid(studentID));
            var thesis = await _context.ThesisDBS.FindAsync(new Guid(thesisID));
            var requestedThesesDB = _context.RequestedThesesDBS;

            // check student and thesis exist
            if (thesis is null || student is null || requestedThesesDB is null)
                return Problem("Nonexistent entities.");


            if (ModelState.IsValid && thesis.Status == StatusEnum.Posted)
            {
                RequestedThesesModel requested = new RequestedThesesModel
                {
                    ThesisID = thesis.ThesisID,
                    StudentID = student.Id,
                };

                requestedThesesDB.Add(requested);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), nameof(ThesisController));
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

        private bool ThesisModelExists(Guid id)
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

        private AssignedThesisModel LinkAssignedThesisData(AssignedThesisModel thesisModel)
        {
            if (thesisModel.ThesisID != Guid.Empty)
                thesisModel.Thesis =  _context.ThesisDBS.Find(thesisModel.ThesisID);
            if (thesisModel.StudentID != Guid.Empty)
                thesisModel.Student = _context.StudentsDBS.Find(thesisModel.StudentID);
            if (thesisModel.TeacherID != Guid.Empty)
                thesisModel.Teacher = _context.TeachersDBS.Find(thesisModel.TeacherID);

            return thesisModel;
        }
    }
}
