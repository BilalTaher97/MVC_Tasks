using System.Runtime.Intrinsics.Arm;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

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

                TempData["email"] = Email;
                TempData["password"] = Password;
                TempData["username"] = UserName;

                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("Register");
            }

           
        }


        public IActionResult Login()
        {

            



            string data = Request.Cookies["userInfo"];
            string data_2 = Request.Cookies["username"];
            string data_3 = Request.Cookies["password"];

            if (data != null)
                return RedirectToAction("Index", "Home");
            else
                return View();
        
        }

        [HttpPost]
        public IActionResult HandelLogin(string Email, string Password , string rememberMe)
        {

            if(TempData["username"] == null)
            {
                return RedirectToAction("Login");
            }

            HttpContext.Session.SetString(SessionUserName, TempData["username"].ToString());
            HttpContext.Session.SetString(SessionEmail, TempData["email"].ToString());
            HttpContext.Session.SetString(SessionPassword, TempData["password"].ToString());


            string? email = HttpContext.Session.GetString(SessionEmail);
            string? password = HttpContext.Session.GetString(SessionPassword);


            if (email != null && password != null)
            {


                if (Email == email && Password == password)
                {
                    if (rememberMe != null)
                    {
                        CookieOptions obj = new CookieOptions();
                        obj.Expires = DateTime.Now.AddDays(2);

                        Response.Cookies.Append("userInfo", TempData["email"].ToString(), obj);
                        Response.Cookies.Append("username", TempData["username"].ToString(), obj);
                        Response.Cookies.Append("password", TempData["password"].ToString(), obj);
                
                    }


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

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            Response.Cookies.Delete("userInfo");


            return RedirectToAction("Login");
        }

        public IActionResult Admin()
        {
            return View();
        }
    }
}
