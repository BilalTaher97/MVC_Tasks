using Microsoft.AspNetCore.Mvc;
using Task3.Models;

namespace Task3.Controllers
{
    public class DepartmentController : Controller
    {

        private readonly MyDbContext _context;


        public DepartmentController(MyDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Department);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult HandelCreate(Department Dep)
        {

            _context.Add(Dep);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
