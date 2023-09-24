
using DiplomaSite3.Data;
using DiplomaSite3.Enums;
using DiplomaSite3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

            foreach ( var item in viewModel.ThesisList)
            {
                LinkAssignedThesisData(item);
            }

			return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new AdminThesisVM();
            PopulateDegreesDropDownList();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminThesisVM adminThesisModel)
        {

            if (ModelState.IsValid)
            {
                var thesis = adminThesisModel.ThesisModel.Thesis;
                thesis.ThesisID = Guid.NewGuid();

                DegreeModel degree = new DegreeModel
                {
                   
                    Degree = thesis.Degree.Degree,
                    FacultyId = thesis.Degree.FacultyId,
                    DepartmentId = thesis.Degree.DepartmentId,
                    ProgrammeId = thesis.Degree.ProgrammeId
                };
                 var added = _context.DegreesDBS.Add(degree);
                _context.SaveChanges();

                await Console.Out.WriteLineAsync(added + "\n"+ added.ToString()+ "\n" + added.Entity.Id);

                thesis.Degree = _context.DegreesDBS.Find(added.Entity.Id);
                thesis.DegreeId = thesis.Degree.Id;
                _context.ThesisDBS.Add(thesis); 
                
                AssignedThesisModel assigned = new AssignedThesisModel
                {
                    ThesisID = thesis.ThesisID,
                    StudentID = null,
                    TeacherID = null,
                };

                var userID = new Guid(User.Claims.First().Value);
                var teacher = _context.TeachersDBS.Where(d => d.Id.Equals(userID)).First();
                assigned.TeacherID = teacher.Id;

                assigned = LinkAssignedThesisData(assigned);

                _context.AssignedThesesDBS.Add(assigned);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = thesis.ThesisID });
            }
            return RedirectToAction(nameof(Index));
        }

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
            
            viewModel.ThesisModel = LinkAssignedThesisData(thesisModel);
           
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
        public async Task<IActionResult> ViewRequests(Guid id, string? searchStudent) 
        {
            var model = new RequestedThesesVM();

            if (_context.AssignedThesesDBS is null || _context.TeachersDBS is null || _context.RequestedThesesDBS is null || _context.ThesisDBS is null)
                return Problem("Required Entity sets are null.");


            if (id.Equals(Guid.Empty) )
                return Problem("Invalid request form.");

            var thesis = await _context.ThesisDBS.FindAsync(id);
            var assignedThesis = await _context.AssignedThesesDBS.FindAsync(id);
            
            var teacher = await _context.TeachersDBS.FindAsync(assignedThesis.TeacherID);
            
            
            var requests = _context.RequestedThesesDBS;

            if (assignedThesis is null || teacher is null || requests is null || thesis is null)
                return Problem("Nonexistent entities.");

            assignedThesis = LinkAssignedThesisData(assignedThesis);

            model.RequestedThesis = thesis;

            var requestsQuerry = from r in requests select r;
            requestsQuerry=requestsQuerry.Where(x => x.ThesisID == id);

            if (!string.IsNullOrEmpty(searchStudent))
            {
                requestsQuerry = requestsQuerry.Where(r=>r.Student.FullName.Contains(searchStudent));
            }

            var requested = await requestsQuerry.ToListAsync();
            
            foreach (var request in requested)
            {
                var student = await _context.StudentsDBS.FindAsync(request.StudentID);
                model.StudentsList.Add(student);
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GiveThesis(IFormCollection collection)
        {

            if (_context.AssignedThesesDBS is null || _context.TeachersDBS is null || _context.RequestedThesesDBS is null || _context.ThesisDBS is null)
                return Problem("Required Entity sets are null.");

            // check Post has data
            string thesisID = collection["ThesisID"];
            string teacherID = collection["TeacherID"];
            string studentID = collection["StudentID"];

            if (thesisID.IsNullOrEmpty() || teacherID.IsNullOrEmpty() || studentID.IsNullOrEmpty())
                return Problem("Invalid request form.");

            var thesis = await _context.ThesisDBS.FindAsync(new Guid(thesisID));
            var teacher = await _context.TeachersDBS.FindAsync(new Guid(teacherID));
            var student = await _context.StudentsDBS.FindAsync(new Guid(studentID));

            var assignedThesis = await _context.AssignedThesesDBS.FindAsync(new Guid(thesisID));
            var requestedThesisModel = await _context.RequestedThesesDBS.FirstAsync(r => r.Thesis == assignedThesis.Thesis);

            // check student and thesis exist
            if (assignedThesis is null || teacher is null || requestedThesisModel is null || student is null)
                return Problem("Nonexistent entities.");

            if (assignedThesis.Teacher != teacher)
                return Problem("Teacher does not own this Thesis");

            if (ModelState.IsValid && assignedThesis.Thesis.Status == StatusEnum.Posted)
            {
                thesis.Status = StatusEnum.WIP;
                thesis.Assigned = assignedThesis;
                thesis.AssignDate = DateTime.UtcNow;
                assignedThesis.StudentID = student.Id;
                assignedThesis.Student = student;

                student.AssignedThesis = assignedThesis;

                _context.ThesisDBS.Update(thesis);
                _context.StudentsDBS.Update(student);
                _context.AssignedThesesDBS.Update(assignedThesis);
await _context.SaveChangesAsync();

                do
                {
                    _context.RequestedThesesDBS.Remove(requestedThesisModel);
                    requestedThesisModel = await _context.RequestedThesesDBS.FirstAsync(r => r.Thesis == assignedThesis.Thesis);
                    _context.SaveChanges();
                } 
                while (_context.RequestedThesesDBS.Contains(requestedThesisModel));

                
                return RedirectToAction("Index");
            }

            return RedirectToAction(nameof(Index), nameof(ThesisController));
        }

        [HttpGet]
        public async Task<IActionResult> SetDefense(Guid? id)
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
            thesisModel.Thesis.DefendDate = DateTime.Now.Date.AddDays(1).AddHours(9);

            return View(thesisModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetDefense(IFormCollection collection)
        {
            // check Post has data
            string thesisID = collection["ThesisID"];
            string defenseDate = collection["DefenseValue"];

            if (thesisID.IsNullOrEmpty() || defenseDate.IsNullOrEmpty())
                return Problem("Invalid request form.");

            if (_context.ThesisDBS is null)
                return Problem("Thesis Set is null.");

            var thesis = await _context.ThesisDBS.FindAsync(new Guid(thesisID));
            var date = DateTime.Parse(defenseDate);

            if (thesis != null)
            {
                if (date != null)
                    thesis.DefendDate = date;
                else thesis.DefendDate = DateTime.Now.AddDays(7.0);

                thesis.Status = StatusEnum.InAppraisal;

                _context.ThesisDBS.Update(thesis);
                await _context.SaveChangesAsync();
            }
            else return Problem("Thesis not found");

            return RedirectToAction("Index", "TeacherTheses");
        }

        
             [HttpGet]
        public async Task<IActionResult> SetGrade(Guid? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ThesisReviewer")]
        public async Task<IActionResult> SetGrade(IFormCollection collection)
        {
            // check Post has data
            string thesisID = collection["ThesisID"];
            string gradeValue = collection["GradeValue"];

            if (thesisID.IsNullOrEmpty() || gradeValue.IsNullOrEmpty())
                return Problem("Invalid request form.");

            if (_context.ThesisDBS is null)
                return Problem("Thesis Set is null.");

            var thesis = await _context.ThesisDBS.FindAsync(new Guid(thesisID));
            gradeValue= gradeValue.Normalize().Replace('.', ',');

            var grade = float.Parse(gradeValue);

            if (thesis != null)
            {
                if (grade != null)
                    thesis.Grade = (decimal)grade;
                else thesis.Grade = (decimal)1.0;

                thesis.Status = StatusEnum.Archived;

                _context.ThesisDBS.Update(thesis);
                await _context.SaveChangesAsync();
            }
            else return Problem("Thesis not found");

            return RedirectToAction("Index", "Thesis", new { Search="", SearchStatus=3 });
        }


        private AssignedThesisModel LinkAssignedThesisData(AssignedThesisModel thesisModel)
        {
            if (thesisModel.ThesisID != Guid.Empty)
                thesisModel.Thesis = _context.ThesisDBS.Find(thesisModel.ThesisID);
            if (thesisModel.StudentID != Guid.Empty)
                thesisModel.Student = _context.StudentsDBS.Find(thesisModel.StudentID);
            if (thesisModel.TeacherID != Guid.Empty)
                thesisModel.Teacher = _context.TeachersDBS.Find(thesisModel.TeacherID);
            if (thesisModel.Thesis != null)
                if (thesisModel.Thesis.DegreeId != 0)
                {
                    thesisModel.Thesis.Degree = _context.DegreesDBS.Find(thesisModel.Thesis.DegreeId);
                thesisModel.Thesis.Degree= LinkDegreeData(thesisModel.Thesis.Degree);
                }
            return thesisModel;
        }

        private DegreeModel LinkDegreeData(DegreeModel degreeModel)
        {

            if (degreeModel.FacultyId != null)
                degreeModel.Faculty = _context.FacultiesDBS.Find(degreeModel.FacultyId);
            if (degreeModel.DepartmentId != null)
                degreeModel.Department = _context.DepartmentsDBS.Find(degreeModel.DepartmentId);
            if (degreeModel.ProgrammeId != null)
                degreeModel.Programme = _context.ProgrammesDBS.Find(degreeModel.ProgrammeId);

            return degreeModel;
        }

        private void PopulateDegreesDropDownList(object selected = null)
        {
            var facultyQuery = from f in _context.FacultiesDBS
                               orderby f.FacultyName
                               select f;
            var departmentQuery = from d in _context.DepartmentsDBS
                                  orderby d.DepartmentName
                                  select d;
            var programmeQuery = from p in _context.ProgrammesDBS
                                 orderby p.ProgrammeName
                                 select p;

            ViewBag.FacultyList = new SelectList(facultyQuery, "Id", "FacultyName", selected);
            ViewBag.DepartmentList = new SelectList(departmentQuery, "Id", "DepartmentName", selected);
            ViewBag.ProgrammeList = new SelectList(programmeQuery, "Id", "ProgrammeName", selected);

        }
    }
}
