using Microsoft.AspNetCore.Mvc;
using ESandMSProject.Models;
using ESandMSProject.Models.Domain;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json;

namespace ESandMSProject.Controllers
{
    public class LoginsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LoginsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Login login)
        {
            // Role A = Admin , Role S = Student
            var loginInfos = _context.Logins.FirstOrDefault(x => x.Username == login.Username && x.Password == login.Password);
            if (loginInfos != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,loginInfos.Id.ToString()),
                    new Claim(ClaimTypes.Name,login.Username),
                    new Claim(ClaimTypes.Role, loginInfos.Roles)
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                if (loginInfos.Roles.Contains("A"))
                {
                    return Json(new { success = true, role = "A", redirectTo = Url.Action("Index", "Halls") });
                }
                else if (loginInfos.Roles.Contains("S"))
                {
                    // Set cookie value with Id included
                    Response.Cookies.Append("loginInfos", JsonConvert.SerializeObject(new { Id = loginInfos.Id, Username = login.Username, Roles = loginInfos.Roles }));
                    return Json(new { success = true, role = "S", redirectTo2 = Url.Action("Index", "TimeTable") });
                }
            }
            return Json(new { success = false, message = "Login is wrong!" });
        }
        [HttpGet]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete("loginInfos");
            return RedirectToAction("Index", "Logins");
        }
    }
}
