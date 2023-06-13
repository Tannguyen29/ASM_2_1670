using ASM_2_1670.Areas.Admin.Models;
using ASM_2_1670.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ASM_2_1670.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly ASM_2_1670Context _context;

        public AccountController(ASM_2_1670Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

		[HttpPost]
		public async Task<IActionResult> Login(User _user)
		{
			var _users = _context.User.Where(model => model.UserEmail == _user.UserEmail && model.UserPassword == _user.UserPassword).FirstOrDefault();

			if (_users == null)
			{
				ViewBag.LoginStatus = 0;
			}
			else
			{
				var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, _users.UserEmail),
			new Claim("FullName", _users.UserName),
			new Claim(ClaimTypes.Role, _users.UserRole),
		};

				var claimsIdentity = new ClaimsIdentity(
					claims, CookieAuthenticationDefaults.AuthenticationScheme);

				var authProperties = new AuthenticationProperties();

				await HttpContext.SignInAsync(
					CookieAuthenticationDefaults.AuthenticationScheme,
					new ClaimsPrincipal(claimsIdentity),
					authProperties);

				// Redirect to different pages depending on the user's role
				if (_users.UserRole == "Admin")
				{
					return RedirectToAction("Index", "Admin");
				}
				else
				{
					return RedirectToAction("Index", "Home");
				}
			}

			return View();
		}



		public IActionResult Logout()
        {
            HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Account");
        }
    }
}
