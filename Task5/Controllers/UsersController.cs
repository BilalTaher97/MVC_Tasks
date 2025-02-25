using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Task5.Models;

namespace Task5.Controllers
{
    public class UsersController : Controller
    {
        private readonly MyDbContext _context;

        
        public UsersController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Password")] User user)
        {
            user.Role = "User";
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Password,Role")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            // تحميل الكائن من قاعدة البيانات
            var newUser = await _context.Users.FindAsync(user.Id);

            if (newUser == null)
            {
                return NotFound();
            }

            // التحقق من الدور وتعيين القيم المناسبة
            if (newUser.Role == "User")
            {
                user.Role = "User";
            }
            else
            {
                user.Role = "Admin";
            }

            // التحقق من صحة النموذج
            if (ModelState.IsValid)
            {
                try
                {
                    // فصل الكائن القديم إذا كان في السياق بالفعل
                    _context.Entry(newUser).State = EntityState.Detached;

                    // تحديث الكائن
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // التعامل مع أخطاء التزامن
                    if (!UserExists(user.Id))
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

            return View(user);

        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }



        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult HandelLogin(User user)
        {

            var U1 = _context.Users.FirstOrDefault(u => user.Email == u.Email && user.Password == u.Password);

            if (U1 == null)
            {
                return RedirectToAction("Login");
            }

            HttpContext.Session.SetInt32("_ID", U1.Id);
            HttpContext.Session.SetString("_Name", U1.Name);
            HttpContext.Session.SetString("_Email", U1.Email);
            HttpContext.Session.SetString("_Password", U1.Password);            
            HttpContext.Session.SetString("_Role", U1.Role);
            
          

            return RedirectToAction("Index", "Home", U1);
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            user.Role = "User";
            _context.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login");

        }


        public IActionResult Profile()
        {
           

            return View();
        }


        public IActionResult EditProfile()
        {

            int? Id = HttpContext.Session.GetInt32("_ID");

            var user = _context.Users.FirstOrDefault(user => user.Id == Id);

            if (user == null)
            {
                return RedirectToAction("Profile");
            }




            return View(user);
        }


        [HttpPost]
        public IActionResult EditProfile(User user)
        {
            int? Id = HttpContext.Session.GetInt32("_ID");
            var user2 = _context.Users.FirstOrDefault(u => u.Id == Id);

            if (user2 == null)
            {
                return RedirectToAction("EditProfile");
            }

            user2.Name = user.Name;
            user2.Email = user.Email;
            user2.Password = user.Password;

            HttpContext.Session.SetInt32("_ID", Id.Value);
            HttpContext.Session.SetString("_Name", user2.Name);
            HttpContext.Session.SetString("_Email", user2.Email);
            HttpContext.Session.SetString("_Password", user2.Password);
            HttpContext.Session.SetString("_Role", user2.Role);

            _context.SaveChanges();
           
           return RedirectToAction("Profile");
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login");

        }

        public IActionResult Admin()
        {


            return View();

        }
    }
}
