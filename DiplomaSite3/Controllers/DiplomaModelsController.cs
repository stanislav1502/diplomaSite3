using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DiplomaSite3.Data;
using DiplomaSite3.Models;

namespace DiplomaSite3.Controllers
{
    public class DiplomaModelsController : Controller
    {
        private readonly DiplomaSite3Context _context;

        public DiplomaModelsController(DiplomaSite3Context context)
        {
            _context = context;
        }

        // GET: DiplomaModels
        public async Task<IActionResult> Index()
        {
              return _context.DiplomaModel != null ? 
                          View(await _context.DiplomaModel.ToListAsync()) :
                          Problem("Entity set 'DiplomaSite3Context.DiplomaModel'  is null.");
        }

        // GET: DiplomaModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.DiplomaModel == null)
            {
                return NotFound();
            }

            var diplomaModel = await _context.DiplomaModel
                .FirstOrDefaultAsync(m => m.DiplomaID == id);
            if (diplomaModel == null)
            {
                return NotFound();
            }

            return View(diplomaModel);
        }

        // GET: DiplomaModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DiplomaModels/Create
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

        // GET: DiplomaModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.DiplomaModel == null)
            {
                return NotFound();
            }

            var diplomaModel = await _context.DiplomaModel.FindAsync(id);
            if (diplomaModel == null)
            {
                return NotFound();
            }
            return View(diplomaModel);
        }

        // POST: DiplomaModels/Edit/5
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

        // GET: DiplomaModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.DiplomaModel == null)
            {
                return NotFound();
            }

            var diplomaModel = await _context.DiplomaModel
                .FirstOrDefaultAsync(m => m.DiplomaID == id);
            if (diplomaModel == null)
            {
                return NotFound();
            }

            return View(diplomaModel);
        }

        // POST: DiplomaModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.DiplomaModel == null)
            {
                return Problem("Entity set 'DiplomaSite3Context.DiplomaModel'  is null.");
            }
            var diplomaModel = await _context.DiplomaModel.FindAsync(id);
            if (diplomaModel != null)
            {
                _context.DiplomaModel.Remove(diplomaModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiplomaModelExists(Guid id)
        {
          return (_context.DiplomaModel?.Any(e => e.DiplomaID == id)).GetValueOrDefault();
        }
    }
}
