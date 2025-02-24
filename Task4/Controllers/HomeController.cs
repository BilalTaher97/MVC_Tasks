using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Task4.Models;

namespace Task4.Controllers
{
    public class HomeController : Controller
    {

        private readonly MyDbContext _context;


        public HomeController(MyDbContext context)
        {
            _context = context;
        }


        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            return View(_context.Products.ToList());
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Details(int? Id)
        {
            if(Id == null)
            {
                return RedirectToAction("Index");
            }

            var product = _context.Products.Find(Id);

            if(product == null)
            {
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public IActionResult Edit(int? Id)
        {

            if (Id == null)
            {
                return RedirectToAction("Index");
            }

            var product = _context.Products.Find(Id);

            if (product == null)
            {
                return RedirectToAction("Index");
            }

            return View(product);
        }


        [HttpPost]
        public IActionResult Edit(Product P)
        {
            _context.Update(P);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? Id)
        {

            if (Id == null)
            {
                return RedirectToAction("Index");
            }

            var product = _context.Products.Find(Id);

            if (product == null)
            {
                return RedirectToAction("Index");
            }

            _context.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
