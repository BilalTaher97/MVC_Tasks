using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Task3.Models;



namespace Task3.Controllers
{
    public class HomeController : Controller
    {

        private readonly MyDbContext _Context;



        public HomeController(MyDbContext context)
        {
            _Context = context;
        }


        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {

            return View(_Context.Users.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult HandelCreate(User user)
        {
            _Context.Users.Add(user);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
