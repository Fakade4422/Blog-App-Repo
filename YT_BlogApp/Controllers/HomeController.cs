using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using YT_BlogApp.Models;
//Authentication
using Blog.Auth.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Blog.Auth.Repository;
using YT_BlogApp.Services;
using Blog.Administrator.Repository;
using Blog.Administrator.Models;
using System.Net.Mail;
using BCrypt.Net;
using System.Text.RegularExpressions;

namespace YT_BlogApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //Authentication
        private readonly IAuthRepository _authRepo;
        private readonly IAdministratorRepository _adminRepo;
        private readonly IUserInfoServices _userServices;
        int OTP;
        User user;

        public HomeController(ILogger<HomeController> logger, IAuthRepository authRepo, IAdministratorRepository adminRepo, IUserInfoServices userServices)
        {
            _logger = logger;
            _authRepo = authRepo;
            _adminRepo = adminRepo;
            _userServices = userServices;
        }

        public async Task <IActionResult> Index()
        {

            return View();
        }
        public IActionResult Blog()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Services()
        {
            return View();
        }public IActionResult Contact()
        {
            return View();
        }

        /*---Authentication, For the sign in page  ------*/
        public IActionResult SignIn(string returnUrl = "/")
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(Login login)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Invalid Email or Password";
                return View(login);
            }

                var user = await _authRepo.ValidateUser(login);
                bool validatePassword = false;

                if (user != null)
                {
                    if (user.Password.StartsWith("$2a$") || user.Password.StartsWith("$2b$") || user.Password.StartsWith("$2y$"))
                    {
                        validatePassword = BCrypt.Net.BCrypt.Verify(login.Password, user.Password);
                    }
                    else
                    {
                        // Temporary: Handle plaintext passwords during transition
                        validatePassword = user.Password == login.Password;
                        if (validatePassword)
                        {
                            // Hash and update the password
                            user.Password = BCrypt.Net.BCrypt.HashPassword(login.Password);
                            await _authRepo.UpdateUserPassword(user); // Add UpdateUser to IAuthRepository
                        }
                    }
     
                }

                if (user == null || validatePassword == false)
                {
                    ViewBag.Message = "Invalid Email or password";
                    return View(login);
                }
                else
                {

                    try
                    {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.UserID)),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                    new AuthenticationProperties()
                    {
                        IsPersistent = login.RememberMe
                    });

                    ActiveUser activeUser = new ActiveUser
                        {
                            UserID = user.UserID,
                            TimeLoggedIn = DateTime.Now,
                            DayLoggedIn = DateTime.Today
                        };
                        await _adminRepo.NewActiveUser(activeUser);


                        if (User.Identity.IsAuthenticated)
                        {
                            string role = await _userServices.GetRole();

                            if (role == "Admin" && user.Role == "Admin")
                            {
                                return RedirectToAction("ManagePosts", "Admin");
                            }

                        }
                    
                        return RedirectToAction("Index", "Home"); // Fallback redirect
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "An error occurred during sign-in. Please try again.";
                        _logger.LogError(ex, "Error during sign-in for user {Email}", user.Email);
                        return View(login);
                    }


                }
               
        }

        public IActionResult SignUp()
        {
            return View();
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
