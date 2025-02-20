using Microsoft.AspNetCore.Mvc;

namespace Task2.Controllers
{
    public class UserController : Controller
    {

        const string SessionUserName = "_UserName";
        const string SessionEmail = "_Email";
        const string SessionPassword = "_Password";
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        


        public IActionResult Login()
        {

            return View();
        }


       


        public IActionResult Profile()
        {
            return View();
        }
    }
}
