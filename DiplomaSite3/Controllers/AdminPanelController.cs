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
    [Authorize(Roles = "Admin")]
    public class AdminPanelController : Controller
    {
        private readonly DiplomaSite3Context _context;

        public AdminPanelController(DiplomaSite3Context context)
        {
            _context = context;
        }

        // GET: AdminPanel
        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            var diplomas = _context.DiplomasDBS;
            if (diplomas == null)
            {
                return Problem("Entity set 'DiplomaSite3Context.Diplomas'  is null.");  
            }
            var users = _context.UsersDBS;
            if (users == null)
            {
                return Problem("Entity set 'DiplomaSite3Context.Users'  is null.");
            }

            var diplomasQuerry = from d in diplomas
                         select d;
            var usersQuerry = from u in users
                                 select u;
            if (!string.IsNullOrEmpty(searchString))
            {
                diplomasQuerry = diplomasQuerry.Where(s => s.Title!.Contains(searchString));
            }

            var viewModel = new AdminVM
            {
                Diplomas = await diplomasQuerry.ToListAsync(),
                Users = await usersQuerry.ToListAsync()
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

        // DIPLOMAS

        [HttpGet]
        public async Task<IActionResult> DDetails(Guid? id)
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
        public async Task<IActionResult> DEdit(Guid? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DEdit(Guid id, [Bind("DiplomaID,Title,Description,DefendDate,Grade,Tags,Status,TeacherID,StudentID")] DiplomaModel diplomaModel)
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
                    var diplomaModelExists = (_context.DiplomasDBS?.Any(e => e.DiplomaID == id)).GetValueOrDefault();
                    if (!diplomaModelExists)
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

        [HttpGet]
        public async Task<IActionResult> DDelete(Guid? id)
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

        [HttpPost, ActionName("DDelete")]
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

        // USERS

        [HttpGet]
        public async Task<IActionResult> UDetails(Guid? id)
        {
            if (id == null || _context.UsersDBS == null)
            {
                return NotFound();
            }

            var userModel = await _context.UsersDBS
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        public async Task<IActionResult> UEdit(Guid? id)
        {
            if (id == null || _context.UsersDBS == null)
            {
                return NotFound();
            }

            var userModel = await _context.UsersDBS.FindAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }
            return View(userModel);
        }

        // POST: Diplomas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UEdit(Guid id, [Bind("Id,Username,Email")] UserModel userModel)
        {
            if (id != userModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    var userModelExists = (_context.UsersDBS?.Any(e => e.Id == id)).GetValueOrDefault();
                    if (!userModelExists)
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
            return View(userModel);
        }

        [HttpGet]
        // GET: Diplomas/Delete/5
        public async Task<IActionResult> UDelete(Guid? id)
        {
            if (id == null || _context.UsersDBS == null)
            {
                return NotFound();
            }

            var userModel = await _context.UsersDBS
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // POST: Diplomas/Delete/5
        [HttpPost, ActionName("UDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UDeleteConfirmed(Guid id)
        {
            if (_context.UsersDBS == null)
            {
                return Problem("Entity set 'DiplomaSite3Context.Users'  is null.");
            }
            var userModel = await _context.UsersDBS.FindAsync(id);
            if (userModel != null)
            {
                _context.UsersDBS.Remove(userModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
