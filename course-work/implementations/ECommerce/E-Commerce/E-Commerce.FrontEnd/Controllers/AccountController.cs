using E_Commerce.FrontEnd.Models.Auth;
using E_Commerce.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.FrontEnd.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthApiService _auth;

        public AccountController(AuthApiService auth)
        {
            _auth = auth;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
            {
                ViewBag.Error = "Provide email and password.";
                return View();
            }

            var token = await _auth.Login(model.Email, model.Password);
            if (token == null)
            {
                ViewBag.Error = "Login failed. Check credentials or API endpoint.";
                return View();
            }

            HttpContext.Session.SetString("JWToken", token);
            HttpContext.Session.SetString("UserEmail", model.Email);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JWToken");
            HttpContext.Session.Remove("UserEmail");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
            {
                ViewBag.Error = "Please provide full name, email, and password.";
                return View();
            }

            if (model.Password != model.ConfirmPassword)
            {
                ViewBag.Error = "Passwords do not match.";
                return View();
            }

            var token = await _auth.Register(model.Username, model.Email, model.Password, model.ConfirmPassword);

            if (token == null)
            {
                ViewBag.Error = "Registration failed.";
                return View();
            }

            HttpContext.Session.SetString("JWToken", token);
            HttpContext.Session.SetString("UserEmail", model.Email);

            return RedirectToAction("Index", "Home");
        }
    }
}
