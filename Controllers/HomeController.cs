using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using APP.Models;

namespace APP.Controllers
{
    public class HomeController : Controller
    {
        private readonly SDMSDbContext _context;

        public HomeController(SDMSDbContext context)
        {
            _context = context;
        }

        // GET: Home
        //public async Task<IActionResult> Index()
        //{
        //    var check = HttpContext.Session.GetString("UserSession");
        //    if (check != null)
        //    {
        //        return View(await _context.Students.ToListAsync());
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login");
        //    }
        //}
        public async Task<IActionResult> Index()
        {
            var check = HttpContext.Session.GetString("UserSession");
            if(check != null)
            {
                ViewBag.Check = check;
            }
            return View(await _context.Students.ToListAsync());
        }

        // GET: Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Home/Create
        public IActionResult Create()
        {
            var check = HttpContext.Session.GetString("UserSession");
            if(check != null)
            {
            return View();

            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // POST: Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,FatherName,Gender,Age,Class")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var student = await _context.Students.FindAsync(id);
            var check = HttpContext.Session.GetString("UserSession");
            if(check != null)
            {
            return View(student);
            }
            if (id == null)
            {
                return NotFound();
            }
            if (student == null)
            {
                return NotFound();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FatherName,Gender,Age,Class")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            return View(student);
        }

        // GET: Home/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
            var check = HttpContext.Session.GetString("UserSession");
            if (check != null)
            {
                return View(student);
            }
            if (id == null)
            {
                return NotFound();
            }

            if (student == null)
            {
                return NotFound();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Auth user)
        {
            var myUser = _context.Auths.Where(x=> x.Username == user.Username && x.Password == user.Password).FirstOrDefault();
            if(myUser != null)
            {
                HttpContext.Session.SetString("UserSession",myUser.Username);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Login Failed ...";
            }
            return View();
        }
        public IActionResult Logout()
        {
            var check = HttpContext.Session.GetString("UserSession");
            if(check != null){
                HttpContext.Session.Remove("UserSession");
                TempData["logout"] = "Successfully logout...";
                return RedirectToAction("Index");
            }
            return  View();
        }
    }
}
