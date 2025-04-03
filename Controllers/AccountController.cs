using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManagementMVC.Models;
using TaskManagementMVC.Repository;

namespace TaskManagementMVC.Controllers
{
    //[Route("account")] 
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        //  GET: account/register
        [HttpGet("")]
        [HttpGet("account/register")]
        public IActionResult Register()
        {
            ViewBag.GenderList = GetGenderList();
            return View();
        }

        //  POST: account/register
        [HttpPost("account/register")]
        public async Task<IActionResult> Register(UserModel user)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.GenderList = GetGenderList(); 
                return View(user);
            }

            bool userExists = await _userRepository.IsUsernameExists(user.UserName);
            if (userExists)
            {
                ModelState.AddModelError("UserName", "Username already exists.");
                ViewBag.GenderList = GetGenderList();
                return View(user);
            }

            bool isRegistered = await _userRepository.RegisterUser(user);
            if (isRegistered)
                return RedirectToAction("Login");

            ModelState.AddModelError("", "Registration failed. Please try again.");
            ViewBag.GenderList = GetGenderList();
            return View(user);
        }

        //  GET: account/login
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        //  POST: account/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("", "Username and Password are required.");
                return View();
            }

            var user = await _userRepository.AuthenticateUser(username, password);
            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("UserName", user.UserName);
                return RedirectToAction("Index", "Task");
            }

            ModelState.AddModelError("", "Invalid username or password.");
            return View();
        }

        //  GET: account/logout
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName"); // Ensure individual keys are removed
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("Login","Account");
        }
        private List<SelectListItem> GetGenderList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Male" },
                new SelectListItem { Value = "2", Text = "Female" },
                new SelectListItem { Value = "3", Text = "Other" }
            };
        }
    }
}
