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


        public IActionResult Details(int? Id)
        {
            if(Id == null)
            {
                return RedirectToAction("Index");
            }

            var user = _Context.Users.FirstOrDefault(x => x.Id == Id);

            if(user == null)
            {
                return RedirectToAction("Index");
            }

            return View(user);
        }


        public IActionResult Delete(int? Id)
        {
            if(Id == null)
            {
                return RedirectToAction("Index");
            }

            var user = _Context.Users.FirstOrDefault(y => y.Id == Id);

            if(user == null)
            {
                return RedirectToAction("Index");
            }


            return View(user);
        }

        public IActionResult ConDelete(int Id)
        {
           

            var user = _Context.Users.FirstOrDefault(y => y.Id == Id);

            if(user == null)
            {
                return RedirectToAction("Index");
            }

            _Context.Users.Remove(user);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult HandelCreate(User user)
        {
            _Context.Users.Add(user);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int? Id)
        {
            if(Id == null)
            {
                return RedirectToAction("Index");
            }

            var user = _Context.Users.FirstOrDefault(x => x.Id == Id);

            if(user == null)
            {
                return RedirectToAction("Index");
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult HandelEdit(User user)
        {

            if(ModelState.IsValid)
            {
                _Context.Update(user);
                _Context.SaveChanges();
            }
           

            return RedirectToAction("Index");
        }







        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
