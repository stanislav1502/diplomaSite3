
using DiplomaSite3.Data;
using DiplomaSite3.Enums;
using DiplomaSite3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

            foreach ( var assignedThesis in viewModel.ThesisList)
            {
				var thesis = await _context.ThesisDBS.FindAsync(assignedThesis.ThesisID);
				assignedThesis.Thesis = thesis is not null ? thesis : null;

				var student = await _context.StudentsDBS.FindAsync(assignedThesis.StudentID);
				assignedThesis.Student = student is not null ? student : null;
				
				var teacher = await _context.TeachersDBS.FindAsync(assignedThesis.TeacherID);
				assignedThesis.Teacher = teacher is not null ? teacher : null;
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
            var requestedThesisModel = await _context.RequestedThesesDBS.FirstAsync(r => r.Thesis == assignedThesis.Thesis );

            // check student and thesis exist
            if (assignedThesis is null || teacher is null || requestedThesisModel is null || student is null)
                return Problem("Nonexistent entities.");

            if (assignedThesis.Teacher != teacher)
                return Problem("Teacher does not own this Thesis");

            if (ModelState.IsValid && assignedThesis.Thesis.Status == StatusEnum.Posted)
            {
                thesis.Status = StatusEnum.WIP;
                thesis.Assigned = assignedThesis;

                assignedThesis.StudentID = student.Id;
                assignedThesis.Student = student;

                student.AssignedThesis = assignedThesis;

                _context.ThesisDBS.Update(thesis);
                _context.StudentsDBS.Update(student);
                _context.AssignedThesesDBS.Update(assignedThesis);
                _context.RequestedThesesDBS.Remove(requestedThesisModel);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), nameof(ThesisController));
        }

    }
}
