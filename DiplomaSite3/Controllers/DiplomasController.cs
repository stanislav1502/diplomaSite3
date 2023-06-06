﻿
using DiplomaSite3.Data;
using DiplomaSite3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using System.Collections.Generic;

namespace DiplomaSite3.Controllers
{
    public class DiplomasController : Controller
    {
        private readonly DiplomaSite3Context _context;

        public DiplomasController(DiplomaSite3Context context)
        {
            _context = context;
        }

        // GET: Diplomas
        public async Task<IActionResult> Index(string searchString)
        {
            var diplomas = _context.DiplomasDBS;
            if (diplomas == null)
            {
                return Problem("Entity set 'DiplomaSite3Context.Diplomas'  is null.");  
            }

            List<AdminDiplomaVM> viewModel = new List<AdminDiplomaVM>();
            foreach (var diploma in diplomas)
            {
                var teacher = await _context.TeachersDBS.FindAsync(diploma.TeacherID);
                var student = await _context.StudentsDBS.FindAsync(diploma.StudentID) ;

                var advm = new AdminDiplomaVM(diploma, teacher == null ? null : teacher.FullName, student == null ? null : student.FullName);
                viewModel.Add(advm);
            }

            return View(viewModel);

        }

        // GET: Diplomas/Details/5
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

        // GET: Diplomas/Create
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
