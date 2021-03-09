using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Task9_1.Data;
using Task9_1.Models;

namespace Task9_1.Controllers
{
    public class StudentsController : Controller
    {
        private readonly Task9_1Context _context;

        public StudentsController(Task9_1Context context)
        {
            _context = context;
        }
        static int groupID;
        // GET: Students
        public async Task<IActionResult> Index(int id)
        {
            groupID = id;
            return View(await _context.Student.Where(c => c.GroupID == id).ToListAsync());
        }

       
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,GroupID,FirstName,LastName")] Student student)
        {
            if (ModelState.IsValid)
            {
                student.GroupID = groupID;
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Students", new { id = groupID });
            }
            return View(student);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,GroupID,FirstName,LastName")] Student student)
        {
            if (id != student.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Student.Attach(student);
                    _context.Entry(student).Property(x => x.FirstName).IsModified = true;
                    _context.Entry(student).Property(x => x.LastName).IsModified = true;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Students", new { id = groupID });
            }
            return View(student);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Student.FindAsync(id);
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Students", new { id = groupID });
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.ID == id);
        }
    }
}
