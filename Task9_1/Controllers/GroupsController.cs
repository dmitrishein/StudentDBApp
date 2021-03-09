using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task9_1.Data;
using Task9_1.Models;

namespace Task9_1.Controllers
{
    public class GroupsController : Controller
    {
        private readonly Task9_1Context _context;

        public GroupsController(Task9_1Context context)
        {
            _context = context;
        }
        static int courseID;
        // GET: Groups
        public async Task<IActionResult> Index(int id)
        {
            courseID = id;
            return View(await _context.Group.Where(c => c.CourseID == id).ToListAsync());
        }
        
        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Group
                .FirstOrDefaultAsync(m => m.ID == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CourseID,Name")] Group @group)
        {
            if (ModelState.IsValid)
            {
                group.CourseID = courseID;
                _context.Add(@group);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Groups", new { id = courseID });
            }
            return View(@group);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Group.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }
            return View(@group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CourseID,Name")] Group @group)
        {
            if (id != @group.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(@group.Name);
                    //await _context.SaveChangesAsync();
                    _context.Group.Attach(group);
                    _context.Entry(group).Property(x => x.Name).IsModified = true;
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Groups", new { id = courseID });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(@group.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(@group);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Group
                .FirstOrDefaultAsync(m => m.ID == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @group = await _context.Group.FindAsync(id);
            if (!StudentsExists(id))
            {
                _context.Group.Remove(@group);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Groups", new { id = courseID });
            }
            else
                return RedirectToAction("Index", "Groups", new { id = courseID });

        }

        private bool GroupExists(int id)
        {
            return _context.Group.Any(e => e.ID == id);
        }

        private bool StudentsExists(int id)
        {
            return _context.Student.Where(x => x.GroupID == id).Any();
        }
    }
}
