using APP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APP.Controllers
{
    public class AuthsController : Controller
    {
        private readonly SDMSDbContext _context;

        public AuthsController(SDMSDbContext context)
        {
            _context = context;
        }

        // GET: Auths
        public async Task<IActionResult> Index()
        {
            var check = HttpContext.Session.GetString("UserSession");
            if (check != null)
            {
                ViewBag.Check = check;
                return View(await _context.Auths.ToListAsync());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: Auths/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var check = HttpContext.Session.GetString("UserSession");
            var auth = await _context.Auths.FirstOrDefaultAsync(m => m.Id == id);
            if (check != null)
            {
                ViewBag.Check = check;
                return View(auth);
            }

            if (id == null)
            {
                return NotFound();
            }

            if (auth == null)
            {
                return NotFound();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: Auths/Create
        public IActionResult Create()
        {
            var check = HttpContext.Session.GetString("UserSession");
            if (check != null)
            {
                ViewBag.Check = check;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Auths/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Password")] Auth auth)
        {
            if (ModelState.IsValid)
            {
                _context.Add(auth);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(auth);
        }

        // GET: Auths/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auth = await _context.Auths.FindAsync(id);
            if (auth == null)
            {
                return NotFound();
            }
            var check = HttpContext.Session.GetString("UserSession");
            if (check != null)
            {
                ViewBag.Check = check;
                return View(auth);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
            
        }

        // POST: Auths/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password")] Auth auth)
        {
            if (id != auth.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(auth);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthExists(auth.Id))
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
            return View(auth);
        }

        // GET: Auths/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auth = await _context.Auths
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auth == null)
            {
                return NotFound();
            }
            var check = HttpContext.Session.GetString("UserSession");
            if(check != null)
            {
                ViewBag.Check = check;
            return View(auth);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Auths/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var auth = await _context.Auths.FindAsync(id);
            if (auth != null)
            {
                _context.Auths.Remove(auth);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthExists(int id)
        {
            return _context.Auths.Any(e => e.Id == id);
        }
    }
}
