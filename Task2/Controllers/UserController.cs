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

        [HttpPost]
        public IActionResult HandelRegister(string Email, string Password, string UserName)
        {
            if (Email != null && Password != null)
            {

                HttpContext.Session.SetString(SessionUserName, UserName);
                HttpContext.Session.SetString(SessionEmail, Email);
                HttpContext.Session.SetString(SessionPassword, Password);

                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("Register");
            }

           
        }


        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public IActionResult HandelLogin(string Email, string Password)
        {
            string? email = HttpContext.Session.GetString(SessionEmail);
            string? password = HttpContext.Session.GetString(SessionPassword);


            if (email != null && password != null)
            {

                TempData["email"] = email;
                TempData["password"] = password;
                TempData["username"] = HttpContext.Session.GetString(SessionUserName);



                if (Email == email && Password == password)
                {


                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Login");
                }

            }
            else
            {
                return RedirectToAction("Login");
            }


        }

        public IActionResult Profile()
        {
            return View();
        }
    }
}
