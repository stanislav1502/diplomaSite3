﻿#nullable disable

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
          
            var users = _context.UsersDBS;
            if (users == null)
            {
                return Problem("Entity set 'DiplomaSite3Context.Users'  is null.");
            }

            var usersQuerry = from u in users
                                 select u;
            if (!string.IsNullOrEmpty(searchString))
            {
                usersQuerry = usersQuerry.Where(u => u.NormalizedUserName!.Contains(searchString.Normalize()) );
            }

            var viewModel = new AdminVM
            {
                AdminsList = await usersQuerry.Where(u => u.UserType == Enums.MyRolesEnum.Admin).ToListAsync(),
                TeachersList = await usersQuerry.Where(u => u.UserType == Enums.MyRolesEnum.Teacher).ToListAsync(),
                StudentsList = await usersQuerry.Where(u => u.UserType == Enums.MyRolesEnum.Student).ToListAsync()
            };

            return View(viewModel);

        }


        // DIPLOMAS

        [HttpGet]
        public async Task<IActionResult> DDetails(Guid? id)
        {
            if (id == null || _context.ThesisDBS == null)
            {
                return NotFound();
            }

            var thesisModel = await _context.ThesisDBS
                .FirstOrDefaultAsync(m => m.ThesisID == id);
            if (thesisModel == null)
            {
                return NotFound();
            }

            return View(thesisModel);
        }

        [HttpGet]
        public async Task<IActionResult> DEdit(Guid? id)
        {
            if (id == null || _context.ThesisDBS == null)
            {
                return NotFound();
            }

            var thesisModel = await _context.ThesisDBS.FindAsync(id);
            if (thesisModel == null)
            {
                return NotFound();
            }
            return View(thesisModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DEdit(Guid id, [Bind("ThesisID,Title,Description,DefendDate,Grade,Status,TeacherID,StudentID")] ThesisModel thesisModel)
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
                    var thesisModelExists = (_context.ThesisDBS?.Any(e => e.ThesisID == id)).GetValueOrDefault();
                    if (!thesisModelExists)
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

        [HttpGet]
        public async Task<IActionResult> DDelete(Guid? id)
        {
            if (id == null || _context.ThesisDBS == null)
            {
                return NotFound();
            }

            var thesisModel = await _context.ThesisDBS
                .FirstOrDefaultAsync(m => m.ThesisID == id);
            if (thesisModel == null)
            {
                return NotFound();
            }

            return View(thesisModel);
        }

        [HttpPost, ActionName("DDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.ThesisDBS == null)
            {
                return Problem("Entity set 'DiplomaSite3Context.Diplomas'  is null.");
            }
            var thesisModel = await _context.ThesisDBS.FindAsync(id);
            if (thesisModel != null)
            {
                _context.ThesisDBS.Remove(thesisModel);
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

        public async Task<IActionResult> EmailConfirm(Guid id)
        {
            if (id == Guid.Empty || _context.UsersDBS == null)
            {
                return Problem("No entities for operation");
            }

            var userModel = await _context.UsersDBS.FindAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }

            switch (userModel.UserType)
            {
                case Enums.MyRolesEnum.Student:
                    break;
                case Enums.MyRolesEnum.Teacher:
                    var teacherModel = await _context.TeachersDBS.FindAsync(id);
                    teacherModel.Verified = true;
                    _context.Update(teacherModel);
                    break;
                case Enums.MyRolesEnum.Admin:
                    var adminModel = await _context.TeachersDBS.FindAsync(id);
                    adminModel.Verified = true;
                    _context.Update(adminModel);
                    break;
                default:
                    break;
            }
            userModel.EmailConfirmed = true;
            
            _context.UsersDBS.Update(userModel);
            _context.SaveChanges();

            return RedirectToAction("Index","AdminPanel");
        }

        //// POST: Diplomas/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> UEdit(Guid id, [Bind("Id,Username,Email")] UserModel userModel)
        //{
        //    if (id != userModel.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(userModel);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            var userModelExists = (_context.UsersDBS?.Any(e => e.Id == id)).GetValueOrDefault();
        //            if (!userModelExists)
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(userModel);
        //}

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
                var teacherModel = await _context.TeachersDBS.FindAsync(id);
                _context.TeachersDBS.Remove(teacherModel);
                _context.UsersDBS.Remove(userModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
