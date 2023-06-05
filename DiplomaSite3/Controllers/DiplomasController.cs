﻿
using DiplomaSite3.Data;
using DiplomaSite3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Index()
        {
            List<AdminDiplomaVM> vm = new List<AdminDiplomaVM>();
            var diplomas = _context.Diplomas;
            if (diplomas != null)
            {
                foreach (var diploma in diplomas)
                {

                    vm.Add(new AdminDiplomaVM(diploma));
                }
                return View(vm);
            }
            else 
            {
                return Problem("Entity set 'DiplomaSite3Context.Diplomas'  is null.");
            }

        }

        // GET: Diplomas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Diplomas == null)
            {
                return NotFound();
            }

            var diplomaModel = await _context.Diplomas
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
            if (id == null || _context.Diplomas == null)
            {
                return NotFound();
            }

            var diplomaModel = await _context.Diplomas.FindAsync(id);
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
            if (id == null || _context.Diplomas == null)
            {
                return NotFound();
            }

            var diplomaModel = await _context.Diplomas
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
            if (_context.Diplomas == null)
            {
                return Problem("Entity set 'DiplomaSite3Context.Diplomas'  is null.");
            }
            var diplomaModel = await _context.Diplomas.FindAsync(id);
            if (diplomaModel != null)
            {
                _context.Diplomas.Remove(diplomaModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiplomaModelExists(Guid id)
        {
          return (_context.Diplomas?.Any(e => e.DiplomaID == id)).GetValueOrDefault();
        }
    }
}
